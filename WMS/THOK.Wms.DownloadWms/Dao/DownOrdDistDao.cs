using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.WMS.DownloadWms.Dao
{
    public class DownOrdDistDao : BaseDao
    {
        /// <summary>
        /// ����Ӫ�����䳵����������
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataTable GetDistBillMaster(string bistBillId)
        {
            string sql = string.Format("SELECT * FROM V_DWV_ORD_DIST_BILL WHERE {0} ", bistBillId);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// ����Ӫ�����䳵��ϸ������
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataTable GetDistBillDetail(string bistBillId)
        {
            string sql = string.Format(" SELECT * FROM V_WMS_DIST_BILL_DETAIL WHERE {0} ", bistBillId);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// ��ѯ�����ع����䳵������
        /// </summary>
        /// <returns></returns>
        public DataTable QueryOrgDistCode()
        {
            string sql = "SELECT DIST_BILL_ID FROM DWV_ORD_DIST_BILL";
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// �����䳵����Ϣ�����ֲִ�
        /// </summary>
        /// <param name="bistBillTable"></param>
        public void Insert(DataTable bistBillMasterTable)
        {
            this.BatchInsert(bistBillMasterTable, "WMS_DIST_BILL");
        }
    }
}
