using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using THOK.WMS.DownloadWms.Dao;

namespace THOK.WMS.DownloadWms.Bll
{
   public class DownSaleRegionBll
   {
       #region ��Ӫ��ϵͳ����Ӫ��������Ϣ

       /// <summary>
        /// ����Ӫ��������Ϣ
        /// </summary>
        /// <returns></returns>
        public bool DownOrgInfo()
        {
            bool tag = true;
            this.Delete();
            DataTable saleTable = this.GetSaleInfo();
            if (saleTable.Rows.Count > 0)
                this.Insert(saleTable);
            else
                tag = false;
            return tag;
        }

        /// <summary>
        /// ����Ӫ��������Ϣ
        /// </summary>
        /// <returns></returns>
       public DataTable GetSaleInfo()
        {
            using (PersistentManager dbPm = new PersistentManager("YXConnection"))
            {
                DownSaleRegionDao dao = new DownSaleRegionDao();
                dao.SetPersistentManager(dbPm);
                return dao.GetSaleInfo();
            }
        }

        /// <summary>
        /// ���Ӫ��������Ϣ
        /// </summary>
        public void Delete()
        {
            using (PersistentManager dbPm = new PersistentManager())
            {
                DownSaleRegionDao dao = new DownSaleRegionDao();
                dao.Delete();
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="orgDt"></param>
       public void Insert(DataTable saleTable)
        {
            using (PersistentManager dbPm = new PersistentManager())
            {
                DownSaleRegionDao dao = new DownSaleRegionDao();
                dao.Insert(saleTable);
            }
        }
        #endregion
    }
}
