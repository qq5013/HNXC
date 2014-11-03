﻿using System;
using System.Collections.Generic;
using System.Text;
using THOK.MCP;
using System.Data;
using THOK.XC.Process.Dal;

namespace THOK.XC.Process.Process_01
{
    public class NotReadBarcodeProcess : AbstractProcess
    {
        protected override void StateChanged(StateItem stateItem, IProcessDispatcher dispatcher)
        {
            /*  处理事项：
             * 一楼入库烟包处理
            */

            try
            {

                object obj = ObjectUtil.GetObject(stateItem.State);
                if (obj == null)
                    return;
                if (obj.ToString() == "0")
                    return;

                string strBadFlag = "";
                //其他情况电控报警处理
                switch (obj.ToString())
                {
                    case "1":
                        strBadFlag = "左边条码无法读取";
                        break;
                    case "2":
                        strBadFlag = "右边条码无法读取";
                        break;
                    case "3":
                        strBadFlag = "两边条码无法读取";
                        break;
                    case "4":
                        strBadFlag = "两边条码不一致";
                        break;
                    case "5":
                        Logger.Error("RFID故障");
                        return;
                    case "6":
                        Logger.Error("外形检测不符合");
                        return;
                }
                string strBarCode;
                string[] strMessage = new string[3];
                strMessage[0] = "3";
                strMessage[1] = strBadFlag;

                while ((strBarCode = FormDialog.ShowDialog(strMessage, null)) != "")
                {
                    byte[] b = Common.ConvertStringChar.stringToByte(strBarCode, 200);
                    WriteToService("StockPLC_01", "01_2_124_1", b); //写入条码  
                    WriteToService("StockPLC_01", "01_2_124_2", 1);//写入标识。
                    break;
                }
            }
            catch (Exception e)
            {
                Logger.Error("THOK.XC.Process.Process_01.NotReadBarcodeProcess:" + e.Message);
            }
        }
    }
}
