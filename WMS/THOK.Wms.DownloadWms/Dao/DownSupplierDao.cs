using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.WMS.DownloadWms.Dao
{
    public class DownSupplierDao : BaseDao
    {
        #region ���˳�Ӫϵͳ�����ع�Ӧ������

        /// <summary>
        /// ���ع�Ӧ������
        /// </summary>
        /// <returns></returns>
        public DataTable GetSupplierInfo(string spplierCode)
        {
            string sql = string.Format("SELECT * FROM V_WMS_FACTORY WHERE {0}",spplierCode);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// �����ݲ������ݿ�
        /// </summary>
        /// <param name="ds"></param>
        public void Insert(DataSet ds)
        {
            BatchInsert(ds.Tables["BI_SUPPLIER_INSERT"], "BI_SUPPLIER");
        }

        /// <summary>
        /// ��ѯ��Ӧ�̱��
        /// </summary>
        /// <returns></returns>
        public DataTable GetSupplierCode()
        {
            string sql = "SELECT SUPPLIERCODE FROM BI_SUPPLIER";
            return this.ExecuteQuery(sql).Tables[0];
        }
        #endregion
    }
}
