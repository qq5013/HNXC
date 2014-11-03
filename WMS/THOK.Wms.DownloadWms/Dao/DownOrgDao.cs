using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.WMS.DownloadWms.Dao
{
   public class DownOrgDao:BaseDao
    {
        /// <summary>
        /// �����λ��Ϣ
        /// </summary>
        public void Delete()
        {
            string sql = "DELETE DWV_OUT_ORG";
            this.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// ����������λ��Ϣ
        /// </summary>
        /// <returns></returns>
        public DataTable GetOrgInfo()
        {
            string sql = " SELECT * FROM V_WMS_ORG";
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// ������ݵ����ݿ�
        /// </summary>
        /// <param name="dt"></param>
        public void Insert(DataTable dt)
        {
            this.BatchInsert(dt, "DWV_OUT_ORG");
        }
    }
}
