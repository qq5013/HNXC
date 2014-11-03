using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;

namespace THOK.XC.Process.Process_02
{
    public class CheckProcess : AbstractProcess
    {
        private System.Timers.Timer PalletTime = new System.Timers.Timer();

        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  处理事项：
             * 二楼出库条码校验
             *  
            */
            try
            {
                object[] obj = ObjectUtil.GetObjects(stateItem.State);
                if (obj[0] == null)
                    return;
                if (obj[0].ToString() == "0")
                    return;

                string TaskNo = obj[0].ToString().PadLeft(4, '0');
                TaskDal dal = new TaskDal();
                string[] strValue = dal.GetTaskOutInfo(TaskNo);

                string FromStation = "";
                string ToStation = "";
                string WriteItem = "";
                string ReadItem = "";
                switch (stateItem.ItemName)
                {
                    case "02_1_340_1":
                        FromStation = "340";
                        WriteItem = "02_2_340";
                        ReadItem = "02_1_340_";
                        break;
                    case "02_1_360_1":
                        FromStation = "360";
                        WriteItem = "02_2_360";
                        ReadItem = "02_1_360_";
                        break;
                }
                object objCheck = ObjectUtil.GetObject(WriteToService("StockPLC_02", ReadItem + "2"));
                if (objCheck.ToString() == "0")
                {
                    string BarCode = Common.ConvertStringChar.BytesToString(ObjectUtil.GetObjects(WriteToService("StockPLC_02", ReadItem + "3")));
                    dal.UpdateTaskCheckBarCode(strValue[0], BarCode);
                }
                //读取DB60序号是否已下
                //object objNo = ObjectUtil.GetObject(WriteToService("StockPLC_02", WriteItem + "_5"));
                //if (objNo.ToString().Trim().Length > 0)
                //{
                    WriteToService("StockPLC_02", WriteItem + "_6", 1);
                    return;
                //}

                //否则重新下一次任务给输送机，保证信息准确
                if (!string.IsNullOrEmpty(strValue[0]))
                {
                    dal.UpdateTaskDetailState(string.Format("TASK_ID='{0}' AND ITEM_NO=3", strValue[0]), "3");

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

                        int[] OrderNo = dal.GetOrderNo(ForderBillNo, TaskID, TaskType);
                        WriteValue[1] = int.Parse(ToStation);
                        WriteValue[2] = 1; //烟包类型
                        WriteValue[3] = OrderNo[1]; //头尾标识

                        string barcode = dt.Rows[0]["PRODUCT_BARCODE"].ToString();

                        //MES工单号,长度30
                        string WO_OD = dt.Rows[0]["SOURCE_BILLNO"].ToString();
                        byte[] b = new byte[230];
                        for (int k = 0; k < 230; k++)
                            b[k] = 32;

                        Common.ConvertStringChar.stringToByte(barcode, 200).CopyTo(b, 0);
                        Common.ConvertStringChar.stringToByte(WO_OD, 30).CopyTo(b, 200);
                        //Logger.Info("开始写入PLC");
                        long WriteBatchNo = long.Parse(dal.GetBatchNo(ForderBillNo));
                        WriteToService("StockPLC_02", WriteItem + "_1", WriteValue);
                        WriteToService("StockPLC_02", WriteItem + "_2", b);
                        WriteToService("StockPLC_02", WriteItem + "_3", OrderNo[2]);
                        WriteToService("StockPLC_02", WriteItem + "_4", WriteBatchNo);
                        WriteToService("StockPLC_02", WriteItem + "_5", OrderNo[0]);
                        WriteToService("StockPLC_02", WriteItem + "_6", 1);
                        Logger.Info("条码校验时重新下任务,任务号:" + TaskNo + 
                            ";目标地址:" + ToStation + 
                            ";头尾标识:" + OrderNo[1].ToString() + 
                            ";序号:" + OrderNo[0].ToString());
                    }
                    dal.UpdateTaskDetailStation(FromStation, ToStation, "1", string.Format("TASK_ID='{0}' AND ITEM_NO=4", strValue[0]));
                }

            }
            catch (Exception e)
            {
                Logger.Error("THOK.XC.Process.Process_02.CheckProcess，原因：" + e.Message);
            }
        }

    }
}
