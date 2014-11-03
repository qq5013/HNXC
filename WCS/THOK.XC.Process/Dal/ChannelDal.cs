using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.XC.Process.Dao;
using THOK.Util;

namespace THOK.XC.Process.Dal
{
    public class ChannelDal : BaseDal
    {

        /// <summary>
        /// ���仺��������뻺�浽�������ػ����ID��
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public string InsertChannel(string TaskID, string Bill_No)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                string strChannel_No = "";
                TaskDao dao = new TaskDao();
                DataTable dt = dao.TaskInfo(string.Format("TASK_ID='{0}'", TaskID));
                string Line_No = dt.Rows[0]["TARGET_CODE"].ToString().Trim();
                string BillNo = dt.Rows[0]["BILL_NO"].ToString();

                ChannelDao Cdao = new ChannelDao();
                dt = Cdao.ChannelInfo(Line_No);

                if (!Cdao.HasTaskInChannel(TaskID))
                {
                    switch (Line_No)
                    {
                        case "01":
                        case "02":
                            strChannel_No = Cdao.GetChannelNoByBillNo(BillNo);
                            if (strChannel_No == "")
                            {
                                DataRow[] drs = dt.Select("QTY=0 AND QTY<CACHE_QTY", "ORDERNO");
                                if (drs.Length > 0)
                                    strChannel_No = drs[0]["CHANNEL_NO"].ToString();
                            }
                            else
                            {
                                DataRow[] drs = dt.Select(string.Format("QTY<CACHE_QTY and CHANNEL_NO='{0}'", strChannel_No), "ORDERNO");
                                if (drs.Length == 0)
                                {
                                     drs = dt.Select("QTY=0 AND QTY<CACHE_QTY", "ORDERNO");
                                    if (drs.Length > 0)
                                        strChannel_No = drs[0]["CHANNEL_NO"].ToString();
                                }
                            }
                            break;
                        case "03":
                            if (int.Parse(dt.Rows[0]["CACHE_QTY"].ToString()) - int.Parse(dt.Rows[0]["QTY"].ToString()) > 15)
                            {
                                strChannel_No = dt.Rows[0]["CHANNEL_NO"].ToString();
                            }
                            break;
                    }

                    if (strChannel_No != "")
                    {
                        Cdao.InsertChannel(TaskID, Bill_No, strChannel_No);
                    }
                }

                return strChannel_No;
            }
        }

        /// <summary>
        /// ���½��뻺���ʱ�䣬��ORDER_NO     
        /// </summary>
        /// <returns></returns>
        public int UpdateInChannelTime(string TaskID, string Bill_No, string ChannelNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                int strValue = 0;
                ChannelDao dao = new ChannelDao();
                int count = dao.ProductCount(Bill_No);
                TaskDao tdao = new TaskDao();

                int taskCount = tdao.GetTaskCount(Bill_No);
                if (count == 0)
                    strValue = 1;
                if (count == taskCount - 1)
                    strValue = 2;
                dao.UpdateInChannelTime(TaskID, Bill_No, ChannelNo);
                return strValue;
            }
        }
        /// <summary>
        /// �ж��Ƿ��Ѿ��ڻ�����У�true ����
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public bool HasTaskInChannel(string TaskID)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                bool blnValue = false;
                 ChannelDao dao = new ChannelDao();
                 blnValue= dao.HasTaskInChannel(TaskID);
                 return blnValue;
            }
 
        }
          /// <summary>
        /// ���³���
        /// </summary>
        public void UpdateOutChannelTime(string TaskID)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao dao = new ChannelDao();
                dao.UpdateOutChannelTime(TaskID);
            }
        }
        /// <summary>
        /// ��ȡ����Ļ����
        /// </summary>
        /// <param name="TaskNO"></param>
        /// <param name="BillNo"></param>
        /// <returns></returns>
        public string GetChannelFromTask(string TaskNO, string BillNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                ChannelDao dao = new ChannelDao();
                return dao.GetChannelFromTask(TaskNO, BillNo);
            }
        }

        /// <summary>
        /// ���³���
        /// </summary>
        public int UpdateInChannelAndTime(string TaskID, string Bill_No, string ChannelNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                int strValue = 0;
                ChannelDao dao = new ChannelDao();
                int count = dao.ProductCount(Bill_No);
                TaskDao tdao = new TaskDao();

                int taskCount = tdao.GetTaskCount(Bill_No);
                if (count == 0)
                    strValue = 1;
                if (count == taskCount - 1)
                    strValue = 2;
                dao.UpdateInChannelAndTime(TaskID, Bill_No, ChannelNo);
                return strValue;
            }
        }

        /// <summary>
        /// ���ݵ��ţ���ȡ������Ļ������š�
        /// </summary>
        /// <param name="BillNo"></param>
        /// <returns></returns>
        public string GetChannelNoByBillNo(string BillNo)
        {
            using (PersistentManager pm = new PersistentManager())
            {
                string strChannelNo = "";
                ChannelDao dao = new ChannelDao();
                strChannelNo = dao.GetChannelNoByBillNo(BillNo);

                return strChannelNo;
            }

        }
       
    }
}
