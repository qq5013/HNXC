using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.WMS.DownloadWms.Dao
{
    public class DownDeptDao : BaseDao
    {
        #region Ӫ��ϵͳ���ز�����Ϣ

        /// <summary>
        /// ���ز�����Ϣ
        /// </summary>
        /// <returns></returns>
        public DataTable GetDeptInfo(string deptCode)
        {
            string sql = string.Format("SELECT DEPT_CODE,DEPT_NAME,UP_CODE,UP_DOWN_CODE,DEPT_TYPE,ISACTIVE FROM V_WMS_DEPT WHERE {0}", deptCode);
            return this.ExecuteQuery(sql).Tables[0];
        }

        /// <summary>
        /// �Ѳ�����Ϣ�������ݿ�
        /// </summary>
        /// <param name="ds"></param>
        public void Insert(DataSet ds)
        {
            BatchInsert(ds.Tables["BI_DEPARTMENT"], "BI_DEPARTMENT");
        }
        #endregion

        #region ��ѯ�ִ�������Ϣ
        /// <summary>
        /// ��ѯ�ִ����ű��
        /// </summary>
        /// <returns></returns>
        public DataTable GetDeptCode()
        {
            string sql = "SELECT DEPTCODE FROM BI_DEPARTMENT";
            return this.ExecuteQuery(sql).Tables[0];
        }

        #endregion
    }
}
