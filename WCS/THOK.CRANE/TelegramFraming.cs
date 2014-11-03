﻿using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.CRANE
{
    public class TelegramFraming
    {
        
        public delegate Telegram TelegramDataDelegate(Telegram tgm,TelegramData tgd);

        /// <summary>
        /// 报文方法
        /// </summary>
        /// <param name="tgm">telegram</param>
        /// <param name="tgd">Application telegram data</param>
        /// <param name="tdd">TelegramDelegate传递方法名</param>
        public string DataFraming(string sequenceno ,TelegramData tgd, TelegramDataDelegate TelegramDelegate)
        {
            Telegram tgm = new Telegram();
            //调用指令序号方法
            tgm.RequestFlag = byte.Parse(sequenceno.Substring(0, 1));
            tgm.SequenceNo = sequenceno.Substring(1, 4);
            tgm = TelegramDelegate(tgm, tgd);
            return tgm.ToString();
            //发送报文
        }
        /// <summary>
        /// ARQ指令
        /// </summary>
        /// <param name="tgm"></param>
        /// <param name="tgd"></param>
        /// <returns></returns>
        public Telegram TelegramARQ(Telegram tgm,TelegramData tgd)
        {
            tgm.TelegramData = "ARQ";
            tgm.TelegramData += tgd.CraneNo;
            tgm.TelegramData += tgd.AssignmentID;
            tgm.TelegramData += tgd.AssignmentType;
            tgm.TelegramData += "01";
            tgm.TelegramData += tgd.StartPosition;
            tgm.TelegramData += tgd.DestinationPosition;
            tgm.TelegramData += "REHIFUFU";

            return tgm;
        }
        /// <summary>
        /// DER指令
        /// </summary>
        /// <param name="tgm"></param>
        /// <param name="tgd"></param>
        /// <returns></returns>
        public Telegram TelegramDER(Telegram tgm, TelegramData tgd)
        {
            tgm.TelegramData = "DER";
            tgm.TelegramData += tgd.CraneNo;
            tgm.TelegramData += tgd.AssignmentID;

            return tgm;
        }
        /// <summary>
        /// STA指令
        /// </summary>
        /// <param name="tgm"></param>
        /// <param name="tgd"></param>
        /// <returns></returns>
        public Telegram TelegramSTA(Telegram tgm, TelegramData tgd)
        {
            tgm.TelegramData = "STA";
            tgm.TelegramData += tgd.CraneNo;

            return tgm;
        }
        /// <summary>
        /// STO指令
        /// </summary>
        /// <param name="tgm"></param>
        /// <param name="tgd"></param>
        /// <returns></returns>
        public Telegram TelegramSTO(Telegram tgm, TelegramData tgd)
        {
            tgm.TelegramData = "STO";
            tgm.TelegramData += tgd.CraneNo;

            return tgm;
        }
        /// <summary>
        /// CRQ指令
        /// </summary>
        /// <param name="tgm"></param>
        /// <param name="tgd"></param>
        /// <returns></returns>
        public Telegram TelegramCRQ(Telegram tgm, TelegramData tgd)
        {
            tgm.TelegramData = "CRQ";
            tgm.TelegramData += tgd.CraneNo;

            return tgm;
        }
        /// <summary>
        /// DUA指令
        /// </summary>
        /// <param name="tgm"></param>
        /// <param name="tgd"></param>
        /// <returns></returns>
        public Telegram TelegramDUA(Telegram tgm, TelegramData tgd)
        {
            tgm.TelegramData = "DUA00000";

            return tgm;
        }
        /// <summary>
        /// DUM指令
        /// </summary>
        /// <param name="tgm"></param>
        /// <param name="tgd"></param>
        /// <returns></returns>
        public Telegram TelegramDUM(Telegram tgm, TelegramData tgd)
        {
            tgm.TelegramData = "DUM00000";

            return tgm;
        }
        /// <summary>
        /// ACK指令
        /// </summary>
        /// <param name="tgm"></param>
        /// <param name="tgd"></param>
        /// <returns></returns>
        public Telegram TelegramACK(Telegram tgm, TelegramData tgd)
        {
            tgm.TelegramData = "ACK0";
            tgm.TelegramData += tgd.SequenceNo;
            return tgm;
        }
        /// <summary>
        /// SYN指令
        /// </summary>
        /// <param name="tgm"></param>
        /// <param name="tgd"></param>
        /// <returns></returns>
        public Telegram TelegramSYN(Telegram tgm, TelegramData tgd)
        {
            tgm.TelegramData = "SYN00000";

            return tgm;
        }
    }
}
