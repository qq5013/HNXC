﻿using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;

namespace THOK.XC.Process.Process_02
{
    public class StockOutCarFinishProcess : AbstractProcess
    {
       
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  处理事项：
             * 
             *  stateItem.ItemName ：
             *  Init - 初始化。
             *      FirstBatch - 生成第一批入库请求任务。
             *      StockInRequest - 根据请求，生成入库任务。
             * 
             *  stateItem.State ：参数 - 请求的卷烟编码。        
            */
            object obj =stateItem.State;

            if (obj == null)
                return;
            if (obj.ToString() == "0")
                return;

            string FromStation="";
            string ToStation="";
            string WriteItem = "";
            try
            {
                switch (stateItem.ItemName)
                {
                    case "02_1_340":
                        FromStation = "340";
                        ToStation = "372";
                        WriteItem = "02_2_340";
                        break;
                    case "02_1_360":
                        FromStation = "360";
                        ToStation = "392";
                        WriteItem = "02_2_360";
                        break;                        
                }

                string TaskNo = obj.ToString().PadLeft(4, '0');
                TaskDal dal = new TaskDal();
                string[] strValue = dal.GetTaskOutInfo(TaskNo);
                if (!string.IsNullOrEmpty(strValue[0]))
                {
                    dal.UpdateTaskDetailState(string.Format("TASK_ID='{0}' AND ITEM_NO=3", strValue[0]), "2");
                    //
                    DataTable dt = dal.TaskInfo(string.Format("TASK_ID='{0}'", strValue[0]));

                    if (dt.Rows.Count > 0)
                    {
                        string ForderBillNo = dt.Rows[0]["FORDERBILLNO"].ToString();
                        string TaskID = dt.Rows[0]["TASK_ID"].ToString();
                        string TaskType = dt.Rows[0]["TASK_TYPE"].ToString();

                        int[] WriteValue = new int[4];
                        WriteValue[0] = int.Parse(TaskNo);
                        string LineNo = dt.Rows[0]["TARGET_CODE"].ToString().Trim();

                        SysCarAddressDal sdal = new SysCarAddressDal();
                        string[] OutStation = sdal.GetOutStation(LineNo);
                        ToStation = OutStation[2];

                        ////目标地址
                        //if (dt.Rows[0]["TARGET_CODE"].ToString().Trim() == "1001")
                        //    ToStation = "475"; //A线
                        //else if (dt.Rows[0]["TARGET_CODE"].ToString().Trim() == "1003")
                        //    ToStation = "440"; //B线
                        //else
                        //    ToStation = "412"; //C线

                        int[] OrderNo = dal.GetOrderNo(ForderBillNo, TaskID, TaskType);
                        WriteValue[1] = int.Parse(ToStation);
                        WriteValue[2] = 1; //烟包类型
                        WriteValue[3] = OrderNo[1]; //头尾标识

                        string barcode = dt.Rows[0]["PRODUCT_BARCODE"].ToString();
                        //string palletcode = dt.Rows[0]["PALLET_CODE"].ToString();
                        //MES工单号,长度30
                        string WO_OD = dt.Rows[0]["SOURCE_BILLNO"].ToString();
                        //byte[] b = new byte[230];
                        //for (int k = 0; k < 230; k++)
                        //    b[k] = 32;

                        //Common.ConvertStringChar.stringToByte(barcode, 200).CopyTo(b, 0);
                        //Common.ConvertStringChar.stringToByte(WO_OD, 30).CopyTo(b, 200);

                        long WriteBatchNo = long.Parse(dal.GetBatchNo(ForderBillNo));
                        //Logger.Info("开始写入PLC");

                        WriteToService("StockPLC_02", WriteItem + "_1", WriteValue);
                        //WriteToService("StockPLC_02", WriteItem + "_2", b);
                        WriteToService("StockPLC_02", WriteItem + "_2", barcode + WO_OD);
                        WriteToService("StockPLC_02", WriteItem + "_3", OrderNo[2]);
                        WriteToService("StockPLC_02", WriteItem + "_4", WriteBatchNo);                        
                        WriteToService("StockPLC_02", WriteItem + "_5", OrderNo[0]);
                        WriteToService("StockPLC_02", WriteItem + "_6", 1);
                        Logger.Info("任务号:" + TaskNo + ";目标地址:" + ToStation + ";头尾标识:" + OrderNo[1] + ";序号:" + OrderNo[0] + ";批次号:" + WriteBatchNo + "已下给输送机");
                    }
                    //if (dt.Rows.Count > 0)
                    //{
                    //    int[] WriteValue = new int[3];
                    //    WriteValue[0] = int.Parse(TaskNo);
                    //    WriteValue[1] = int.Parse(FromStation);
                    //    WriteValue[2] = int.Parse(ToStation);

                    //    string barcode = dt.Rows[0]["PRODUCT_BARCODE"].ToString();
                    //    string palletcode = dt.Rows[0]["PALLET_CODE"].ToString();

                    //    byte[] b = new byte[290];
                    //    for (int k = 0; k < 290; k++)
                    //        b[k] = 32;

                    //    WriteToService("StockPLC_02", WriteItem + "_1", WriteValue);
                    //    Common.ConvertStringChar.stringToByte(barcode, 200).CopyTo(b, 0);
                    //    Common.ConvertStringChar.stringToByte(palletcode, 90).CopyTo(b, 200);

                    //    WriteToService("StockPLC_02", WriteItem + "_2", b);
                    //    WriteToService("StockPLC_02", WriteItem + "_3", 1);
                    //}
                    dal.UpdateTaskDetailStation(FromStation, ToStation, "1", string.Format("TASK_ID='{0}' AND ITEM_NO=4", strValue[0]));
                }
            }

            catch (Exception e)
            {
                Logger.Error("THOK.XC.Process.Process_02.StockOutCarFinishProcess，原因：" + e.Message);
            }
        }
    }
}
