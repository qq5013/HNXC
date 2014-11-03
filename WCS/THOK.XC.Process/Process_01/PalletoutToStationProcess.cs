﻿using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;

namespace THOK.XC.Process.Process_01
{
    public class PalletOutToStationProcess : AbstractProcess
    {
        /*  处理事项：
         *  空托盘组出库到达158，200
        */
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            try
            {

                object obj = ObjectUtil.GetObject(stateItem.State);
                //return;
                if (obj == null)
                    return;
                if (obj.ToString() == "0")
                    return;

                string writeItem = "";
                switch (stateItem.ItemName)
                {
                    case "01_1_158_2":
                        writeItem = "01_2_158_1";
                        break;
                    case "01_1_200_2":
                        writeItem = "01_2_200_1";
                        break;
                }
                string TaskNo = ((short)obj).ToString().PadLeft(4, '0');
                //根据任务号，获取TaskID及BILL_NO
                TaskDal dal = new TaskDal();
                string[] strInfo = dal.GetTaskOutInfo(TaskNo);
                if (!string.IsNullOrEmpty(strInfo[0]))
                {
                    //更新路线状态
                    dal.UpdateTaskDetailState(string.Format("TASK_ID='{0}' AND ITEM_NO=2", strInfo[0]), "2");
                    dal.UpdateTaskState(strInfo[0], "2");
                    //更新BillMaster状态完成
                    BillDal billdal = new BillDal();
                    billdal.UpdateInBillMasterFinished(strInfo[1], "0");
                    //通知电控，空托盘组到达158,200    
                    WriteToService("StockPLC_01", writeItem, 1);
                }
            }
            catch (Exception e)
            {
                Logger.Error("THOK.XC.Process.Process_01.PalletOutToStationProcess：" + e.Message);
            }
        }
    }

}
