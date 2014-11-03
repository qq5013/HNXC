﻿using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;

namespace THOK.XC.Process.Process_01
{
    public class StockInStationProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  
             * 一楼入库到达入库站台。
            */
            try
            {
                object obj =  ObjectUtil.GetObject(stateItem.State);
                if (obj == null)
                    return;
                if (obj.ToString() == "0")
                    return;

                switch (stateItem.ItemName)
                {
                    case "01_1_136":
                        break;
                    case "01_1_142":
                        break;
                    case "01_1_152":
                        break;
                    case "01_1_170":
                        break;
                    case "01_1_178":
                        break;
                    case "01_1_186":
                        break;
                }
                //电控系统返回任务号9999
                string TaskNo = obj.ToString().PadLeft(4, '0');
                TaskDal taskDal = new TaskDal();
                string[] TaskInfo = taskDal.GetTaskInfo(TaskNo);
                DataTable dt = taskDal.TaskInfo(string.Format("TASK_ID='{0}'", TaskInfo[0]));
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    string taskType = dt.Rows[0]["TASK_TYPE"].ToString();
                    string ItemNo = "0";
                    string NextItemNo="1";
                    string CellCode = "";
                    switch(taskType)
                    {
                        //入库
                        case "11":
                            ItemNo = "2";
                            NextItemNo = "3";
                            CellCode = dr["CELL_CODE"].ToString();
                            break;                        
                        //盘点
                        case "13":
                            ItemNo = "3";
                            NextItemNo = "4";
                            CellCode = dr["CELL_CODE"].ToString();
                            break;
                        //移库
                        case "14":
                            ItemNo = "2";
                            NextItemNo = "3";
                            CellCode = dr["NEWCELL_CODE"].ToString();
                            break;
                    }
                    //更新路线完成状态
                    taskDal.UpdateTaskDetailState(string.Format("TASK_NO='{0}' AND ITEM_NO='{1}'", TaskNo, ItemNo), "2");

                    SysStationDal sysdal = new SysStationDal();
                    DataTable dtstation = sysdal.GetSationInfo(CellCode, "11","3");
                    if (dtstation.Rows.Count > 0)
                    {
                        //更新调度堆垛机的起始位置及目标地址。
                        taskDal.UpdateTaskDetailCrane(dtstation.Rows[0]["STATION_NO"].ToString(), CellCode, "0", dtstation.Rows[0]["CRANE_NO"].ToString(), string.Format("TASK_ID='{0}' AND ITEM_NO={1}", TaskInfo[0], NextItemNo));
                    }
                    string strWhere = string.Format("TASK_NO='{0}'AND DETAIL.STATE='0' and ITEM_NO='{1}'", TaskNo, NextItemNo);
                    DataTable dtInCrane = taskDal.CraneTaskIn(strWhere);
                    if (dtInCrane.Rows.Count > 0)
                        WriteToProcess("CraneProcess", "CraneInRequest", dtInCrane);
                }
            }
            catch (Exception e)
            {
                Logger.Error("THOK.XC.Process.Process_01.StockInStationProcess：" + e.Message);
            }
        }
    }
}
