using System;
using System.Collections.Generic;
using System.Text;
using THOK.Util;
using System.Data;

namespace THOK.WMS.DownloadWms.Dao
{
    public class DownReachDao : BaseDao
   {
       #region �����ͻ�����

       /// <summary>
       /// �����ͻ�����
       /// </summary>
       /// <returns></returns>
       public DataTable GetReachInfo()
       {
           string sql = " SELECT * FROM V_WMS_DIST_STATION";
           return this.ExecuteQuery(sql).Tables[0];
       }

       /// <summary>
       /// �����ͻ����������ͻ�����
       /// </summary>
       /// <returns></returns>
       public DataTable GetReachInfo(string reachCode)
       {
           string sql = string.Format(" SELECT * FROM V_WMS_DIST_STATION WHERE {0}", reachCode);
           return this.ExecuteQuery(sql).Tables[0];
       }

       /// <summary>
       /// ��ѯ�ͻ�����
       /// </summary>
       /// <returns></returns>
       public DataTable QueryReachCode()
       {
           string sql = " SELECT DIST_STA_CODE FROM DWV_OUT_DIST_STATION";
           return this.ExecuteQuery(sql).Tables[0];
       }

       /// <summary>
       /// ������ݵ����ݿ�
       /// </summary>
       /// <param name="reachDt"></param>
       public void Insert(DataTable reachDt)
       {
           this.BatchInsert(reachDt, "DWV_OUT_DIST_STATION");
       }

       /// <summary>
       /// ����ͻ������
       /// </summary>
       public void Delete()
       {
          string sql = "DELETE DWV_OUT_DIST_STATION";
          this.ExecuteNonQuery(sql);
       }


       #endregion
   }
}
