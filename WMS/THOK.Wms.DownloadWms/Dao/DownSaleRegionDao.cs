using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.WMS.DownloadWms.Dao
{
    public class DownSaleRegionDao : BaseDao
    {
        /// <summary>
        /// ���Ӫ��������Ϣ
        /// </summary>
        public void Delete()
        {
            string sql = "DELETE WMS_SALE_REGION";
            this.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// ����Ӫ��������Ϣ
        /// </summary>
        /// <returns></returns>
        public DataTable GetSaleInfo()
        {
            string sql = "SELECT * FROM V_WMS_SALE_REGION";
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// ������ݵ����ݿ�
        /// </summary>
        /// <param name="dt"></param>
        public void Insert(DataTable saleTable)
        {
            this.BatchInsert(saleTable, "WMS_SALE_REGION");
        }
    }
}
