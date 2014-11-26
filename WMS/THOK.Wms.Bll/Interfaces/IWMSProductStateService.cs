﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using THOK.Wms.DbModel;


namespace THOK.Wms.Bll.Interfaces
{
    public  interface IWMSProductStateService : IService<WMS_PRODUCT_STATE>
    {
        //获取某条单据下的作业任务
        object Details(int page, int rows, string billno);
        //单据拆分为作业,(入库)
        bool Task(string billno, string btypecode,string tasker,out string error);
        //出库作业功能.
        bool Task(string billno, string cigarettecode, string formulacode, string batchweight, string tasker, out string error);
        //结束出库作业订单
        bool end(string billno, out string error);
        //手动出库作业
        bool TaskSD(string billno,string cellstr, string tasker, out string error);
        //托盘入库作业
        bool Task(string billno, string tasker, out string error);
        //紧急补料作业
        bool FeedingTask(string billno, string tasker, out string error);
        //获取要补料的单据下的产品条码(只获取可以补料的条码)
        object Barcodeselect(int page, int rows, string soursebillno);
        //作业查询
        object Worksearch(int page, int rows, string BILL_NO, string TASK_DATE, string BTYPE_CODE, string TASK_NO, string CIGARETTE_CODE, string FORMULA_CODE, string PRODUCT_BARCODE, string FINISH_DATE);
        //作业明细
        object Workdetail(int page, int rows, string Taskid);
        //
        string GetPdfName(string Path, string username, string barcodes, string billno,string PrintCount);
        //设置条码,barcodestr:所有烟包对应条码形成的字符串
        bool SetBarcode(string billno, string barcodestr);
        //获取出库批次下的明细
        object outbarcdedetali(int page, int rows,string billno, string barcode);
        
    }
}
