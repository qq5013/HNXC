﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;

namespace THOK.XC.Process.Dao
{
    public class CellDao : BaseDao
    {
        /// <summary>
        /// 出库-- 货位解锁
        /// </summary>
        /// <param name="strCell"></param>
        public void UpdateCellOutFinishUnLock(string strCell)
        {
            string strSQL = string.Format("UPDATE CMD_CELL SET IS_LOCK='0',PRODUCT_CODE='',PRODUCT_BARCODE='',SCHEDULE_NO='',REAL_WEIGHT=0,PALLET_CODE='',BILL_NO='',IN_DATE=NULL,CELL_STATE='' WHERE CELL_CODE='{0}' ", strCell);
            ExecuteNonQuery(strSQL);
        }

        /// <summary>
        /// 入库-- 更新货位存储信息
        /// </summary>
        /// <param name="strCell"></param>
        public void UpdateCellInFinishUnLock(string TaskID)
        {
            string strSQL = string.Format("UPDATE CMD_CELL CELL " +
                                          "SET (PRODUCT_CODE,PRODUCT_BARCODE,REAL_WEIGHT,PALLET_CODE,BILL_NO,IN_DATE,IS_LOCK,CELL_STATE,ERROR_FLAG)= " +
                                          "(SELECT PRODUCT_CODE,PRODUCT_BARCODE,REAL_WEIGHT,PALLET_CODE,DECODE(SOURCE_BILLNO,NULL,BILL_NO,SOURCE_BILLNO) AS BILL_NO," +
                                          "SYSDATE AS IN_DATE,'0','0','0' FROM VIEW_WCS_TASK TASK WHERE CELL.CELL_CODE=TASK.CELL_CODE AND TASK.TASK_ID='{0}' ) " +
                                          "WHERE EXISTS(SELECT 1 FROM VIEW_WCS_TASK T WHERE CELL.CELL_CODE=T.CELL_CODE AND T.TASK_ID='{0}' ) ", TaskID);

            ExecuteNonQuery(strSQL);
        }
        /// <summary>
        /// 入库-- 更新货位存储信息
        /// </summary>
        /// <param name="strCell"></param>
        public void UpdateCellMoveFinishUnLock(string TaskID)
        {
            string strSQL = string.Format("UPDATE CMD_CELL CELL " +
                                          "SET (PRODUCT_CODE,PRODUCT_BARCODE,REAL_WEIGHT,PALLET_CODE,BILL_NO,IN_DATE,IS_LOCK,CELL_STATE)= " +
                                          "(SELECT PRODUCT_CODE,PRODUCT_BARCODE,REAL_WEIGHT,PALLET_CODE,DECODE(SOURCE_BILLNO,NULL,BILL_NO,SOURCE_BILLNO) AS BILL_NO," +
                                          "SYSDATE AS IN_DATE,'0','0' FROM WCS_TASK TASK WHERE CELL.CELL_CODE=TASK.NEWCELL_CODE AND TASK.TASK_ID='{0}' ) " +
                                          "WHERE EXISTS(SELECT 1 FROM WCS_TASK T WHERE CELL.CELL_CODE=T.NEWCELL_CODE AND T.TASK_ID='{0}' ) ", TaskID);

            ExecuteNonQuery(strSQL);
        }
        /// <summary>
        /// 货位锁定
        /// </summary>
        /// <param name="strCell"></param>
        public void UpdateCellLock(string strCell)
        {
            string strSQL = string.Format("UPDATE CMD_CELL SET IS_LOCK='1' WHERE CELL_CODE='{0}' ", strCell);
            ExecuteNonQuery(strSQL);
        }
        /// <summary>
        /// 货位解锁
        /// </summary>
        /// <param name="strCell"></param>
        public void UpdateCellUnLock(string strCell)
        {
            string strSQL = string.Format("UPDATE CMD_CELL SET IS_LOCK='0' WHERE CELL_CODE='{0}' ", strCell);
            ExecuteNonQuery(strSQL);
        }
        /// <summary>
        /// 货位解除异常
        /// </summary>
        /// <param name="strCell"></param>
        public void UpdateCellClearError(string strCell,bool IsAll)
        {
            string strSQL = "";
            if(!IsAll)
                strSQL = string.Format("UPDATE CMD_CELL SET ERROR_FLAG='0',MEMO=NULL WHERE CELL_CODE='{0}' ", strCell);
            else
                strSQL = string.Format("UPDATE CMD_CELL SET IS_LOCK='0',PRODUCT_CODE='',PRODUCT_BARCODE='',SCHEDULE_NO='',REAL_WEIGHT=0,PALLET_CODE='',BILL_NO='',IN_DATE=NULL,CELL_STATE='',ERROR_FLAG='0',MEMO=NULL WHERE CELL_CODE='{0}' ", strCell);
            ExecuteNonQuery(strSQL);
        }
        public void UpdateCellNewPalletCode(string CellCode, string NewPalletCode)
        {
            string strSQL = string.Format("UPDATE CMD_CELL SET MEMO='货位RFID信息不匹配', NEW_PALLET_CODE ='{0}',ERROR_FLAG='1' WHERE CELL_CODE='{1}' ", NewPalletCode, CellCode);
            ExecuteNonQuery(strSQL);
        }

        public void UpdateCellErrFlag(string CellCode, string ErrMsg)
        {
            string strSQL = string.Format("UPDATE CMD_CELL SET MEMO='{0}',ERROR_FLAG='1' WHERE CELL_CODE='{1}' ", ErrMsg, CellCode);
            ExecuteNonQuery(strSQL);
        }
        public void UpdateCell(string CellCode, string BillNo,string ProductCode,string ProductBarcode,float RealWeight,string IsLock,string IsActive,string IsError)
        {
            string strSQL = string.Format("UPDATE CMD_CELL SET BILL_NO='{0}',PRODUCT_CODE='{1}',PRODUCT_BARCODE='{2}'," +
                                          "REAL_WEIGHT={3},IS_LOCK='{4}',IS_ACTIVE='{5}',ERROR_FLAG='{6}',CELL_STATE='' WHERE CELL_CODE='{7}'",
                                          BillNo, ProductCode, ProductBarcode, RealWeight, IsLock, IsActive, IsError,CellCode);
            ExecuteNonQuery(strSQL);
        }
        public DataTable GetCellInfo(string CellCode)
        {
            string strSQL = string.Format("SELECT * FROM CMD_CELL WHERE CELL_CODE='{1}' ", CellCode);
            return ExecuteQuery(strSQL).Tables[0];
        }

        public DataTable Find()
        {
            string sql = @"SELECT * FROM CMD_CELL A
                           LEFT JOIN CMD_AREA B ON A.AREA_CODE=B.AREA_CODE
                           LEFT JOIN CMD_SHELF C ON A.SHELF_CODE=C.SHELF_CODE
                           LEFT JOIN CMD_PRODUCT D ON A.PRODUCT_CODE=D.PRODUCT_CODE
                           ORDER BY C.AREA_CODE,A.SHELF_CODE,CELL_CODE";
            return ExecuteQuery(sql).Tables[0];
        }

        public DataTable GetShelf()
        {
            string sql = "SELECT * FROM CMD_SHELF ORDER BY SHELF_CODE";
            return ExecuteQuery(sql).Tables[0];
        }
        public void UpdateCellRemoveFinish(string NewCellCode, string oldCellCode)
        {
            string strSQL = string.Format("UPDATE CMD_CELL CELL " +
                                          "SET (PRODUCT_CODE,PRODUCT_BARCODE,REAL_WEIGHT,PALLET_CODE,BILL_NO,IN_DATE,IS_LOCK)= " +
                                          "(SELECT PRODUCT_CODE,PRODUCT_BARCODE,REAL_WEIGHT,PALLET_CODE,BILL_NO,IN_DATE,'0' FROM CMD_CELL WHERE CELL_CODE='{0}') " +
                                          "WHERE CELL.CELL_CODE='{1}'", oldCellCode, NewCellCode);
            ExecuteNonQuery(strSQL);            
        }
        public void UpdateNewCell(string NewCellCode, string oldCellCode)
        {
            string strSQL = string.Format("UPDATE CMD_CELL CELL " +
                                          "SET (PRODUCT_CODE,PRODUCT_BARCODE,REAL_WEIGHT,PALLET_CODE,BILL_NO,IN_DATE)= " +
                                          "(SELECT PRODUCT_CODE,PRODUCT_BARCODE,REAL_WEIGHT,PALLET_CODE,BILL_NO,IN_DATE FROM CMD_CELL WHERE CELL_CODE='{0}') " +
                                          "WHERE CELL.CELL_CODE='{1}'", oldCellCode, NewCellCode);
            ExecuteNonQuery(strSQL);
        }
        /// <summary>
        /// 获取空货位
        /// </summary>
        /// <returns></returns>
        public DataTable GetEmptyCell(string CraneNo)
        {
            string strSQL = string.Format("SELECT * FROM CMD_CELL " + 
                                          "INNER JOIN CMD_SHELF ON CMD_CELL.SHELF_CODE=CMD_SHELF.SHELF_CODE " +
                                          "WHERE CMD_CELL.IS_ACTIVE='1' AND CMD_CELL.IS_LOCK='0' AND CMD_CELL.PRODUCT_CODE IS NULL AND CMD_SHELF.CRANE_NO='{0}' " +
                                          "ORDER BY CELL_CODE",CraneNo);
            return ExecuteQuery(strSQL).Tables[0];
        }
        /// <summary>
        /// 获取存货货位
        /// </summary>
        /// <returns></returns>
        public DataTable GetBatchCell(string CigaretteCode, string FormulaCode, string ProductCode, string CraneNo)
        {
            string strSQL;
            if(ProductCode!="0000")
                strSQL = string.Format("SELECT CMD_CELL.*,CMD_PRODUCT.PRODUCT_NAME,CMD_CIGARETTE.CIGARETTE_NAME,'{1}' FORMULA_NAME " + 
                            "FROM CMD_CELL " +
                            "INNER JOIN VIEW_BILL_MAST ON CMD_CELL.BILL_NO=VIEW_BILL_MAST.BILL_NO AND VIEW_BILL_MAST.CIGARETTE_CODE='{0}' AND VIEW_BILL_MAST.FORMULA_CODE='{1}' " +
                            "LEFT JOIN CMD_CIGARETTE ON VIEW_BILL_MAST.CIGARETTE_CODE= CMD_CIGARETTE.CIGARETTE_CODE " + 
                            "LEFT JOIN CMD_PRODUCT ON CMD_CELL.PRODUCT_CODE=CMD_PRODUCT.PRODUCT_CODE " +
                            "LEFT JOIN CMD_SHELF ON CMD_CELL.SHELF_CODE=CMD_SHELF.SHELF_CODE " +
                            "WHERE CMD_CELL.IS_ACTIVE='1' AND CMD_CELL.IS_LOCK='0' AND CMD_CELL.ERROR_FLAG='0' AND CMD_CELL.PRODUCT_CODE='{2}' AND CMD_SHELF.CRANE_NO='{3}' " +
                            "ORDER BY CMD_CELL.IN_DATE,CMD_CELL.CELL_CODE", CigaretteCode, FormulaCode, ProductCode, CraneNo);
            else
                strSQL = string.Format("SELECT CMD_CELL.*,CMD_PRODUCT.PRODUCT_NAME,'' CIGARETTE_NAME,'' FORMULA_NAME " +
                            "FROM CMD_CELL " +
                            "LEFT JOIN CMD_PRODUCT ON CMD_CELL.PRODUCT_CODE=CMD_PRODUCT.PRODUCT_CODE " +
                            "LEFT JOIN CMD_SHELF ON CMD_CELL.SHELF_CODE=CMD_SHELF.SHELF_CODE " +
                            "WHERE CMD_CELL.IS_ACTIVE='1' AND CMD_CELL.IS_LOCK='0' AND CMD_CELL.ERROR_FLAG='0' AND CMD_CELL.PRODUCT_CODE='{0}' AND CMD_SHELF.CRANE_NO='{1}' " +
                            "ORDER BY CMD_CELL.IN_DATE,CMD_CELL.CELL_CODE", ProductCode, CraneNo);
            return ExecuteQuery(strSQL).Tables[0];
        }
    }
}
