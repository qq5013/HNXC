﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using System.Data.OracleClient;

namespace THOK.XC.Process.Dao
{
    public  class TaskDao : BaseDao
    {
        /// <summary>
        /// 根据出库任务生成Task_Detail，并返回。
        /// </summary>
        /// <returns></returns>
        public DataTable TaskOutToDetail()
        {
            //处理一楼出库(抽检倒库12、盘点13、移库14)，生成Task_Detail。
            DataTable dtCraneTask = CraneTaskOut("TASK.TASK_TYPE IN('12','13'，'14') AND TASK.STATE='0'");

            //string strWhere = string.Format(" TASK.TASK_TYPE='22' AND TASK.STATE='0'");
            //DataTable dt = CraneTaskOut(strWhere);
            ////与1楼出库任务进行合并
            //dtCraneTask.Merge(dt);

            string strBillNo = "";
            //找出2楼状态为1正在执行出库任务的出库单
            string strSQL = "SELECT * FROM WCS_TASK_DETAIL " +
                            "LEFT JOIN WCS_TASK ON WCS_TASK_DETAIL.TASK_ID=WCS_TASK.TASK_ID " +
                            "WHERE TASK_TYPE='22' AND CRANE_NO IS NOT NULL AND WCS_TASK_DETAIL.STATE ='1' " +
                            "ORDER BY WCS_TASK.BILL_NO DESC";
            DataTable dt = ExecuteQuery(strSQL).Tables[0];

            //如果没有正在出库的任务，找二楼还没有产生TASK_DTAIL的出库单
            if (dt.Rows.Count == 0)
            {
                strSQL = "SELECT  DISTINCT WMS_BILL_MASTER.SCHEDULE_NO,WMS_BILL_MASTER.SCHEDULE_ITEMNO,WCS_TASK.TASK_LEVEL, WCS_TASK.TASK_DATE,WCS_TASK.BILL_NO " +
                         "FROM WCS_TASK " +
                         "INNER JOIN WMS_BILL_MASTER  ON WCS_TASK.BILL_NO=WMS_BILL_MASTER.BILL_NO " +
                         "WHERE WCS_TASK.STATE=0 AND WCS_TASK.TASK_TYPE='22' " +
                         "ORDER BY WMS_BILL_MASTER.SCHEDULE_NO,WMS_BILL_MASTER.SCHEDULE_ITEMNO,WCS_TASK.TASK_LEVEL, WCS_TASK.TASK_DATE,WCS_TASK.BILL_NO ";

                dt = ExecuteQuery(strSQL).Tables[0];
            }
            if (dt.Rows.Count > 0)
            {
                //正在出或者准备要出的出库单号
                strBillNo = dt.Rows[0]["BILL_NO"].ToString();
                string strWhere = string.Format(" TASK.TASK_TYPE='22' and TASK.BILL_NO ='{0}' AND TASK.STATE='0'", strBillNo);
                dt = CraneTaskOut(strWhere);
                //与1楼出库任务进行合并
                dtCraneTask.Merge(dt);
            }
            return dtCraneTask;
        }
        /// <summary>
        /// 出入库，插入明细。如果明细已经存在则不进行重新插入， 返回TaskNo
        /// </summary>
        /// <param name="task_id"></param>
        /// <returns></returns>
        public string InsertTaskDetail(string TaskID)
        {
            string strTaskDetailNo = "";

            string strSQL = string.Format("SELECT TASK_NO FROM WCS_TASK_DETAIL WHERE TASK_ID='{0}' ", TaskID);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strTaskDetailNo = dt.Rows[0][0].ToString();
            }
            else
            {
                strTaskDetailNo = GetTaskDetailNo(TaskID);

                strSQL = string.Format("INSERT INTO WCS_TASK_DETAIL(TASK_ID,ITEM_NO,TASK_NO,ASSIGNMENT_ID,STATE,DESCRIPTION,BILL_NO,CRANE_NO) " +
                         "SELECT WCS_TASK.TASK_ID,SYS_TASK_ROUTE.ITEM_NO,'{1}','{2}','0'," +
                         "SYS_TASK_ROUTE.DESCRIPTION,WCS_TASK.BILL_NO,CASE WHEN SYS_TASK_ROUTE.ITEM_NO=1 AND WCS_TASK.TASK_TYPE IN('12','22','13','14') THEN CMD_SHELF.CRANE_NO ELSE NULL END " +
                         "FROM WCS_TASK " +
                         "LEFT JOIN CMD_CELL ON CMD_CELL.CELL_CODE=WCS_TASK.CELL_CODE " +
                         "LEFT JOIN CMD_SHELF ON CMD_CELL.SHELF_CODE=CMD_SHELF.SHELF_CODE " +                         
                         "LEFT JOIN SYS_TASK_ROUTE ON WCS_TASK.TASK_TYPE=SYS_TASK_ROUTE.TASK_TYPE " +
                         "WHERE TASK_ID='{0}' " +
                         "ORDER BY SYS_TASK_ROUTE.ITEM_NO ", TaskID, strTaskDetailNo, "0000" + strTaskDetailNo);
                ExecuteNonQuery(strSQL);

                dt = TaskInfo(string.Format("TASK_ID='{0}'", TaskID));
                if (dt.Rows.Count > 0)
                {
                    string TaskType = dt.Rows[0]["TASK_TYPE"].ToString();
                    if (TaskType == "13")
                    {
                        strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET ASSIGNMENT_ID='{0}' WHERE TASK_ID='{1}' AND ITEM_NO=4 ", strTaskDetailNo.PadLeft(8, '1'), TaskID);
                        ExecuteNonQuery(strSQL);
                    }
                }
            }
            return strTaskDetailNo;
        }

     

        /// <summary>
        /// 获取已经插入Task_Detail 中，堆垛机调度程序。
        /// </summary>
        /// <returns></returns>
        public DataTable CraneTaskIn(string strWhere)
        {
            string where = strWhere;
            if (strWhere.Trim() == "")
                where = "1=1";
            string strSQL = "SELECT TASK.TASK_ID,TASK_NO,DETAIL.ITEM_NO,ASSIGNMENT_ID,DETAIL.CRANE_NO, '30'||TASK.CELL_CODE||'01' AS CELLSTATION,SYS_STATION.CRANE_POSITION CRANESTATION ,DETAIL.STATE,TASK.BILL_NO," +
                            "TASK.PRODUCT_CODE,TASK.CELL_CODE,TASK.TASK_TYPE,TASK.TASK_LEVEL,TASK.TASK_DATE,TASK.IS_MIX,SYS_STATION.SERVICE_NAME,SYS_STATION.ITEM_NAME_1," +
                            "SYS_STATION.ITEM_NAME_2,TASK.PRODUCT_BARCODE,TASK.PALLET_CODE,'' AS SQUENCE_NO,TASK.TARGET_CODE,SYS_STATION.STATION_NO,SYS_STATION.MEMO,TASK.PRODUCT_TYPE,CMD_SHELF_NEW.CRANE_NO AS NEW_CRANE_NO,TASK.NEWCELL_CODE,  " +
                            "DECODE(TASK.NEWCELL_CODE,NULL,'',  '30'||TASK.NEWCELL_CODE||'01') AS NEW_TO_STATION,SYS_STATION_NEW.STATION_NO AS NEW_TARGET_CODE,TASK.FORDER,TASK.FORDERBILLNO,DETAIL.ERR_CODE,BMASTER.BTYPE_CODE,BMASTER.BILL_METHOD " +
                            "FROM WCS_TASK_DETAIL DETAIL " +
                            "LEFT JOIN WCS_TASK TASK  ON DETAIL.TASK_ID=TASK.TASK_ID " +
                            "LEFT JOIN WMS_BILL_MASTER BMASTER ON TASK.BILL_NO=BMASTER.BILL_NO "+
                            "LEFT JOIN WMS_FORMULA_DETAIL FDETAIL ON BMASTER.FORMULA_CODE=FDETAIL.FORMULA_CODE AND FDETAIL.PRODUCT_CODE=TASK.PRODUCT_CODE "+
                            "LEFT JOIN SYS_STATION on SYS_STATION.STATION_TYPE=TASK.TASK_TYPE  and SYS_STATION.CRANE_NO=DETAIL.CRANE_NO and SYS_STATION.ITEM=DETAIL.ITEM_NO " +
                            "LEFT JOIN CMD_CELL CMD_CELL_NEW on CMD_CELL_NEW.CELL_CODE=TASK.NEWCELL_CODE " +
                            "LEFT JOIN CMD_SHELF CMD_SHELF_NEW on CMD_CELL_NEW.SHELF_CODE=CMD_SHELF_NEW.SHELF_CODE " +
                            "LEFT JOIN SYS_STATION SYS_STATION_NEW ON   SYS_STATION_NEW.STATION_TYPE='14' and SYS_STATION_NEW.ITEM=3  and SYS_STATION_NEW.CRANE_NO=CMD_SHELF_NEW.CRANE_NO  " +
                            "WHERE " + where +
                            "ORDER BY TASK.TASK_LEVEL,TASK.TASK_DATE,TASK.BILL_NO, TASK.IS_MIX,FDETAIL.FORDER";

            return ExecuteQuery(strSQL).Tables[0];
        }

        /// <summary>
        /// 根据Task获取出库信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable CraneTaskOut(string strWhere)
        {
            string where = strWhere;
            if (strWhere == "")
                where = "1=1";
            string strSQL = "SELECT TASK.TASK_ID,'' AS TASK_NO,SYS_TASK_ROUTE.ITEM_NO，''AS  ASSIGNMENT_ID,CMD_SHELF.CRANE_NO, '30'||TASK.CELL_CODE||'01' AS CELLSTATION,SYS_STATION.CRANE_POSITION CRANESTATION  ,'0' AS STATE,TASK.BILL_NO," +
                           "TASK.PRODUCT_CODE,TASK.CELL_CODE,TASK.TASK_TYPE,TASK.TASK_LEVEL,TASK.TASK_DATE,TASK.IS_MIX,SYS_STATION.SERVICE_NAME,SYS_STATION.ITEM_NAME_1," +
                           "SYS_STATION.ITEM_NAME_2,TASK.PRODUCT_BARCODE,TASK.PALLET_CODE,'' AS SQUENCE_NO,TASK.TARGET_CODE,SYS_STATION.STATION_NO,SYS_STATION.MEMO,TASK.PRODUCT_TYPE,CMD_SHELF_NEW.CRANE_NO AS NEW_CRANE_NO,TASK.NEWCELL_CODE, " +
                           "DECODE(TASK.NEWCELL_CODE,NULL,'',  '30'||TASK.NEWCELL_CODE||'01') AS NEW_TO_STATION,SYS_STATION_NEW.STATION_NO AS NEW_TARGET_CODE,TASK.FORDER,TASK.FORDERBILLNO,'' AS ERR_CODE " +
                           "FROM WCS_TASK TASK " +
                           "LEFT JOIN WMS_BILL_MASTER BMASTER ON TASK.BILL_NO=BMASTER.BILL_NO " +
                           "LEFT JOIN WMS_FORMULA_DETAIL FDETAIL ON BMASTER.FORMULA_CODE=FDETAIL.FORMULA_CODE AND FDETAIL.PRODUCT_CODE=TASK.PRODUCT_CODE " +
                           "LEFT JOIN CMD_CELL ON CMD_CELL.CELL_CODE=TASK.CELL_CODE " +
                           "LEFT JOIN CMD_SHELF ON CMD_CELL.SHELF_CODE=CMD_SHELF.SHELF_CODE " +
                           "LEFT JOIN SYS_TASK_ROUTE ON SYS_TASK_ROUTE.TASK_TYPE=TASK.TASK_TYPE and SYS_TASK_ROUTE.ITEM_NO=1 " +
                           "LEFT JOIN CMD_CELL CMD_CELL_NEW ON CMD_CELL_NEW.CELL_CODE=TASK.NEWCELL_CODE " +
                           "LEFT JOIN CMD_SHELF CMD_SHELF_NEW ON CMD_CELL_NEW.SHELF_CODE=CMD_SHELF_NEW.SHELF_CODE " +
                           "LEFT JOIN SYS_STATION SYS_STATION ON SYS_STATION.STATION_TYPE=TASK.TASK_TYPE AND SYS_STATION.CRANE_NO=CMD_SHELF.CRANE_NO AND SYS_STATION.ITEM=SYS_TASK_ROUTE.ITEM_NO " +
                           "LEFT JOIN SYS_STATION SYS_STATION_NEW ON SYS_STATION_NEW.STATION_TYPE='14' AND SYS_STATION_NEW.ITEM=1  and SYS_STATION_NEW.CRANE_NO=CMD_SHELF_NEW.CRANE_NO  " +
                           "WHERE  " + where;

            //按制丝线交叉排序任务
            //string strSQL = "SELECT TASK.TASK_ID,'' AS TASK_NO,SYS_TASK_ROUTE.ITEM_NO，''AS  ASSIGNMENT_ID,CMD_SHELF.CRANE_NO, '30'||TASK.CELL_CODE||'01' AS CELLSTATION,SYS_STATION.CRANE_POSITION CRANESTATION  ,'0' AS STATE,TASK.BILL_NO," +
            //               "TASK.PRODUCT_CODE,TASK.CELL_CODE,TASK.TASK_TYPE,TASK.TASK_LEVEL,TASK.TASK_DATE,TASK.IS_MIX,SYS_STATION.SERVICE_NAME,SYS_STATION.ITEM_NAME_1," +
            //               "SYS_STATION.ITEM_NAME_2,TASK.PRODUCT_BARCODE,TASK.PALLET_CODE,'' AS SQUENCE_NO,TASK.TARGET_CODE,SYS_STATION.STATION_NO,SYS_STATION.MEMO,TASK.PRODUCT_TYPE,CMD_SHELF_NEW.CRANE_NO AS NEW_CRANE_NO,TASK.NEWCELL_CODE, " +
            //               "DECODE(TASK.NEWCELL_CODE,NULL,'',  '30'||TASK.NEWCELL_CODE||'01') AS NEW_TO_STATION,SYS_STATION_NEW.STATION_NO AS NEW_TARGET_CODE,TASK.FORDER,TASK.FORDERBILLNO,'' AS ERR_CODE,TASK.NEWID " +
            //               "FROM (SELECT WCS_TASK.*,ROW_NUMBER() OVER(PARTITION BY TARGET_CODE ORDER BY TARGET_CODE) NEWID FROM WCS_TASK) TASK " +
            //               "LEFT JOIN WMS_BILL_MASTER BMASTER ON TASK.BILL_NO=BMASTER.BILL_NO " +
            //               "LEFT JOIN WMS_FORMULA_DETAIL FDETAIL ON BMASTER.FORMULA_CODE=FDETAIL.FORMULA_CODE AND FDETAIL.PRODUCT_CODE=TASK.PRODUCT_CODE " +
            //               "LEFT JOIN CMD_CELL ON CMD_CELL.CELL_CODE=TASK.CELL_CODE " +
            //               "LEFT JOIN CMD_SHELF ON CMD_CELL.SHELF_CODE=CMD_SHELF.SHELF_CODE " +
            //               "LEFT JOIN SYS_TASK_ROUTE ON SYS_TASK_ROUTE.TASK_TYPE=TASK.TASK_TYPE and SYS_TASK_ROUTE.ITEM_NO=1 " +
            //               "LEFT JOIN CMD_CELL CMD_CELL_NEW ON CMD_CELL_NEW.CELL_CODE=TASK.NEWCELL_CODE " +
            //               "LEFT JOIN CMD_SHELF CMD_SHELF_NEW ON CMD_CELL_NEW.SHELF_CODE=CMD_SHELF_NEW.SHELF_CODE " +
            //               "LEFT JOIN SYS_STATION SYS_STATION ON SYS_STATION.STATION_TYPE=TASK.TASK_TYPE AND SYS_STATION.CRANE_NO=CMD_SHELF.CRANE_NO AND SYS_STATION.ITEM=SYS_TASK_ROUTE.ITEM_NO " +
            //               "LEFT JOIN SYS_STATION SYS_STATION_NEW ON SYS_STATION_NEW.STATION_TYPE='14' AND SYS_STATION_NEW.ITEM=1  and SYS_STATION_NEW.CRANE_NO=CMD_SHELF_NEW.CRANE_NO  " +
            //               "WHERE  " + where +
            //               "ORDER BY TASK.NEWID,TASK.TASK_DATE,TASK.BILL_NO,TASK.FORDER,TASK.PRODUCT_CODE,TASK.TASK_ID";
            return ExecuteQuery(strSQL).Tables[0];
        }

        /// <summary>
        /// 根据Task获取入库，起始位置，目标位置，及堆垛机编号
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable TaskInCraneStation(string strWhere)
        {
            string where = strWhere;
            if (strWhere == "")
                where = "1=1";
            string strSQL = "SELECT '30'||TASK.cell_code||'01' AS CELLSTATION,SYS_STATION.CRANE_POSITION AS CRANESTATION,CMD_SHELF.CRANE_NO FROM WCS_TASK TASK " +
                           "LEFT JOIN CMD_CELL on CMD_CELL.CELL_CODE=TASK.CELL_CODE " +
                           "LEFT JOIN CMD_SHELF on CMD_CELL.SHELF_CODE=CMD_SHELF.SHELF_CODE " +
                           "LEFT JOIN SYS_STATION SYS_STATION on SYS_STATION.STATION_TYPE=TASK.TASK_TYPE and SYS_STATION.CRANE_NO=CMD_SHELF.CRANE_NO " +
                           "WHERE  " + where;
            return ExecuteQuery(strSQL).Tables[0];
        }

        private string GetTaskDetailNo(string TaskID)
        {
            string strValue = "";
            string strSQL = string.Format("SELECT TASK_ID,TASK.BILL_NO,BILL.BTYPE_CODE,BTYPE.BILL_TYPE||BTYPE.TARGET_CODE AS TASKNOTYPE " + 
                                          "FROM WCS_TASK TASK " + 
                                          "LEFT JOIN WMS_BILL_MASTER BILL ON BILL.BILL_NO=TASK.BILL_NO " +
                                          "LEFT JOIN CMD_BILL_TYPE BTYPE ON BTYPE.BTYPE_CODE=BILL.BTYPE_CODE " +
                                          "WHERE TASK_ID='{0}'", TaskID);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            string mode = "";
            if (dt.Rows.Count > 0)
            {
                string BType = dt.Rows[0]["TASKNOTYPE"].ToString();
                switch (BType)
                {
                    case "2195"://紧急补料单  9000-9299
                        mode = "F";
                        break;
                    case "3195"://抽检        9300-9499 
                        mode = "R";
                        break;
                    case "2122"://倒库        9500-9799
                        mode = "B";
                        break;
                    case "4195": //盘点单     9800--9998
                        mode = "C";
                        break;
                    default:
                        mode = "M";
                        break;

                }
            }
            SysStationDao SysDao = new SysStationDao();
            strValue = SysDao.GetTaskNo(mode);
                
            return strValue;
        }

        
        /// <summary>
        /// 更新任务状态Task
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="state"></param>
        public void UpdateTaskState(string TaskID, string state)
        {
            string strSql;
            if(state=="2")
                strSql = string.Format("UPDATE WCS_TASK SET STATE='{0}',FINISH_DATE=SYSDATE WHERE TASK_ID='{1}'", state, TaskID);
            else
                strSql = string.Format("UPDATE WCS_TASK SET STATE='{0}' WHERE TASK_ID='{1}'", state, TaskID);
            ExecuteNonQuery(strSql);
        }
        /// <summary>
        /// 更新任务状态Task
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="state"></param>
        public void UpdateTaskState(string TaskID, string state,string Flag)
        {
            string strSql;
            if (state == "2")
                strSql = string.Format("UPDATE WCS_TASK SET STATE='{0}',FINISH_DATE=SYSDATE,RFIDERR_HANDLE_FLAG='{2}' WHERE TASK_ID='{1}'", state, TaskID, Flag);
            else
                strSql = string.Format("UPDATE WCS_TASK SET STATE='{0}',RFIDERR_HANDLE_FLAG='{2}' WHERE TASK_ID='{1}'", state, TaskID, Flag);
            ExecuteNonQuery(strSql);
        }
        public bool UpdateTaskState(string TaskID, int state)
        {
            string   strSql = string.Format("UPDATE WCS_TASK SET STATE='{0}' WHERE TASK_ID='{1}'", state, TaskID);
            return  ExecuteNonQuery(strSql) > 0;
        }
        
        /// <summary>
        /// 更新任务状态Task
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="state"></param>
        public void UpdateTaskHState(string TaskID, string state)
        {
            string strSql;
            if (state == "2")
                strSql = string.Format("UPDATE WCS_TASKH SET STATE='{0}',FINISH_DATE=SYSDATE WHERE TASK_ID='{1}'", state, TaskID);
            else
                strSql = string.Format("UPDATE WCS_TASKH SET STATE='{0}' WHERE TASK_ID='{1}'", state, TaskID);
            ExecuteNonQuery(strSql);
        }



        /// <summary>
        /// 更新堆垛机顺序号。
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="Squenceno"></param>
        public void UpdateCraneQuenceNo(string TaskID, string Squenceno, string ItemNo,string psCrnNo)
        {
            string strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET  CRANE_NO='{3}',SQUENCE_NO='{0}',ERR_CODE=''  WHERE TASK_ID='{1}' AND ITEM_NO='{2}'", Squenceno, TaskID, ItemNo, psCrnNo);
            ExecuteNonQuery(strSQL);
        }

        /// <summary>
        /// 更新堆垛机顺序号。
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="Squenceno"></param>
        public void UpdateCraneQuenceNo(string TaskID, string Squenceno,string ItemNo)
        {
            string strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET SQUENCE_NO='{0}'  WHERE TASK_ID='{1}' AND ITEM_NO='{2}'", Squenceno, TaskID,ItemNo);
            ExecuteNonQuery(strSQL);
        }
        /// <summary>
        /// 更新堆垛机顺序号。
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="Squenceno"></param>
        public void UpdateCraneQuenceNo(string TaskID, string ItemNo, string CraneNo, string Squenceno, string FromStation, string ToStation)
        {
            string strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET CRANE_NO='{0}',SQUENCE_NO='{1}',FROM_STATION='{2}',TO_STATION='{3}' " +
                                          "WHERE TASK_ID='{4}' AND ITEM_NO='{5}'", CraneNo, Squenceno, FromStation, ToStation,TaskID, ItemNo);
            ExecuteNonQuery(strSQL);
        }
        /// <summary>
        /// 更新堆垛机错误编号。
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="Squenceno"></param>
        public void UpdateCraneErrCode(string TaskID, string ItemNo,string ErrCode)
        {
            string strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET ERR_CODE='{0}' WHERE TASK_ID='{1}' AND ITEM_NO='{2}'", ErrCode, TaskID, ItemNo);
            ExecuteNonQuery(strSQL);            
        }
        /// <summary>
        /// 更新堆垛机错误编号。
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="Squenceno"></param>
        public void UpdateCraneReturnCode(string CraneNo, string ErrCode)
        {
            string strSQL = string.Format("UPDATE CMD_CRANE SET ERR_CODE='{0}' WHERE CRANE_NO ='{1}'", ErrCode, CraneNo);
            ExecuteNonQuery(strSQL);
        }
        /// <summary>
        /// 更新堆垛机错误编号。
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="Squenceno"></param>
        public void UpdateDetailCraneErrCode(string CraneNo,string AssignmentID, string ErrCode)
        {
            string strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET ERR_CODE='{0}' WHERE ASSIGNMENT_ID='{1}' AND CRANE_NO='{2}'", ErrCode, AssignmentID, CraneNo);
            ExecuteNonQuery(strSQL);
        }
        /// <summary>
        /// 根据条件，返回小车任务明细。
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable TaskCarDetail(string strWhere)
        {
            string where = "1=1";
            if (!string.IsNullOrEmpty(strWhere))
                where = strWhere;
            string strSQL = "SELECT WCS_TASK.TASK_ID,CMD_CELL.CELL_CODE,STATION.STATION_NO,STATION.IN_STATION,ADDRESS1.CAR_ADDRESS STATION_NO_ADDRESS," +
                            "ADDRESS2.CAR_ADDRESS IN_STATION_ADDRESS,CMD_SHELF.CRANE_NO,DETAIL.TASK_NO,WCS_TASK.TASK_TYPE,DETAIL.CAR_NO, DETAIL.ITEM_NO," +
                            "STATION.OUT_STATION_1,ADDRESS3.CAR_ADDRESS OUT_STATION_1_ADDRESS, STATION.OUT_STATION_2,ADDRESS4.CAR_ADDRESS OUT_STATION_2_ADDRESS," + 
                            "'' WRITEITEM,WCS_TASK.TARGET_CODE, WCS_TASK.IS_MIX,WCS_TASK.FORDER,WCS_TASK.FORDERBILLNO,WCS_TASK.PRODUCT_TYPE,WCS_TASK.PRODUCT_CODE," + 
                            "WCS_TASK.PRODUCT_BARCODE,WCS_TASK.PALLET_CODE,DETAIL.STATE " +
                            "FROM WCS_TASK " +
                            "LEFT JOIN CMD_CELL ON WCS_TASK.CELL_CODE=CMD_CELL.CELL_CODE " +
                            "LEFT JOIN CMD_SHELF ON CMD_CELL.SHELF_CODE=CMD_SHELF.SHELF_CODE " +
                            "LEFT JOIN SYS_CAR_STATION STATION ON CMD_SHELF.CRANE_NO=STATION.CRANE_NO AND STATION.STATION_TYPE=WCS_TASK.TASK_TYPE " +
                            "LEFT JOIN SYS_CAR_ADDRESS ADDRESS1 ON STATION.STATION_NO=ADDRESS1.STATION_NO " +
                            "LEFT JOIN SYS_CAR_ADDRESS ADDRESS2 ON STATION.IN_STATION=ADDRESS2.STATION_NO " +
                            "LEFT JOIN SYS_CAR_ADDRESS ADDRESS3 ON STATION.OUT_STATION_1=ADDRESS3.STATION_NO "+
                            "LEFT JOIN SYS_CAR_ADDRESS ADDRESS4 ON STATION.OUT_STATION_2=ADDRESS4.STATION_NO "+ 
                            "INNER JOIN WCS_TASK_DETAIL DETAIL ON WCS_TASK.TASK_ID=DETAIL.TASK_ID " +
                            "WHERE " + strWhere + " ORDER BY FORDERBILLNO,FORDER";

            return ExecuteQuery(strSQL).Tables[0];
        }

        /// <summary>
        /// 获取堆垛机最大流水号
        /// </summary>
        /// <returns></returns>
        public string GetMaxSQUENCENO()
        {
            string strValue="";
            string strSQL = "SELECT MAX(SQUENCE_NO)  FROM WCS_TASK_DETAIL ";
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (dt.Rows.Count > 0)
                strValue = dt.Rows[0][0].ToString();
            return strValue;
        }

        /// <summary>
        ///  更新任务明细状态
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="TaskType"></param>
        public void UpdateTaskDetailState(string strWhere, string State)
        {
            string where = "1=1";
            if (!string.IsNullOrEmpty(strWhere))
                where = strWhere;
            string strSQL = "";
            if(State=="1")
                strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET STATE='{0}',BEGIN_DATE=SYSDATE WHERE {1}", State, where);
            else if (State == "2" || State == "3")
                strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET STATE='{0}',FINISH_DATE=SYSDATE WHERE {1}", State, where);
            else if(State=="0")
                strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET CRANE_NO=NULL,CAR_NO=NULL,FROM_STATION=NULL,TO_STATION=NULL,STATE='{0}',BEGIN_DATE=NULL WHERE {1}", State, where);
            ExecuteNonQuery(strSQL);
        }
        /// <summary>
        ///  更新任务明细状态
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="TaskType"></param>
        public void UpdateTaskDetailHState(string strWhere, string State)
        {
            string where = "1=1";
            if (!string.IsNullOrEmpty(strWhere))
                where = strWhere;
            string strSQL = string.Format("UPDATE WCS_TASK_DETAILH SET STATE='{0}' WHERE {1}", State, where);
            ExecuteNonQuery(strSQL);
        }
        /// <summary>
        ///  更新任务明细状态
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="TaskType"></param>
        public void UpdateTaskDetail(string TaskID, string ToStation,int ItemNo)
        {
            string strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET STATE='2',TO_STATION='{0}',FINISH_DATE=SYSDATE WHERE TASK_ID='{1}' AND ITEM_NO={2}", ToStation, TaskID, ItemNo);
            ExecuteNonQuery(strSQL);
            strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET FROM_STATION='{0}' WHERE TASK_ID='{1}' AND ITEM_NO={2}", ToStation, TaskID, ItemNo+1);
            ExecuteNonQuery(strSQL);
            if (ItemNo == 6)
            {
                strSQL = string.Format("UPDATE WCS_TASK SET STATE='2',FINISH_DATE=SYSDATE WHERE TASK_ID='{0}'", TaskID);
                ExecuteNonQuery(strSQL);
            }
        }
        /// <summary>
        /// 更新起始位置，目标位置
        /// </summary>
        /// <param name="FromStation"></param>
        /// <param name="ToStation"></param>
        /// <param name="strWhere"></param>
        public void UpdateTaskDetailStation(string FromStation, string ToStation, string State, string strWhere)
        {
            string where = "1=1";
            if (!string.IsNullOrEmpty(strWhere))
                where = strWhere;

            string strSQL = "";
            if(State=="1")
                strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET  FROM_STATION='{0}',TO_STATION='{1}',STATE='{2}',BEGIN_DATE=SYSDATE WHERE {3}", new string[] { FromStation, ToStation, State, where });
            else if (State == "2" || State == "3")
                strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET  FROM_STATION='{0}',TO_STATION='{1}',STATE='{2}',FINISH_DATE=SYSDATE WHERE {3}", new string[] { FromStation, ToStation, State, where });
            ExecuteNonQuery(strSQL);
        }
        /// <summary>
        /// 给小车安排任务，更新任务明细表小车编号，起始位置，结束位置
        /// </summary>
        /// <param name="CarNo"></param>
        public void UpdateTaskDetailCar(string state, string strWhere)
        {
            string where = "1=1";
            if (!string.IsNullOrEmpty(strWhere))
                where = strWhere;

            string strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET STATE='{0}' WHERE {1}", state, where);
            ExecuteNonQuery(strSQL);

        }
        /// <summary>
        /// 给小车安排任务，更新任务明细表小车编号，起始位置，结束位置
        /// </summary>
        /// <param name="CarNo"></param>
        public void UpdateTaskDetailCar(string FromStation, string ToStation, string state, string CarNo,string strWhere)
        {
            string where = "1=1";
            if (!string.IsNullOrEmpty(strWhere))
                where = strWhere;

            string strSQL = "";
            if (state == "1")
                strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET FROM_STATION='{0}',TO_STATION='{1}',STATE='{2}',CAR_NO='{3}',BEGIN_DATE=SYSDATE WHERE {4}", new string[] { FromStation, ToStation, state, CarNo, where });
            else if (state == "2" || state == "3")
                strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET FROM_STATION='{0}',TO_STATION='{1}',STATE='{2}',CAR_NO='{3}',FINISH_DATE=SYSDATE WHERE {4}", new string[] { FromStation, ToStation, state, CarNo, where });
            else
                strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET FROM_STATION='{0}',TO_STATION='{1}',STATE='{2}',CAR_NO='{3}',BEGIN_DATE=NULL,FINISH_DATE=NULL WHERE {4}", new string[] { FromStation, ToStation, state, CarNo, where });
            ExecuteNonQuery(strSQL);

        }
        /// <summary>
        /// 给小车安排任务，更新任务明细表小车编号，起始位置，结束位置
        /// </summary>
        /// <param name="CarNo"></param>
        public void UpdateTaskDetailCrane(string FromStation, string ToStation, string state, string CraneNo, string strWhere)
        {
            string where = "1=1";
            if (!string.IsNullOrEmpty(strWhere))
                where = strWhere;

            string strSQL = "";
            if (state == "0" || state == "1")
                strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET FROM_STATION='{0}',TO_STATION='{1}',STATE='{2}',CRANE_NO='{3}',BEGIN_DATE=SYSDATE WHERE {4}", new string[] { FromStation, ToStation, state, CraneNo, where });
            else if (state == "2" || state == "3")
                strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET FROM_STATION='{0}',TO_STATION='{1}',STATE='{2}',CRANE_NO='{3}',FINISH_DATE=SYSDATE WHERE {4}", new string[] { FromStation, ToStation, state, CraneNo, where });
            
            //string strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET FROM_STATION='{0}',TO_STATION='{1}',STATE='{2}',CRANE_NO='{3}' WHERE {4}", new string[] { FromStation, ToStation, state, CraneNo, where });
            //strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET FROM_STATION='{0}',TO_STATION='{1}',CRANE_NO='{2}' WHERE {3}", new string[] { FromStation, ToStation, CraneNo, where });
            ExecuteNonQuery(strSQL);
        }

        /// <summary>
        /// 分配货位,返回 0:TaskID，1:货位 
        /// </summary>
        /// <param name="strWhere"></param>
        public string[] AssignCell(string strWhere, string ApplyStation)
        {            
            string where = "1=1";
            if (!string.IsNullOrEmpty(strWhere))
                where = strWhere;
            string strSQL = "SELECT * FROM WCS_TASK WHERE " + where;
            strSQL = "SELECT A.*,NVL(B.TASK_NO,'') TASK_NO,NVL(B.STATE,'0') TASK_STATE FROM WCS_TASK A LEFT JOIN WCS_TASK_DETAIL B ON A.TASK_ID=B.TASK_ID AND B.ITEM_NO=2 WHERE " + where;
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            
            if (dt.Rows.Count == 0)
            {
                throw new Exception("此条码无入库任务或条码申请重复。" + strWhere);
            }
            string TaskState = dt.Rows[0]["STATE"].ToString().Trim();
            string DetailState = dt.Rows[0]["TASK_STATE"].ToString().Trim();
            string VCell = dt.Rows[0]["CELL_CODE"].ToString().Trim();
            string TaskNo = dt.Rows[0]["TASK_NO"].ToString().Trim();
            string TaskID = dt.Rows[0]["TASK_ID"].ToString().Trim();            
            string billNo = dt.Rows[0]["BILL_NO"].ToString().Trim();
            string ProductCode = dt.Rows[0]["PRODUCT_CODE"].ToString().Trim();

            if (TaskState == "0" || (TaskState == "1" && DetailState == "0"))
            {
                if (VCell == "")
                {
                    StoredProcParameter parameters = new StoredProcParameter();
                    if (ApplyStation == "131" && ProductCode == "0000")
                        parameters.AddParameter("VBILLNO", "131");
                    else
                        parameters.AddParameter("VBILLNO", billNo);
                    parameters.AddParameter("VPRODUCTCODE", ProductCode);
                    parameters.AddParameter("VCELL", "00000000", DbType.String, ParameterDirection.Output);

                    if (ProductCode == "0000")
                        ExecuteNonQuery("APPLYPALLETSCELL", parameters);
                    else
                        ExecuteNonQuery("APPLYCELL", parameters);
                    VCell = parameters["VCELL"].ToString();
                }
                if (VCell == "")
                {
                    throw new Exception("没有可分配的货位！");
                }
            }
            else
            {
                throw new Exception("此条码已经执行入库，请核对!" + strWhere);
            }
            
            if (ProductCode == "0000")
                strSQL = string.Format("UPDATE CMD_CELL SET IS_LOCK='1',PRODUCT_CODE='{1}',CELL_STATE='1' WHERE CELL_CODE='{0}'", VCell, ProductCode);
            else
                strSQL = string.Format("UPDATE CMD_CELL SET IS_LOCK='1',BILL_NO='{1}',CELL_STATE='1' WHERE CELL_CODE='{0}'", VCell, billNo);
            ExecuteNonQuery(strSQL);

            strSQL = string.Format("UPDATE WCS_TASK A SET A.CELL_CODE='{0}' WHERE {1}", VCell, where);
            ExecuteNonQuery(strSQL);

            string[] strValue = new string[3];
            strValue[0] = TaskID;
            strValue[1] = VCell;
            strValue[2] = TaskNo;
         
            return strValue;
        }
        /// <summary>
        /// 分配货位,返回 0:TaskID，1:货位 
        /// </summary>
        /// <param name="strWhere"></param>
        public string[] ManualAssignCell(string strWhere, string ApplyStation,string VCell)
        {
            string where = "1=1";
            if (!string.IsNullOrEmpty(strWhere))
                where = strWhere;
            string strSQL = "SELECT * FROM WCS_TASK WHERE " + where;
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (dt.Rows.Count == 0)
            {
                throw new Exception("找不到相关的入库单号。");
            }
            string TaskID = dt.Rows[0]["TASK_ID"].ToString();

            string billNo = dt.Rows[0]["BILL_NO"].ToString();
            string ProductCode = dt.Rows[0]["PRODUCT_CODE"].ToString();

            if (ProductCode == "0000")
                strSQL = string.Format("UPDATE CMD_CELL SET IS_LOCK='1',PRODUCT_CODE='{1}',CELL_STATE='1' WHERE CELL_CODE='{0}'", VCell, ProductCode);
            else
                strSQL = string.Format("UPDATE CMD_CELL SET IS_LOCK='1',BILL_NO='{1}',CELL_STATE='1' WHERE CELL_CODE='{0}'", VCell, billNo);
            ExecuteNonQuery(strSQL);

            strSQL = string.Format("UPDATE WCS_TASK SET CELL_CODE='{0}' WHERE {1}", VCell, where);
            ExecuteNonQuery(strSQL);

            string[] strValue = new string[2];
            strValue[0] = TaskID;
            strValue[1] = VCell;

            return strValue;
        }
        public string[] GetTaskOutInfo(string TaskNo)
        {
            string strSQL = string.Format("SELECT DISTINCT TASK.TASK_ID,TASK.BILL_NO FROM WCS_TASK_DETAIL DETAIL " +
                                          "LEFT JOIN WCS_TASK TASK ON DETAIL.TASK_ID=TASK.TASK_ID " +
                                          "WHERE DETAIL.TASK_NO='{0}' AND TASK.STATE IN('0','1') AND TASK.TASK_TYPE IN('12','13','22')", TaskNo);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            string[] str = new string[2];
            if (dt.Rows.Count > 0)
            {
                str[0] = dt.Rows[0]["TASK_ID"].ToString();
                str[1] = dt.Rows[0]["BILL_NO"].ToString();
            }
            return str;
        }
        public string[] GetTaskInfo(string TaskNo)
        {
            string strSQL = string.Format("SELECT DISTINCT TASK.TASK_ID,TASK.BILL_NO FROM WCS_TASK_DETAIL DETAIL " +
                                          "LEFT JOIN WCS_TASK TASK ON DETAIL.TASK_ID=TASK.TASK_ID " +
                                          "WHERE DETAIL.TASK_NO='{0}' AND TASK.STATE IN('0','1') AND TASK.TASK_TYPE IN('11','13','14','21')", TaskNo);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            string[] str = new string[2];
            if (dt.Rows.Count > 0)
            {
                str[0] = dt.Rows[0]["TASK_ID"].ToString();
                str[1] = dt.Rows[0]["BILL_NO"].ToString();
            }
            return str;
        }
        public string GetTaskType(string TaskNo)
        {
            string strSQL = string.Format("SELECT DISTINCT TASK.TASK_TYPE FROM WCS_TASK_DETAIL DETAIL " +
                                          "LEFT JOIN WCS_TASK TASK ON DETAIL.TASK_ID=TASK.TASK_ID " +
                                          "WHERE DETAIL.TASK_NO='{0}' AND DETAIL.STATE IN('0','1')", TaskNo);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];

            if (dt.Rows.Count > 0)
                return dt.Rows[0]["TASK_TYPE"].ToString();
            else
                return "";
        }
        public string GetOutLineNo(string BillNo)
        {
            string strSQL = string.Format("SELECT LINE_NO FROM VIEW_BILL_MAST DETAIL WHERE BILL_NO='{0}' ", BillNo);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];

            if (dt.Rows.Count > 0)
                return dt.Rows[0]["LINE_NO"].ToString();
            else
                return "";
        }
        /// <summary>
        ///  分配货位,返回 0:TaskID，1:货位 
        /// </summary>
        /// <param name="strWhere"></param>
        public string[] AssignCellTwo(string strWhere) //
        {
            string where = "1=1";
            if (!string.IsNullOrEmpty(strWhere))
                where = strWhere;
            string strSQL = "SELECT * FROM WCS_TASK WHERE " + where;
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            string TaskID = dt.Rows[0]["TASK_ID"].ToString();

            string billNo = dt.Rows[0]["BILL_NO"].ToString();
            string ProductCode = dt.Rows[0]["PRODUCT_CODE"].ToString();
            string VCell = "";
            if (dt.Rows[0]["CELL_CODE"].ToString() == "")
            {
                StoredProcParameter parameters = new StoredProcParameter();

                parameters.AddParameter("VBILLNO", billNo);
                parameters.AddParameter("VPRODUCTCODE", ProductCode);
                parameters.AddParameter("VCELL", "00000000", DbType.String, ParameterDirection.Output);
                ExecuteNonQuery("APPLYCELL", parameters);
                VCell = parameters["VCELL"].ToString();
            }
            else
            {
                VCell = dt.Rows[0]["CELL_CODE"].ToString();
            }

            if (VCell == "")
            {
                throw new Exception("没有可分配的货位！");
            }

            strSQL = string.Format("UPDATE CMD_CELL SET IS_LOCK='1',BILL_NO='{1}',CELL_STATE='1' WHERE CELL_CODE='{0}'", VCell, billNo);
            ExecuteNonQuery(strSQL);

            strSQL = string.Format("UPDATE WCS_TASK SET CELL_CODE='{0}' WHERE {1}", VCell, where);
            ExecuteNonQuery(strSQL);

            string[] strValue = new string[2];
            strValue[0] = TaskID;
            strValue[1] = VCell;
            return strValue;
        }
        /// <summary>
        /// 返回任务信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable TaskInfo(string strWhere)
        {
            string where = "1=1";
            if (!string.IsNullOrEmpty(strWhere))
                where = strWhere;
            string strSQL = "SELECT TASK.*,MASTER.BTYPE_CODE,MASTER.BILL_METHOD FROM WCS_TASK TASK LEFT JOIN WMS_BILL_MASTER MASTER ON TASK.BILL_NO=MASTER.BILL_NO WHERE " + where;
            return ExecuteQuery(strSQL).Tables[0];
        }
        /// <summary>
        /// 返回任务信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable TaskInfo(string TaskID,int ItemNo)
        {
            string strSQL = string.Format("SELECT TASK.SOURCE_BILLNO,DETAIL.*,TO_CHAR(DETAIL.BEGIN_DATE,'yyyy/MM/dd HH:mm:ss') BEGINDATE,TO_CHAR(DETAIL.FINISH_DATE,'yyyy/MM/dd HH:mm:ss') FINISHDATE FROM WCS_TASK_DETAIL DETAIL INNER JOIN WCS_TASK TASK ON DETAIL.TASK_ID=TASK.TASK_ID WHERE DETAIL.TASK_ID='{0}' AND DETAIL.ITEM_NO={1}", TaskID, ItemNo);
            return ExecuteQuery(strSQL).Tables[0];
        }
        /// <summary>
        /// 返回任务信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetFeedingTaskInfo(string TaskID)
        {
            string strSQL = string.Format("SELECT * FROM VIEW_TASK_LINE WHERE TASK_ID='{0}'", TaskID);
            return ExecuteQuery(strSQL).Tables[0];
        }
        /// <summary>
        /// 返回任务信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetLeastFeedingTask(string ItemName)
        {
            string strSQL = string.Format("SELECT * FROM VIEW_FEEDTASK WHERE VIEW_FEEDTASK.READ_ITEM='{0}'", ItemName);
            return ExecuteQuery(strSQL).Tables[0];
        }
        /// <summary>
        /// 返回任务信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetLeastFeedingTask(string FOrderBillNo,string LineNo)
        {
            string strSQL = string.Format("SELECT * FROM VIEW_FEEDTASK WHERE VIEW_FEEDTASK.LINE_NO='{0}'", LineNo);
            return ExecuteQuery(strSQL).Tables[0];
        }
        /// <summary>
        /// 返回任务信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int GetTaskOrderNo(string TaskID)
        {
            string BillNo = TaskID.Substring(0, TaskID.Length - 2);
            int OrderNo = 0;
            int TaskCount = 0;
            string strSQL = string.Format("SELECT DECODE(MAX(ORDER_NO),NULL,0,MAX(ORDER_NO)),COUNT(*) FROM VIEW_WCS_TASK WHERE BILL_NO='{0}'", BillNo);
            strSQL = string.Format("SELECT ORDER_NO,AMOUNT FROM VIEW_WCS_TASK WHERE TASK_ID='{0}'", TaskID);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];

            if (dt.Rows.Count > 0)
            {
                int.TryParse(dt.Rows[0][0].ToString(), out OrderNo);
                int.TryParse(dt.Rows[0][1].ToString(), out TaskCount);
                //OrderNo = int.Parse(dt.Rows[0][0].ToString());
                //TaskCount = int.Parse(dt.Rows[0][1].ToString());

                if (OrderNo == 1 && OrderNo < TaskCount)
                    return 1;
                else if (OrderNo == TaskCount)
                    return 2;
                else
                    return 0;
            }

            return OrderNo;
        }
        /// <summary>
        /// 根据单号，返回任务数量
        /// </summary>
        /// <param name="BillNo"></param>
        /// <returns></returns>
        public int GetTaskCount(string BillNo)
        {
            string strSQL = string.Format("SELECT COUNT(*) FROM WCS_TASK WHERE BILL_NO='{0}'", BillNo);
            DataTable dt=ExecuteQuery(strSQL).Tables[0];
            return int.Parse(dt.Rows[0][0].ToString());
        }

        /// <summary>
        /// 根据任务号，返回产品信息。
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public DataTable GetProductInfoByTaskID(string TaskID)
        {
            string strSQL = string.Format("SELECT T.CELL_CODE,T.PRODUCT_CODE,T.PRODUCT_BARCODE,T.BILL_NO,P.*,M.CIGARETTE_CODE,C.CIGARETTE_NAME,M.FORMULA_CODE,F.FORMULA_NAME,G.GRADE_NAME,O.ORIGINAL_NAME,S.STYLE_NAME,CMD_CELL.BILL_NO INBILL_NO " + 
                            "FROM WCS_TASK T " + 
                            "LEFT JOIN CMD_PRODUCT P ON T.PRODUCT_CODE=P.PRODUCT_CODE " +
                            "LEFT JOIN WMS_BILL_MASTER M ON T.BILL_NO=M.BILL_NO " +
                            "LEFT JOIN CMD_CIGARETTE C ON C.CIGARETTE_CODE=M.CIGARETTE_CODE " +
                            "LEFT JOIN WMS_FORMULA_MASTER F ON F.FORMULA_CODE=M.FORMULA_CODE " +
                            "LEFT JOIN CMD_PRODUCT_ORIGINAL O ON O.ORIGINAL_CODE=P.ORIGINAL_CODE " +
                            "LEFT JOIN CMD_PRODUCT_GRADE G ON G.GRADE_CODE=P.GRADE_CODE " +
                            "LEFT JOIN CMD_PRODUCT_STYLE S ON S.STYLE_NO=P.STYLE_NO " +
                            "LEFT JOIN CMD_CELL ON T.CELL_CODE=CMD_CELL.CELL_CODE " +
                            "WHERE TASK_ID='{0}'", TaskID);
            return ExecuteQuery(strSQL).Tables[0];
        }

        /// <summary>
        /// 根据任务号，返回产品信息。
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public DataTable GetCheckInfoByTaskID(string TaskID)
        {
            string strSQL = string.Format("SELECT T.CELL_CODE,T.PRODUCT_CODE,T.PRODUCT_BARCODE,T.BILL_NO,P.*,M.CIGARETTE_CODE,C.CIGARETTE_NAME,M.FORMULA_CODE,F.FORMULA_NAME,G.GRADE_NAME,O.ORIGINAL_NAME,S.STYLE_NAME,CMD_CELL.BILL_NO INBILL_NO " +
                            "FROM WCS_TASK T " +
                            "LEFT JOIN CMD_PRODUCT P ON T.PRODUCT_CODE=P.PRODUCT_CODE " +
                            "LEFT JOIN VIEW_BILL_MAST M ON T.SOURCE_BILLNO=M.BILL_NO " +
                            "LEFT JOIN CMD_CIGARETTE C ON C.CIGARETTE_CODE=M.CIGARETTE_CODE " +
                            "LEFT JOIN WMS_FORMULA_MASTER F ON F.FORMULA_CODE=M.FORMULA_CODE " +
                            "LEFT JOIN CMD_PRODUCT_ORIGINAL O ON O.ORIGINAL_CODE=P.ORIGINAL_CODE " +
                            "LEFT JOIN CMD_PRODUCT_GRADE G ON G.GRADE_CODE=P.GRADE_CODE " +
                            "LEFT JOIN CMD_PRODUCT_STYLE S ON S.STYLE_NO=P.STYLE_NO " +
                            "LEFT JOIN CMD_CELL ON T.CELL_CODE=CMD_CELL.CELL_CODE " +
                            "WHERE TASK_ID='{0}'", TaskID);
            return ExecuteQuery(strSQL).Tables[0];
        }
        /// <summary>
        /// 二楼出库--条码校验出错，记录错误标志，及新条码。
        /// </summary>
        public void UpdateTaskCheckBarCode(string TaskID, string BarCode)
        {
            string strSQL = string.Format("UPDATE WCS_TASK SET BARCODE_CHECK='1', CHECK_PRODUCT_BARCODE='{1}' WHERE TASK_ID='{0}'", TaskID, BarCode);
            ExecuteNonQuery(strSQL);
        }
        /// <summary>
        /// 二楼出库--条码校验出错，记录错误标志，及新条码。
        /// </summary>
        public void UpdateTaskCheckRFID(string TaskID, string PalletCode)
        {
            string strSQL = string.Format("UPDATE WCS_TASK SET RFID_CHECK='1', CHECK_PALLET_CODE='{1}' WHERE TASK_ID='{0}'", TaskID, PalletCode);
            ExecuteNonQuery(strSQL);
        }
        /// <summary>
        /// 分配货位,返回 0:TaskID，1:任务号，2:货物到达入库站台的目的地址--平面号,3:堆垛机入库站台，4:货位，5:堆垛机编号
        /// </summary>
        /// <param name="strWhere"></param>
        public string AssignNewCell(string strWhere, string CraneNo)
        {
            string where = "1=1";
            if (!string.IsNullOrEmpty(strWhere))
                where = strWhere;
            string strSQL = "SELECT * FROM WCS_TASK WHERE " + where;
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (dt.Rows.Count == 0)
            {
                throw new Exception("找不到当前任务。");
            }

            string billNo = dt.Rows[0]["BILL_NO"].ToString();
            string ProductCode = dt.Rows[0]["PRODUCT_CODE"].ToString();
            string VCell = "";

            StoredProcParameter parameters = new StoredProcParameter();
            parameters.AddParameter("VPRODUCTCODE", ProductCode);
            parameters.AddParameter("VCRANENO", CraneNo);
            parameters.AddParameter("VCELL", "00000000", DbType.String, ParameterDirection.Output);

            ExecuteNonQuery("APPLYNEWCELL", parameters);
            VCell = parameters["VCELL"].ToString();

            if (ProductCode == "0000")
                strSQL = string.Format("UPDATE CMD_CELL SET IS_LOCK='1',PRODUCT_CODE='{1}',CELL_STATE='1' WHERE CELL_CODE='{0}'", VCell, ProductCode);
            else
                strSQL = string.Format("UPDATE CMD_CELL SET IS_LOCK='1',BILL_NO='{1}',CELL_STATE='1' WHERE CELL_CODE='{0}'", VCell, billNo);
            ExecuteNonQuery(strSQL);

            strSQL = string.Format("UPDATE WCS_TASK SET CELL_CODE='{0}' WHERE {1}", VCell, where);
            ExecuteNonQuery(strSQL);

            return VCell;
        }
        ///// <summary>
        ///// 分配货位,返回 0:TaskID，1:任务号，2:货物到达入库站台的目的地址--平面号,3:堆垛机入库站台，4:货位，5:堆垛机编号
        ///// </summary>
        ///// <param name="strWhere"></param>
        //public string[] AssignNewCell(string strWhere, string CraneNo)
        //{
        //    string[] strValue = new string[6];
        //    string where = "1=1";
        //    if (!string.IsNullOrEmpty(strWhere))
        //        where = strWhere;
        //    string strSQL = "SELECT * FROM WCS_TASK WHERE " + where;
        //    DataTable dt = ExecuteQuery(strSQL).Tables[0];
        //    if (dt.Rows.Count == 0)
        //    {
        //        throw new Exception("找不到相关的入库单号。");
        //    }
        //    string TaskID = dt.Rows[0]["TASK_ID"].ToString();

        //    string billNo = dt.Rows[0]["BILL_NO"].ToString();
        //    string ProductCode = dt.Rows[0]["PRODUCT_CODE"].ToString();
        //    string VCell = "";
        //    if (dt.Rows[0]["CELL_CODE"].ToString() != "")
        //    {
        //        VCell = dt.Rows[0]["CELL_CODE"].ToString();
        //        CellDao cdao = new CellDao();
        //        DataTable dtCell = cdao.GetCellInfo(VCell);
        //        if (dtCell.Rows[0]["ERROR_FLAG"].ToString() == "1")
        //        {
        //            VCell = "";
        //        }
        //    }
        //    if (VCell == "")
        //    {
        //        StoredProcParameter parameters = new StoredProcParameter();
        //        parameters.AddParameter("VPRODUCTCODE",ProductCode);
        //        parameters.AddParameter("VCRANENO", CraneNo);
        //        parameters.AddParameter("VCELL", "00000000", DbType.String, ParameterDirection.Output);

        //        ExecuteNonQuery("APPLYNEWCELL", parameters);
        //        VCell = parameters["VCELL"].ToString();
               
        //    }
        //    if (VCell == "")
        //    {
        //        throw new Exception("没有可分配的货位！");
        //    }
        //    strSQL = string.Format("UPDATE CMD_CELL SET IS_LOCK='1',BILL_NO='{1}' WHERE CELL_CODE='{0}'", VCell, billNo);
        //    ExecuteNonQuery(strSQL);

        //    strSQL = string.Format("UPDATE WCS_TASK SET CELL_CODE='{0}' WHERE {1}", VCell, where);
        //    ExecuteNonQuery(strSQL);


        //    SysStationDao sysdao = new SysStationDao();

        //    dt = sysdao.GetSationInfo(VCell, "11","3");
        //    string TaskNo = InsertTaskDetail(TaskID);

        //    strValue[0] = TaskID;
        //    strValue[1] = TaskNo;
        //    strValue[2] = dt.Rows[0]["STATION_NO"].ToString();
        //    strValue[3] = dt.Rows[0]["CRANE_POSITION"].ToString();
        //    strValue[4] = VCell;
        //    strValue[5] = dt.Rows[0]["CRANE_NO"].ToString();

        //    return strValue;
        //}

        /// <summary>
        ///  烟包替换记录
        /// </summary>
        /// <param name="strWhere"></param>
        public void InsertChangeProduct(string ProductBarcode, string ProductCode, string NewProductBarcode, string NewProductCode)
        {
            string strSQL = string.Format("INSERT INTO WCS_CHANGEPRODUCT(PRODUCT_BARCODE,PRODUCT_CODE,NEWPRODUCT_BARCODE,NEWPRODUCT_CODE,CHANGE_TIME,IS_CHANGE) VALUES('{0}','{1}','{2}','{3}',SYSDATE,'0')", new string[] { ProductBarcode, ProductCode, NewProductBarcode, NewProductCode });
            ExecuteNonQuery(strSQL);
        }

        /// <summary>
        /// 出库任务排序，判断能否给穿梭车下达出库任务,blnCar表示小车出库
        /// </summary>
        /// <param name="ForderBillNo"></param>
        /// <param name="Forder"></param>
        /// <param name="IsMix"></param>
        /// <returns></returns>
        public bool ProductCanToCar(string ForderBillNo,string Forder,string IsMix,bool blnCar)
        {
            bool blnValue = false;
            string strSQL = string.Format("SELECT COUNT(*) FROM WCS_TASK_DETAIL DETAIL " +
                                          "LEFT JOIN WCS_TASK TASK ON DETAIL.TASK_ID=TASK.TASK_ID " +
                                          "WHERE DETAIL.STATE=1 AND TASK.IS_MIX='{0}' AND TASK.FORDER ={1} AND TASK.FORDERBILLNO='{2}' " +
                                          "AND DETAIL.ITEM_NO={3} ", IsMix, Forder, ForderBillNo, (blnCar ? 3 : 1));

            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (int.Parse(dt.Rows[0][0].ToString()) > 0)
            {
                blnValue = true;
            }
            if (!blnValue)
            {
                if (IsMix == "1") //混装，先判断整包是否出库完成
                {
                    strSQL = string.Format("SELECT COUNT(*) FROM WCS_TASK_DETAIL DETAIL LEFT JOIN WCS_TASK TASK ON DETAIL.TASK_ID=TASK.TASK_ID " +
                           "WHERE DETAIL.ITEM_NO={1} AND DETAIL.STATE=0 AND TASK.IS_MIX=0 AND TASK.FORDERBILLNO='{0}' ", ForderBillNo, (blnCar ? 3 : 1));
                    dt = ExecuteQuery(strSQL).Tables[0];
                    if (int.Parse(dt.Rows[0][0].ToString()) == 0) //整包未出完
                    {
                        strSQL = string.Format("SELECT COUNT(*) FROM WCS_TASK_DETAIL DETAIL LEFT JOIN WCS_TASK TASK ON DETAIL.TASK_ID=TASK.TASK_ID " +
                                   "WHERE DETAIL.ITEM_NO={2} AND DETAIL.STATE=0 AND TASK.IS_MIX=1 AND TASK.FORDER<{1} AND TASK.FORDERBILLNO='{0}' ", ForderBillNo, Forder, (blnCar ? 3 : 1));
                        dt = ExecuteQuery(strSQL).Tables[0];
                        if (int.Parse(dt.Rows[0][0].ToString()) == 0) //整包未出完
                        {
                            blnValue = true;
                        }
                    }
                }
                else
                {
                    strSQL = string.Format("SELECT COUNT(*) FROM WCS_TASK_DETAIL DETAIL LEFT JOIN WCS_TASK TASK ON DETAIL.TASK_ID=TASK.TASK_ID " +
                                  "WHERE DETAIL.ITEM_NO={2} AND DETAIL.STATE=0 AND TASK.IS_MIX=0 AND TASK.FORDER<{1} AND TASK.FORDERBILLNO='{0}' ", ForderBillNo, Forder, (blnCar ? 3 : 1));
                    dt = ExecuteQuery(strSQL).Tables[0];
                    if (int.Parse(dt.Rows[0][0].ToString()) == 0) //整包未出完
                    {
                        blnValue = true;
                    }
                }
            }
            return blnValue;
        }
        /// <summary>
        /// 出库任务排序，判定小车能否下任务
        /// </summary>
        /// <param name="ForderBillNo"></param>
        /// <param name="Forder"></param>
        /// <param name="IsMix"></param>
        /// <returns></returns>
        public bool ProductOutToStation(string ForderBillNo, int Forder)
        {
            bool blnCanOut = false;
            string strSQL = string.Format("SELECT COUNT(*) FROM WCS_TASK_DETAIL DETAIL " +
                                          "LEFT JOIN WCS_TASK TASK ON DETAIL.TASK_ID=TASK.TASK_ID " +
                                          "WHERE DETAIL.STATE<2 AND TASK.FORDERBILLNO='{0}' AND TASK.FORDER <{1} " +
                                          "AND DETAIL.ITEM_NO=2 ", ForderBillNo, Forder);

            if (Forder == 1)
                return true;

            //Forder--;
            //获取前一顺序的是否都完成
            strSQL = string.Format("SELECT COUNT(*) FROM WCS_TASK TASK " +
                                   "WHERE (EXISTS (SELECT * FROM WCS_TASK_DETAIL DETAIL WHERE DETAIL.TASK_ID=TASK.TASK_ID AND DETAIL.ITEM_NO=3 AND DETAIL.STATE IN('0','1')) OR TASK.TASK_ID NOT IN(SELECT TASK_ID FROM WCS_TASK_DETAIL)) AND " +
                                   "TASK.FORDERBILLNO='{0}' AND TASK.FORDER<{1}", ForderBillNo, Forder);

            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (int.Parse(dt.Rows[0][0].ToString()) <= 0)
                blnCanOut = true;

            return blnCanOut;
        }
        /// <summary>
        /// 出库任务排序，判定小车能否下任务
        /// </summary>
        /// <param name="ForderBillNo"></param>
        /// <param name="Forder"></param>
        /// <param name="IsMix"></param>
        /// <returns></returns>
        public bool PermissionOutToStation(string TaskNo)
        {
            bool blnCanOut = false;
            string strSQL = string.Format("SELECT * FROM WCS_TASK_DETAIL DETAIL " +
                                          "LEFT JOIN WCS_TASK TASK ON DETAIL.TASK_ID=TASK.TASK_ID " +
                                          "WHERE DETAIL.TASK_NO='{0}' AND DETAIL.STATE IN('0','1','2','3') AND DETAIL.ITEM_NO=3 ", TaskNo);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (dt.Rows.Count > 0)
            {
                string ForderBillNo = dt.Rows[0]["FORDERBILLNO"].ToString();
                int FOrder = int.Parse(dt.Rows[0]["FORDER"].ToString());
                if (FOrder == 1)
                    return true;

                //获取前一顺序的是否都完成
                //strSQL = string.Format("SELECT COUNT(*) FROM WCS_TASK TASK " +
                //                       "WHERE (EXISTS (SELECT * FROM WCS_TASK_DETAIL DETAIL WHERE DETAIL.TASK_ID=TASK.TASK_ID AND DETAIL.ITEM_NO=3 AND DETAIL.STATE IN('0','1','2')) OR TASK.TASK_ID NOT IN(SELECT TASK_ID FROM WCS_TASK_DETAIL)) AND " +
                //                       "TASK.FORDERBILLNO='{0}' AND TASK.FORDER<{1}", ForderBillNo, FOrder);

                strSQL = string.Format("SELECT COUNT(*) FROM WCS_TASK TASK " +
                                       "WHERE (EXISTS (SELECT * FROM WCS_TASK_DETAIL DETAIL WHERE DETAIL.TASK_ID=TASK.TASK_ID AND DETAIL.ITEM_NO=3 AND DETAIL.STATE IN('0','1')) OR TASK.TASK_ID NOT IN(SELECT TASK_ID FROM WCS_TASK_DETAIL)) AND " +
                                       "TASK.FORDERBILLNO='{0}' AND TASK.FORDER<{1}", ForderBillNo, FOrder);
                dt = ExecuteQuery(strSQL).Tables[0];
                if (int.Parse(dt.Rows[0][0].ToString()) <= 0)
                    blnCanOut = true;
            }

            return blnCanOut;
        }
        /// <summary>
        /// 出库任务排序，判定堆垛机能否出库
        /// </summary>
        /// <param name="ForderBillNo"></param>
        /// <param name="Forder"></param>
        /// <param name="IsMix"></param>
        /// <returns></returns>
        public bool ProductOutToStation(string ForderBillNo, int Forder, string CraneNo)
        {
            bool blnCanOut = false;
            string strSQL = string.Format("SELECT COUNT(*) FROM WCS_TASK_DETAIL DETAIL " +
                                          "LEFT JOIN WCS_TASK TASK ON DETAIL.TASK_ID=TASK.TASK_ID " +
                                          "WHERE DETAIL.STATE=1 AND TASK.FORDERBILLNO='{0}' AND TASK.FORDER <{1} AND DETAIL.CRANE_NO='{2}' " +
                                          "AND DETAIL.ITEM_NO=1 ", ForderBillNo, Forder, CraneNo);

            if (Forder == 1)
                return true;

            //Forder--;
            //获取当前堆垛机前一顺序的是否都完成
            strSQL = string.Format("SELECT COUNT(*) FROM WCS_TASK TASK " +
                                   "INNER JOIN CMD_CELL ON TASK.CELL_CODE=CMD_CELL.CELL_CODE " +
                                   "INNER JOIN CMD_SHELF ON CMD_CELL.SHELF_CODE=CMD_SHELF.SHELF_CODE " +
                                   "WHERE (EXISTS (SELECT * FROM WCS_TASK_DETAIL DETAIL WHERE DETAIL.TASK_ID=TASK.TASK_ID AND DETAIL.ITEM_NO=1 AND DETAIL.STATE IN('0','1')) OR TASK.TASK_ID NOT IN(SELECT TASK_ID FROM WCS_TASK_DETAIL)) AND " +
                                   "TASK.FORDERBILLNO='{0}' AND TASK.FORDER<{1} AND CMD_SHELF.CRANE_NO='{2}'", ForderBillNo, Forder, CraneNo);

            strSQL = string.Format("SELECT COUNT(*) FROM WCS_TASK TASK " +
                                   "INNER JOIN CMD_CELL ON TASK.CELL_CODE=CMD_CELL.CELL_CODE " +
                                   "INNER JOIN CMD_SHELF ON CMD_CELL.SHELF_CODE=CMD_SHELF.SHELF_CODE " +
                                   "WHERE (EXISTS (SELECT * FROM WCS_TASK_DETAIL DETAIL WHERE DETAIL.TASK_ID=TASK.TASK_ID AND DETAIL.ITEM_NO=1 AND DETAIL.STATE IN('0','1')) OR TASK.TASK_ID NOT IN(SELECT TASK_ID FROM WCS_TASK_DETAIL)) AND " +
                                   "TASK.FORDERBILLNO LIKE 'OS%' AND TASK.FORDER<{1} AND CMD_SHELF.CRANE_NO='{2}'", ForderBillNo, Forder, CraneNo);

            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (int.Parse(dt.Rows[0][0].ToString()) <= 0)
                blnCanOut = true;

            return blnCanOut;
        }


        public bool ProductOutToStation_ex(string ForderBillNo, int Forder, string CraneNo)
        {
            bool blnCanOut = false;
           
            if (Forder == 1)
                return true;

            //Forder--;
            //获取当前堆垛机前一顺序的是否都完成
            string  strSQL = string.Format("SELECT COUNT(*) FROM WCS_TASK TASK " +
                                   "INNER JOIN CMD_CELL ON TASK.CELL_CODE=CMD_CELL.CELL_CODE " +
                                   "INNER JOIN CMD_SHELF ON CMD_CELL.SHELF_CODE=CMD_SHELF.SHELF_CODE " +
                                   "WHERE   TASK.STATE = 0 AND " +
                                   "TASK.FORDERBILLNO='{0}' AND TASK.FORDER < {1} AND CMD_SHELF.CRANE_NO='{2}'", ForderBillNo, Forder, CraneNo);

            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (int.Parse(dt.Rows[0][0].ToString()) <= 0)
                blnCanOut = true;

            return blnCanOut;
        }
        /// <summary>
        /// 判断二楼出库，任务号到达拆盘处，是否已经执行？
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public bool SeparateTaskDetailStart(string TaskID)
        {
            bool blnValue = false;
            string strSQL = string.Format("SELECT TASK_ID FROM WCS_TASK_DETAIL WHERE ITEM_NO=5 AND TASK_ID='{0}' AND STATE in (1,2)", TaskID);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (dt.Rows.Count > 0)
                blnValue = true;
            return blnValue;
        }

        /// <summary>
        /// 小车待任务数量
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int CarTaskInfo()
        {
            string strSQL = "SELECT TASK.TASK_ID,ITEM_NO " + 
                            "FROM WCS_TASK_DETAIL DETAIL " +
                            "LEFT JOIN WCS_TASK TASK ON DETAIL.TASK_ID=TASK.TASK_ID " +
                            "WHERE ITEM_NO=3 AND TASK_TYPE='22' AND DETAIL.STATE=0";
            return ExecuteQuery(strSQL).Tables[0].Rows.Count;
        }

         /// <summary>
        /// 二楼托盘组入库申请，判断是否有排程，小车未接货的任务。
        /// </summary>
        /// <param name="BillNo"></param>
        /// <returns></returns>
        public string GetPalletInTask()
        {
            string strvalue = "";
            string strSQL = "SELECT TASK.TASK_ID,ITEM_NO FROM WCS_TASK_DETAIL DETAIL LEFT JOIN WCS_TASK TASK ON DETAIL.TASK_ID=TASK.TASK_ID WHERE ITEM_NO=2 AND TASK_TYPE='21' AND DETAIL.STATE=0 AND PRODUCT_CODE='0000'";
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (dt.Rows.Count > 0)
                strvalue = dt.Rows[0]["TASK_ID"].ToString();
            return strvalue;
        }


        public DataTable GetCraneTaskBySequenceNo(string SequenceNo)
        {
            string strSQL = string.Format("SELECT TASK_ID FROM WCS_TASK_DETAIL WHERE SQUENCE_NO='{0}'", SequenceNo);

            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            string strWhere = "1=2";
            if (dt.Rows.Count > 0)
                strWhere = "1=1";


            return CraneTaskIn(strWhere);
        }
        /// <summary>
        /// 入库时，货位有货，手动操作改变入库货位
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="CraneNo"></param>
        /// <param name="CellCode"></param>
        public void UpdateToStation(string TaskID, string ItemNo, string CellCode, string OldCellCode)
        {
            //原货位处理,解锁并标记
            string strSQL = string.Format("UPDATE CMD_CELL SET IS_LOCK ='0',CELL_STATE='0' WHERE CELL_CODE='{0}'", OldCellCode);
            ExecuteNonQuery(strSQL);

            strSQL = string.Format("SELECT * FROM WCS_TASK WHERE TASK_ID='{0}'", TaskID);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];

            int TaskItemNo = int.Parse(TaskID.Substring(TaskID.Length - 2, 2));
            string BillNo = "";
            string ProductCode = "";
            string TaskType = "";
            if (dt.Rows.Count > 0)
            {
                BillNo = dt.Rows[0]["BILL_NO"].ToString();
                ProductCode = dt.Rows[0]["PRODUCT_CODE"].ToString();
                TaskType = dt.Rows[0]["TASK_TYPE"].ToString();
            }
            strSQL = string.Format("UPDATE CMD_CELL SET IS_LOCK='1',BILL_NO='{1}',PRODUCT_CODE='{2}',CELL_STATE='1' WHERE CELL_CODE='{0}'", CellCode, BillNo, ProductCode);
            ExecuteNonQuery(strSQL);

            ProductStateDao dao = new ProductStateDao();

            if (TaskType == "14")
            {
                strSQL = string.Format("UPDATE WCS_TASK SET NEWCELL_CODE ='{0}' WHERE TASK_ID='{1}'", CellCode, TaskID);
                ExecuteNonQuery(strSQL);
                strSQL = string.Format("UPDATE WMS_PRODUCT_STATE SET NEWCELL_CODE='{0}' WHERE BILL_NO='{1}' AND ITEM_NO='{2}'", CellCode, BillNo, TaskItemNo);
                ExecuteNonQuery(strSQL);
            }
            else
            {
                strSQL = string.Format("UPDATE WCS_TASK SET CELL_CODE ='{0}' WHERE TASK_ID='{1}'", CellCode, TaskID);
                ExecuteNonQuery(strSQL);
                strSQL = string.Format("UPDATE WMS_PRODUCT_STATE SET CELL_CODE='{0}' WHERE BILL_NO='{1}' AND ITEM_NO='{2}'", CellCode, BillNo, TaskItemNo);
                ExecuteNonQuery(strSQL);
            }
            
            strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET TO_STATION ='{0}' WHERE TASK_ID='{1}' AND ITEM_NO='{2}'", CellCode, TaskID, ItemNo);
            ExecuteNonQuery(strSQL);
        }        
        /// <summary>
        /// 出库时，货位无货，手动操作改变出库货位
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="CraneNo"></param>
        /// <param name="CellCode"></param>
        public void UpdateFromStation(string TaskID, string ItemNo, string CellCode, string OldCellCode)
        {
            //原货位处理,解锁并标记
            CellDao cdao = new CellDao();
            //原货位解锁
            cdao.UpdateCellOutFinishUnLock(OldCellCode);
            //新货位锁定
            //cdao.UpdateCellLock(CellCode);
            string strSQL = string.Format("UPDATE CMD_CELL SET IS_LOCK ='1',CELL_STATE='2' WHERE CELL_CODE='{0}'", CellCode);
            ExecuteNonQuery(strSQL);

            string BillNo = TaskID.Substring(0, TaskID.Length - 2);
            int TaskItemNo = int.Parse(TaskID.Substring(TaskID.Length - 2, 2));

            strSQL = string.Format("UPDATE WCS_TASK SET CELL_CODE ='{0}' WHERE TASK_ID='{1}'", CellCode, TaskID);
            strSQL = string.Format("UPDATE WCS_TASK SET (CELL_CODE,PRODUCT_BARCODE,PALLET_CODE)=(SELECT CELL_CODE,PRODUCT_BARCODE,PALLET_CODE FROM CMD_CELL WHERE CELL_CODE ='{0}') WHERE TASK_ID='{1}'", CellCode, TaskID);
            ExecuteNonQuery(strSQL);

            strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET FROM_STATION ='{0}' WHERE TASK_ID='{1}' AND ITEM_NO='{2}'", CellCode, TaskID, ItemNo);
            ExecuteNonQuery(strSQL);

            strSQL = string.Format("UPDATE WMS_PRODUCT_STATE SET (CELL_CODE,PRODUCT_BARCODE,PALLET_CODE)=(SELECT CELL_CODE,PRODUCT_BARCODE,PALLET_CODE FROM CMD_CELL WHERE CELL_CODE ='{0}') WHERE BILL_NO='{1}' AND ITEM_NO='{2}'", CellCode, BillNo, TaskItemNo);
            ExecuteNonQuery(strSQL);
        }
        /// <summary>
        /// 取得二楼出货批次号
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="CraneNo"></param>
        /// <param name="CellCode"></param>
        public string GetBatchNo(string ForderBillNo)
        {
            string BatchNo = "";
            string strSQL = string.Format("SELECT WCS_TASK.BATCH_NO FROM WCS_TASK WHERE WCS_TASK.FORDERBILLNO='{0}' AND ROWNUM<2 ORDER BY TASK_ID", ForderBillNo);
            strSQL = string.Format("SELECT BATCH_NO,COUNT(*) FROM VIEW_WCS_TASK WHERE FORDERBILLNO='{0}' AND TASK_TYPE='22' AND RFIDERR_HANDLE_FLAG='0' GROUP BY BATCH_NO", ForderBillNo);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];

            int Amount = 0;
            if (dt.Rows.Count > 0)
            {
                BatchNo = dt.Rows[0][0].ToString();
                Amount = int.Parse(dt.Rows[0][1].ToString());
                if (BatchNo.Trim().Length > 0)
                    return BatchNo;
                else
                {
                    string PreCode = DateTime.Now.ToString("yyMMdd");
                    string Reg = PreCode + "[0-9][0-9]";
                    strSQL = string.Format("SELECT MAX(BATCH_NO) FROM VIEW_WCS_TASK WHERE REGEXP_LIKE(BATCH_NO,'^{0}$')", Reg);
                    dt = ExecuteQuery(strSQL).Tables[0];
                    if (dt.Rows.Count > 0)
                    {                        
                        string bno = dt.Rows[0][0].ToString();
                        if (bno.Trim().Length > 0)
                        {
                            int index = int.Parse(bno.Substring(6, 2)) + 1;
                            BatchNo = PreCode + index.ToString().PadLeft(2, '0');
                        }
                        else
                            BatchNo = PreCode + "01";
                    }
                    else
                        BatchNo = PreCode + "01";
                }
            }
            strSQL = string.Format("UPDATE WCS_TASK SET BATCH_NO='{0}',AMOUNT={1} WHERE FORDERBILLNO='{2}' AND BATCH_NO IS NULL", BatchNo, Amount, ForderBillNo);
            ExecuteNonQuery(strSQL);
            return BatchNo;
        }
        /// <summary>
        /// 取得二楼出货顺序号
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="CraneNo"></param>
        /// <param name="CellCode"></param>
        private int[] GetOrderNo(string ForderBillNo,string TaskType)
        {
            int[] OrderNo = new int[3];
            string strSQL = string.Format("SELECT DECODE(MAX(ORDER_NO),NULL,0,MAX(ORDER_NO)),COUNT(*) FROM VIEW_WCS_TASK WHERE FORDERBILLNO='{0}' AND TASK_TYPE='{1}' AND RFIDERR_HANDLE_FLAG='0'", ForderBillNo, TaskType);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];

            if (dt.Rows.Count > 0)
            {
                OrderNo[0] = int.Parse(dt.Rows[0][0].ToString()) + 1;
                OrderNo[1] = int.Parse(dt.Rows[0][1].ToString());
                OrderNo[2] = int.Parse(dt.Rows[0][1].ToString());
            }

            return OrderNo;
        }
        /// <summary>
        /// 取得二楼出货顺序号,OrderNo[0] 顺序号 OrderNo[1]头尾标识,OrderNo[2] 总数量
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="CraneNo"></param>
        /// <param name="CellCode"></param>
        public int[] GetOrderNo(string ForderBillNo, string TaskID, string TaskType)
        {
            int[] OrderNo = new int[3];
            OrderNo = GetOrderNo(ForderBillNo, TaskType);

            string strSQL = string.Format("SELECT DECODE(ORDER_NO,NULL,0,ORDER_NO) FROM WCS_TASK WHERE TASK_ID='{0}'", TaskID);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];

            if (dt.Rows.Count > 0)
            {                
                if (dt.Rows[0][0].ToString() == "0")
                {
                    strSQL = string.Format("UPDATE WCS_TASK SET ORDER_NO={1} WHERE TASK_ID='{0}'", TaskID, OrderNo[0]);
                    ExecuteNonQuery(strSQL);

                    //紧急补料情况更新批号及总数量=被补料的出库单的批次及数量
                    if (TaskType == "12")
                    {
                        strSQL = string.Format("UPDATE WCS_TASK SET (BATCH_NO,AMOUNT)=(SELECT BATCH_NO,AMOUNT FROM VIEW_WCS_TASK WHERE FORDERBILLNO='{0}' AND TASK_TYPE='22' AND ROWNUM<2) WHERE TASK_ID='{1}'", ForderBillNo,TaskID);
                        ExecuteNonQuery(strSQL);
                    }
                }
                else
                {
                    OrderNo[0] = int.Parse(dt.Rows[0][0].ToString());
                }
                if (OrderNo[0] == OrderNo[1])
                    OrderNo[1] = 2;
                else if(OrderNo[0] == 1)
                    OrderNo[1] = 1;
                else
                    OrderNo[1] = 0;
            }
            return OrderNo;
        }
        /// <summary>
        /// 紧急补料写给PLC后更新
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="CraneNo"></param>
        /// <param name="CellCode"></param>
        public void UpdateSendPLC(string TaskID)
        {
            string strSQL = string.Format("UPDATE WCS_TASK SET IS_SEND_PLC ='1' WHERE TASK_ID='{0}'", TaskID);
            ExecuteNonQuery(strSQL);
            strSQL = string.Format("UPDATE WCS_TASKH SET IS_SEND_PLC ='1' WHERE TASK_ID='{0}'", TaskID);
            ExecuteNonQuery(strSQL);
        }
        /// <summary>
        /// 取得小车目标地址平面号
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="CraneNo"></param>
        /// <param name="CellCode"></param>
        public string CarTargetCode(string ForderBillNo)
        {
            string CarTargetCode = "";
            string strSQL = string.Format("SELECT DETAIL.TO_STATION FROM WCS_TASK TASK " +
                                          "INNER JOIN WCS_TASK_DETAIL DETAIL ON TASK.TASK_ID=DETAIL.TASK_ID " +
                                          "WHERE TASK.FORDERBILLNO='{0}' AND DETAIL.ITEM_NO=3 AND DETAIL.STATE IN('1','2') AND ROWNUM<2 ORDER BY TASK.TASK_ID", ForderBillNo);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];

            if (dt.Rows.Count > 0)
                CarTargetCode = dt.Rows[0][0].ToString();

            return CarTargetCode;
        }
        /// <summary>
        /// 查询堆垛机任务
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="CraneNo"></param>
        /// <param name="CellCode"></param>
        private DataTable GetAllTaskByCrane(string CraneNo)
        {
            //先找任务,按照等级优先,暂时先没考虑移库的
            string strSQL = string.Format("SELECT TASK.TASK_ID,TASK.BILL_NO,TASK.TASK_TYPE,TASK.TARGET_CODE,TASK.TASK_LEVEL,TASK.FORDER," +
                                          "DECODE(DETAIL.ITEM_NO,NULL,1,DETAIL.ITEM_NO) ITEM_NO, " +
                                          "ROW_NUMBER() OVER(PARTITION BY TARGET_CODE ORDER BY TASK_LEVEL,FORDER) NEWID " +
                                          "FROM WCS_TASK TASK " +
                                          "LEFT JOIN WCS_TASK_DETAIL DETAIL ON DETAIL.TASK_ID=TASK.TASK_ID " +
                                          "LEFT JOIN CMD_CELL ON TASK.CELL_CODE=CMD_CELL.CELL_CODE " +
                                          "LEFT JOIN CMD_CELL NEWCMD_CELL ON TASK.NEWCELL_CODE=NEWCMD_CELL.CELL_CODE " +
                                          "LEFT JOIN CMD_SHELF ON CMD_CELL.SHELF_CODE=CMD_SHELF.SHELF_CODE " +
                                          "LEFT JOIN CMD_SHELF NEWCMD_SHELF ON NEWCMD_CELL.SHELF_CODE=NEWCMD_SHELF.SHELF_CODE  " +
                                          "WHERE TASK.STATE IN('0','1') AND (CMD_SHELF.CRANE_NO='{0}' OR NEWCMD_SHELF.CRANE_NO='{0}') AND (DETAIL.STATE=0 AND DETAIL.CRANE_NO='{0}' OR DETAIL.STATE IS NULL) " +
                                          "ORDER BY NEWID,TASK.TASK_LEVEL DESC,TASK.FORDER", CraneNo);

            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            
            return dt;
        }
        /// <summary>
        /// 查询堆垛机任务
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="CraneNo"></param>
        /// <param name="CellCode"></param>
        public DataRow GetCraneTask(string CraneNo)
        {
            DataRow dr = null;

            DataTable dt = GetAllTaskByCrane(CraneNo);

            if(dt.Rows.Count>0)
            {
                string TaskID = dt.Rows[0]["TASK_ID"].ToString();
                string TaskType = dt.Rows[0]["TASK_TYPE"].ToString();
                int ItemNo = int.Parse(dt.Rows[0]["ITEM_NO"].ToString());

                if (ItemNo == 1)
                    InsertTaskDetail(TaskID);                    

                DataTable dtTask = GetTaskInfo(TaskID, ItemNo);

                if (dtTask.Rows.Count > 0)
                    dr = dtTask.Rows[0];
            }
            return dr;
        }
        /// <summary>
        /// 查询堆垛机任务
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="CraneNo"></param>
        /// <param name="CellCode"></param>
        public DataRow GetNextCraneTask(string CraneNo, string FinishedItemNo, string FinishedTargetCode)
        {
            DataRow dr = null;
            int ItemNo = int.Parse(FinishedItemNo);
            string filter = "1=1";
           
            DataTable dt = GetAllTaskByCrane(CraneNo);
            if (dt.Rows.Count <= 0)
                return dr;

            int QueryCount = 0;
            DataRow[] drs = null;
            //如果上一次完成的任务是出库，那么先找有无入库任务
            while (QueryCount<2)
            {                
                if (ItemNo == 1)
                {
                    filter = "TASK_TYPE='11' OR (TASK_TYPE='13' AND ITEM_NO=4) OR (TASK_TYPE='14' AND ITEM_NO=3) OR TASK_TYPE='21'";
                    drs = dt.Select(filter, "NEWID,TASK_LEVEL DESC,FORDER,TARGET_CODE,BILL_NO");
                    if (drs.Length > 0)
                        break;
                    ItemNo = 2;
                }
                else
                {
                    filter = "TASK_TYPE='22' OR TASK_TYPE='12' OR (TASK_TYPE='13' AND ITEM_NO=1) OR (TASK_TYPE='14' AND ITEM_NO=1)";
                    drs = dt.Select(filter, "NEWID,TASK_LEVEL,FORDER,TARGET_CODE,BILL_NO");
                    
                    if (drs.Length > 0)
                        break;
                    ItemNo = 1;
                }
                QueryCount++;
            }

            if (drs == null)
                return dr;
            if (drs.Length <= 0)
                return dr;

            string TaskID = dr["TASK_ID"].ToString();
            string TaskType = dr["TASK_TYPE"].ToString();
            ItemNo = int.Parse(dr["ITEM_NO"].ToString());

            if (ItemNo == 1)            
                InsertTaskDetail(TaskID);

            DataTable dtTask = GetTaskInfo(TaskID, ItemNo);

            if (dtTask.Rows.Count > 0)
                dr = dtTask.Rows[0];
            
            return dr;
        }
        /// <summary>
        /// 获取任务信息
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="CraneNo"></param>
        /// <param name="CellCode"></param>
        public DataTable GetTaskInfo(string TaskID, int ItemNo)
        {
            string strSQL = string.Format("SELECT TASK.TASK_ID,TASK.TASK_TYPE,DETAIL.TASK_NO,DETAIL.ITEM_NO,DETAIL.ASSIGNMENT_ID,CMD_SHELF.CRANE_NO, '30'||TASK.CELL_CODE||'01' AS CELLSTATION," +
                                      "SYS_STATION.CRANE_POSITION CRANESTATION,TASK.BILL_NO,TASK.PRODUCT_CODE,TASK.CELL_CODE," +
                                      "TASK.IS_MIX,SYS_STATION.SERVICE_NAME,SYS_STATION.ITEM_NAME_1,SYS_STATION.ITEM_NAME_2,TASK.PRODUCT_BARCODE," +
                                      "TASK.PALLET_CODE,'' AS SQUENCE_NO,TASK.TARGET_CODE,SYS_STATION.STATION_NO,SYS_STATION.MEMO,TASK.PRODUCT_TYPE," +
                                      "CMD_SHELF_NEW.CRANE_NO AS NEW_CRANE_NO,TASK.NEWCELL_CODE,DECODE(TASK.NEWCELL_CODE,NULL,'', '30'||TASK.NEWCELL_CODE||'01') AS NEW_TO_STATION," +
                                      "SYS_STATION_NEW.STATION_NO AS NEW_TARGET_CODE,TASK.FORDER,TASK.FORDERBILLNO,DETAIL.ERR_CODE " +
                                      "FROM WCS_TASK TASK " +
                                      "LEFT JOIN WCS_TASK_DETAIL DETAIL ON DETAIL.TASK_ID=TASK.TASK_ID " +
                                      "LEFT JOIN CMD_CELL ON CMD_CELL.CELL_CODE=TASK.CELL_CODE " +
                                      "LEFT JOIN CMD_SHELF ON CMD_CELL.SHELF_CODE=CMD_SHELF.SHELF_CODE " +
                                      "LEFT JOIN CMD_CELL CMD_CELL_NEW ON CMD_CELL_NEW.CELL_CODE=TASK.NEWCELL_CODE " +
                                      "LEFT JOIN CMD_SHELF CMD_SHELF_NEW ON CMD_CELL_NEW.SHELF_CODE=CMD_SHELF_NEW.SHELF_CODE " +
                                      "LEFT JOIN SYS_STATION ON SYS_STATION.STATION_TYPE=TASK.TASK_TYPE AND SYS_STATION.CRANE_NO=CMD_SHELF.CRANE_NO AND SYS_STATION.ITEM=DETAIL.ITEM_NO " +
                                      "LEFT JOIN SYS_STATION SYS_STATION_NEW ON SYS_STATION_NEW.STATION_TYPE='14' AND SYS_STATION_NEW.ITEM=3 AND SYS_STATION_NEW.CRANE_NO=CMD_SHELF_NEW.CRANE_NO " +
                                      "WHERE DETAIL.TASK_ID='{0}' AND DETAIL.ITEM_NO={1}", TaskID, ItemNo);

            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            return dt;
        }
        /// <summary>
        /// 获取任务信息
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="CraneNo"></param>
        /// <param name="CellCode"></param>
        public DataTable GetTaskInfoByFilter(string filter)
        {
            string strSQL = string.Format("SELECT TASK.FORDER,TASK.FORDERBILLNO " +
                                      "FROM WCS_TASK TASK " +
                                      "INNER JOIN WCS_TASK_DETAIL DETAIL ON DETAIL.TASK_ID=TASK.TASK_ID " +
                                      "WHERE {0}", filter);

            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            return dt;
        }
        /// <summary>
        /// 获取需异常处理的堆垛机任务
        /// </summary>
        /// <returns></returns>
        internal DataTable GetErrTask()
        {
            string strSQL = "SELECT * FROM WCS_TASK TASK " +
                            "WHERE  TASK.STATE in ('71','72') " +
                            "ORDER BY TASK.CRANE_NO DESC";

            return ExecuteQuery(strSQL).Tables[0];
        }

        internal bool Change_Trk_Step(string psTask_Id, string psItem_No, int piState)
        {
            string strSQL1 = string.Format("UPDATE WCS_TASK SET STATE ={0} WHERE TASK_ID='{1}'", piState, psTask_Id);

            string strSQL = string.Format("UPDATE WCS_TASK_DETAIL SET STATE ={0} WHERE TASK_ID='{1}' AND ITEM_NO='{2}'", piState, psTask_Id, psItem_No);

            return (ExecuteNonQuery(strSQL) > 0) && (ExecuteNonQuery(strSQL1) > 0);
        }

        internal DataTable Get_Task_Type(string psAssignmentID)
        {
            DataTable dtResut = null;
            string strSql = string.Format("SELECT distinct TASK_ID FROM WCS_TASK_DETAIL TASK_DET " +
                 "WHERE  TASK_DET.Assignment_ID = '{0}'", psAssignmentID);

            DataTable dt = ExecuteQuery(strSql).Tables[0];

            if (dt != null)
            {
                string strSQL = string.Format("SELECT distinct TASK.TASK_TYPE FROM WCS_TASK TASK " +
                     "WHERE  TASK.TASK_ID = '{0}'", dt.Rows[0]["TASK_ID"]);

                dtResut = ExecuteQuery(strSQL).Tables[0];
            }

            return dtResut;
        }
        /// <summary>
        /// 获取每台堆垛机的出库任务，依据任务排序   and BMASTER.STATE in (3,4) 
        /// </summary>
        /// <param name="psCrnNo"></param>
        /// <returns></returns>
        internal DataTable GetOutTasks(string psCrnNo)
        {
            string strSQL = string.Format("SELECT TASK.TASK_ID,'' AS TASK_NO, TASK.TARGET_CODE, SYS_TASK_ROUTE.ITEM_NO,''AS  ASSIGNMENT_ID,CMD_SHELF.CRANE_NO AS CRANE_NO, '30'||TASK.CELL_CODE||'01' AS CELLSTATION,SYS_STATION.CRANE_POSITION CRANESTATION ," +
                           "TASK.STATE AS STATE ,TASK.BILL_NO," + "TASK.PRODUCT_CODE,TASK.CELL_CODE,TASK.TASK_TYPE,TASK.TASK_LEVEL as TASK_LEVEL ,TASK.TASK_DATE,TASK.IS_MIX,SYS_STATION.SERVICE_NAME,SYS_STATION.ITEM_NAME_1," +
                           "SYS_STATION.ITEM_NAME_2,TASK.PRODUCT_BARCODE,TASK.PALLET_CODE,'' AS SQUENCE_NO,TASK.TARGET_CODE,SYS_STATION.STATION_NO,SYS_STATION.MEMO,TASK.PRODUCT_TYPE,CMD_SHELF_NEW.CRANE_NO AS NEW_CRANE_NO,TASK.NEWCELL_CODE, " +
                           "DECODE(TASK.NEWCELL_CODE,NULL,'', '30'||TASK.NEWCELL_CODE||'01') AS NEW_TO_STATION,SYS_STATION_NEW.STATION_NO AS NEW_TARGET_CODE,TASK.FORDER,TASK.FORDERBILLNO,'' AS ERR_CODE,BMASTER.BTYPE_CODE,BMASTER.BILL_METHOD," +
                           "ROW_NUMBER() OVER(PARTITION BY TASK.TARGET_CODE ORDER BY TASK.FORDER) NEWID " +
                           "FROM  WCS_TASK TASK " +
                           "LEFT JOIN WCS_TASK_DETAIL DETAIL ON TASK.TASK_ID=DETAIL.TASK_ID AND DETAIL.ITEM_NO=1 " +
                           "LEFT JOIN WMS_BILL_MASTER BMASTER ON TASK.BILL_NO=BMASTER.BILL_NO " +
                           "LEFT JOIN WMS_FORMULA_DETAIL FDETAIL ON BMASTER.FORMULA_CODE=FDETAIL.FORMULA_CODE AND FDETAIL.PRODUCT_CODE=TASK.PRODUCT_CODE " +
                           "LEFT JOIN CMD_CELL ON CMD_CELL.CELL_CODE=TASK.CELL_CODE " +
                           "LEFT JOIN CMD_SHELF ON CMD_CELL.SHELF_CODE=CMD_SHELF.SHELF_CODE " +
                           "LEFT JOIN SYS_TASK_ROUTE ON SYS_TASK_ROUTE.TASK_TYPE=TASK.TASK_TYPE and SYS_TASK_ROUTE.ITEM_NO=1 " +
                           "LEFT JOIN CMD_CELL CMD_CELL_NEW ON CMD_CELL_NEW.CELL_CODE=TASK.NEWCELL_CODE " +
                           "LEFT JOIN CMD_SHELF CMD_SHELF_NEW ON CMD_CELL_NEW.SHELF_CODE=CMD_SHELF_NEW.SHELF_CODE " +
                           "LEFT JOIN SYS_STATION SYS_STATION ON SYS_STATION.STATION_TYPE=TASK.TASK_TYPE AND SYS_STATION.CRANE_NO=CMD_SHELF.CRANE_NO AND SYS_STATION.ITEM=SYS_TASK_ROUTE.ITEM_NO " +
                           "LEFT JOIN SYS_STATION SYS_STATION_NEW ON SYS_STATION_NEW.STATION_TYPE='14' AND SYS_STATION_NEW.ITEM = 3  and SYS_STATION_NEW.CRANE_NO=CMD_SHELF_NEW.CRANE_NO  " +
                           "WHERE  CMD_SHELF.CRANE_NO = '{0}' AND ((TASK.STATE IN ('0','1') AND DETAIL.STATE='0') OR (TASK.STATE='0' AND DETAIL.STATE IS NULL)) AND TASK.TASK_TYPE in ('22','12','14','13') " +
                           "ORDER BY TASK_LEVEL DESC,NEWID, TASK.FORDER,TASK.TARGET_CODE", psCrnNo);

            return ExecuteQuery(strSQL).Tables[0];
        }
        /// <summary>
        /// 查找发了报文但没执行的任务
        /// </summary>
        /// <param name="psCrnNo"></param>
        /// <returns></returns>
        internal DataTable GetNoExecuteTask()
        {
            string strSQL = "SELECT DISTINCT CRANE_NO FROM WCS_TASK_DETAIL WHERE CRANE_NO IS NOT NULL AND SQUENCE_NO IS NOT NULL AND STATE=0 ORDER BY CRANE_NO";
            return ExecuteQuery(strSQL).Tables[0];
        }
        /// <summary>
        /// 紧急补料顺序
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="CraneNo"></param>
        /// <param name="CellCode"></param>
        public void UpdateFeedingOrderNo(string BillNo, string TaskID)
        {
            //int[] OrderNo = new int[2];
            //OrderNo = GetOrderNo(ForderBillNo);

            //string strSQL = string.Format("SELECT DECODE(ORDER_NO,NULL,0,ORDER_NO) FROM WCS_TASK WHERE WCS_TASK.FORDERBILLNO='{0}' AND TASK_ID='{1}'", ForderBillNo, TaskID);
            //DataTable dt = ExecuteQuery(strSQL).Tables[0];

            //if (dt.Rows.Count > 0)
            //{
            //    if (dt.Rows[0][0].ToString() == "0")
            //    {
            //        strSQL = string.Format("UPDATE WCS_TASK SET ORDER_NO={2} WHERE WCS_TASK.FORDERBILLNO='{0}' AND TASK_ID='{1}'", ForderBillNo, TaskID, OrderNo[0]);
            //        ExecuteNonQuery(strSQL);
            //    }
            //    else
            //    {
            //        OrderNo[0] = int.Parse(dt.Rows[0][0].ToString());
            //    }
            //    if (OrderNo[0] == OrderNo[1])
            //        OrderNo[1] = 2;
            //    else if (OrderNo[0] == 1)
            //        OrderNo[1] = 1;
            //    else
            //        OrderNo[1] = 0;
            //}
            //return OrderNo;
        }
        internal int GetTargetSeq(int piCrnNo)
        {
            string strSQL = string.Format("SELECT Seq_TargetIdx{0}.NextVal FROM dual", piCrnNo.ToString());

            return int.Parse(ExecuteQuery(strSQL).Tables[0].Rows[0][0].ToString());
        }

        internal int GetCarTargetSeq()
        {
            string strSQL = "SELECT SEQ_CARTARGET.NextVal FROM dual";

            return int.Parse(ExecuteQuery(strSQL).Tables[0].Rows[0][0].ToString());
        }
        internal DataTable GetOutTasks(string psCrnNo, string gsTarget)
        {
            string strSQL = string.Format("SELECT TASK.TASK_ID,'' AS TASK_NO, TASK.TARGET_CODE, SYS_TASK_ROUTE.ITEM_NO,''AS  ASSIGNMENT_ID,CMD_SHELF.CRANE_NO AS CRANE_NO, '30'||TASK.CELL_CODE||'01' AS CELLSTATION,SYS_STATION.CRANE_POSITION CRANESTATION  ,TASK.STATE AS STATE ,TASK.BILL_NO," 
                + "TASK.PRODUCT_CODE,TASK.CELL_CODE,TASK.TASK_TYPE,TASK.TASK_LEVEL as TASK_LEVEL ,TASK.TASK_DATE,TASK.IS_MIX,SYS_STATION.SERVICE_NAME,SYS_STATION.ITEM_NAME_1," +
               "SYS_STATION.ITEM_NAME_2,TASK.PRODUCT_BARCODE,TASK.PALLET_CODE,'' AS SQUENCE_NO,TASK.TARGET_CODE,SYS_STATION.STATION_NO,SYS_STATION.MEMO,TASK.PRODUCT_TYPE,CMD_SHELF_NEW.CRANE_NO AS NEW_CRANE_NO,TASK.NEWCELL_CODE, " +
               "DECODE(TASK.NEWCELL_CODE,NULL,'', '30'||TASK.NEWCELL_CODE||'01') AS NEW_TO_STATION,SYS_STATION_NEW.STATION_NO AS NEW_TARGET_CODE,TASK.FORDER,TASK.FORDERBILLNO,'' AS ERR_CODE " +
               "FROM  WCS_TASK TASK " +
                           "LEFT JOIN WCS_TASK_DETAIL DETAIL ON TASK.TASK_ID=DETAIL.TASK_ID AND DETAIL.ITEM_NO=1 " +
                           "LEFT JOIN WMS_BILL_MASTER BMASTER ON TASK.BILL_NO=BMASTER.BILL_NO " +
                           "LEFT JOIN WMS_FORMULA_DETAIL FDETAIL ON BMASTER.FORMULA_CODE=FDETAIL.FORMULA_CODE AND FDETAIL.PRODUCT_CODE=TASK.PRODUCT_CODE " +
                           "LEFT JOIN CMD_CELL ON CMD_CELL.CELL_CODE=TASK.CELL_CODE " +
                           "LEFT JOIN CMD_SHELF ON CMD_CELL.SHELF_CODE=CMD_SHELF.SHELF_CODE " +
                           "LEFT JOIN SYS_TASK_ROUTE ON SYS_TASK_ROUTE.TASK_TYPE=TASK.TASK_TYPE and SYS_TASK_ROUTE.ITEM_NO=1 " +
                           "LEFT JOIN CMD_CELL CMD_CELL_NEW ON CMD_CELL_NEW.CELL_CODE=TASK.NEWCELL_CODE " +
                           "LEFT JOIN CMD_SHELF CMD_SHELF_NEW ON CMD_CELL_NEW.SHELF_CODE=CMD_SHELF_NEW.SHELF_CODE " +
                           "LEFT JOIN SYS_STATION SYS_STATION ON SYS_STATION.STATION_TYPE=TASK.TASK_TYPE AND SYS_STATION.CRANE_NO=CMD_SHELF.CRANE_NO AND SYS_STATION.ITEM=SYS_TASK_ROUTE.ITEM_NO " +
                           "LEFT JOIN SYS_STATION SYS_STATION_NEW ON SYS_STATION_NEW.STATION_TYPE='14' AND SYS_STATION_NEW.ITEM = 3  and SYS_STATION_NEW.CRANE_NO=CMD_SHELF_NEW.CRANE_NO  " +
               " WHERE CMD_SHELF.CRANE_NO = '{0}' AND TASK.TARGET_CODE in {1} AND ((TASK.STATE IN ('0','1') AND DETAIL.STATE='0') OR (TASK.STATE='0' AND DETAIL.STATE IS NULL)) AND TASK.TASK_TYPE in ('22','12','14','13') " +
                           "ORDER BY TASK_LEVEL DESC,TASK.FORDER,TASK.TARGET_CODE ", psCrnNo, gsTarget);

            return ExecuteQuery(strSQL).Tables[0];
        }
        /// <summary>
        /// 取得二楼出货顺序号,OrderNo[0] 顺序号 OrderNo[1]头尾标识,OrderNo[2] 总数量
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="CraneNo"></param>
        /// <param name="CellCode"></param>
        public void UpdateOrderNo(string TaskID, int OrderNo)
        {
            string strSQL = string.Format("UPDATE WCS_TASK SET ORDER_NO={1} WHERE TASK_ID='{0}'", TaskID, OrderNo);
            ExecuteNonQuery(strSQL);
        }
        /// <summary>
        /// 根据货位号，获取入库批次信息
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public DataTable GetTaskInfoByCellCode(string CellCode)
        {
            string strSQL = string.Format("SELECT T.TASK_ID,T.PRODUCT_CODE,T.PRODUCT_BARCODE,T.BILL_NO,P.*," +
                                        "M.CIGARETTE_CODE,C.CIGARETTE_NAME,M.FORMULA_CODE,F.FORMULA_NAME,G.GRADE_NAME," +
                                        "O.ORIGINAL_NAME,S.STYLE_NAME,CMD_CELL.BILL_NO INBILL_NO " +
                                        "FROM VIEW_WCS_TASK T " +
                                        "LEFT JOIN CMD_PRODUCT P ON T.PRODUCT_CODE=P.PRODUCT_CODE " +
                                        "LEFT JOIN WMS_BILL_MASTER M ON T.BILL_NO=M.BILL_NO " +
                                        "LEFT JOIN CMD_CIGARETTE C ON C.CIGARETTE_CODE=M.CIGARETTE_CODE " +
                                        "LEFT JOIN WMS_FORMULA_MASTER F ON F.FORMULA_CODE=M.FORMULA_CODE " +
                                        "LEFT JOIN CMD_PRODUCT_ORIGINAL O ON O.ORIGINAL_CODE=P.ORIGINAL_CODE " +
                                        "LEFT JOIN CMD_PRODUCT_GRADE G ON G.GRADE_CODE=P.GRADE_CODE " +
                                        "LEFT JOIN CMD_PRODUCT_STYLE S ON S.STYLE_NO=P.STYLE_NO " +
                                        "LEFT JOIN CMD_CELL ON T.CELL_CODE=CMD_CELL.CELL_CODE " +
                                        "WHERE T.TASK_TYPE='11' AND T.CELL_CODE='{0}' ORDER BY T.BILL_NO DESC", CellCode);
            return ExecuteQuery(strSQL).Tables[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CarNo"></param>
        /// <param name="TaskNo"></param>
        /// <returns></returns>
        public string[] GetCarToStation(string CarNo, string TaskNo)
        {
            string[] str = new string[2];
            str[0] = "";
            str[1] = "";
            string strSQL = string.Format("SELECT TO_STATION,PROCESS_NAME FROM VIEW_TASK_STATION WHERE CAR_NO='{0}' AND TASK_NO='{1}'", CarNo, TaskNo);
            DataTable dt = ExecuteQuery(strSQL).Tables[0];
            if (dt.Rows.Count > 0)
            {
                str[0] = dt.Rows[0]["TO_STATION"].ToString().Trim();
                str[1] = dt.Rows[0]["ITEM_NAME"].ToString().Trim();
            }

            return str;
        }
    }
}
