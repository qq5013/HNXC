using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.WMS.DownloadWms.Dao
{
    public class DownDistDao : BaseDao
    {
       #region ��������������Ϣ
       
       /// <summary>
       /// ��������������Ϣ
       /// </summary>
       /// <returns></returns>
       public DataTable GetDistInfo()
       {
           string sql = " SELECT * FROM V_WMS_DIST_CTR";
           return this.ExecuteQuery(sql).Tables[0];
       }

       /// <summary>
       /// ���������Ϣ�����ݿ�
       /// </summary>
       /// <param name="distTable"></param>
       public void Insert(DataTable distTable)
       {
           this.BatchInsert(distTable, "DWV_OUT_DIST_CTR");
       }

       #endregion

       #region ��ѯ���ֲִ�����������Ϣ

       /// <summary>
       /// �������������Ϣ
       /// </summary>
       public void Delete()
       {
           string sql = "DELETE DWV_OUT_DIST_CTR";
           this.ExecuteNonQuery(sql);
       }

       /// <summary>
       /// ��ȡ�������ı���
       /// </summary>
       /// <returns></returns>
       public string GetCompany()
       {
           string sql = "SELECT COM_CODE FROM BI_COMPANY";
           return this.ExecuteScalar(sql).ToString();
       }

       #endregion

   }
}
