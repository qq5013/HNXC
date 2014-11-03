using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;
using THOK.WMS.DownloadWms.Dao;

namespace THOK.WMS.DownloadWms.Bll
{
    public class DownOrgBll
    {
        #region ��Ӫ��ϵͳ����������λ��Ϣ

        /// <summary>
        /// ����������λ��Ϣ
        /// </summary>
        /// <returns></returns>
        public bool DownOrgInfo()
        {
            bool tag = true;
            this.Delete();
            DataTable orgdt = this.GetOrgInfo();
            if (orgdt.Rows.Count > 0)
                this.Insert(orgdt);
            else
                tag = false;
            return tag;
        }

        /// <summary>
        /// ����������λ��Ϣ
        /// </summary>
        /// <returns></returns>
        public DataTable GetOrgInfo()
        {
            using (PersistentManager dbPm = new PersistentManager("YXConnection"))
            {
                DownOrgDao dao = new DownOrgDao();
                dao.SetPersistentManager(dbPm);
                return dao.GetOrgInfo();
            }
        }

        /// <summary>
        /// �����λ��Ϣ
        /// </summary>
        public void Delete()
        {
            using (PersistentManager dbPm = new PersistentManager())
            {
                DownOrgDao dao = new DownOrgDao();
                dao.Delete();
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="orgDt"></param>
        public void Insert(DataTable orgDt)
        {
            using (PersistentManager dbPm = new PersistentManager())
            {
                DownOrgDao dao = new DownOrgDao();
                dao.Insert(orgDt);
            }
        }
        #endregion
    }
}
