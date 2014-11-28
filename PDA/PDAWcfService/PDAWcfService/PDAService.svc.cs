using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
//using System.Data.OracleClient;
using System.Configuration;
using Oracle.DataAccess.Client;

namespace PDAWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PDAService" in code, svc and config file together.
    public class PDAService : IPDAService
    {
        //OracleConnection conn;
        public PDAService()
        {
            OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["connString"]);
            conn.Open();
        }
        public DataSet GetBarcodeInfo(string Barcode)
        {
            OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["connString"]);
            conn.Open();

            string strSQL = string.Format("SELECT * FROM VBARCODEINFO WHERE PRODUCT_BARCODE='{0}'",Barcode);
            OracleDataAdapter oda = new OracleDataAdapter(strSQL, conn);
            DataSet ds = new DataSet();
            oda.Fill(ds);
            return ds;
        }
        public int SetBarcodeInfo(string Barcode,string Checker,string Result,string ChannelNo)
        {
            OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["connString"]);
            conn.Open();

            string strSQL = string.Format("UPDATE WCS_PRODUCT_CACHE SET CHECKER='{0}',CHECK_RESULT='{1}',CHECK_CHANNEL_NO='{2}' WHERE PRODUCT_BARCODE='{3}'", Checker,Result,ChannelNo,Barcode);
            strSQL = string.Format("UPDATE WCS_TASK SET CHECK_DATE=SYSDATE,CHECKER='{0}',CHECK_RESULT='{1}',CHECK_CHANNEL_NO='{2}' WHERE PRODUCT_BARCODE='{3}'", Checker, Result, ChannelNo, Barcode);
            OracleCommand cmd = new OracleCommand(strSQL, conn);
            return cmd.ExecuteNonQuery();
        }
        public bool ValidateUser(string userName, string password)
        {
            OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["connString"]);
            conn.Open();

            string strPwd = EncryptPassword(password);
            string strSQL = string.Format("SELECT * FROM AUTH_USER WHERE USER_NAME='{0}' AND IS_LOCK='0'", userName);
            OracleDataAdapter oda = new OracleDataAdapter(strSQL, conn);
            DataSet ds = new DataSet();
            oda.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
                return strPwd == ds.Tables[0].Rows[0]["PWD"].ToString();
            else
                return false;
        }

        //对用户输入的密码进行加密, 返回加密后的字符串
        private string EncryptPassword(string password)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = x.ComputeHash(data);
            return System.Text.Encoding.ASCII.GetString(data);
        }

    }
}
