﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.XC.Process.Dao;
using THOK.Util;

namespace THOK.XC.Process.Dal
{
    public class CraneErrMessageDal : BaseDal
    {
        /// <summary>
        /// 返回堆垛机错误列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetErrMessageList()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                CraneErrMessageDao dao = new CraneErrMessageDao();
                return dao.GetErrMessageList();
            }
        }
        /// <summary>
        /// 返回堆垛机列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetCraneList()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                CraneErrMessageDao dao = new CraneErrMessageDao();
                return dao.GetCraneList();
            }
        }
        /// <summary>
        /// 返回堆垛机列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetActiveCrane()
        {
            using (PersistentManager pm = new PersistentManager())
            {
                CraneErrMessageDao dao = new CraneErrMessageDao();
                return dao.GetActiveCrane();
            }
        }
    }
}