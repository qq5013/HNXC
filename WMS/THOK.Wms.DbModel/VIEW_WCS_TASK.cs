﻿using System;
using System.Collections.Generic;

namespace THOK.Wms.DbModel
{
    public partial class VIEW_WCS_TASK
    {
        public string TASK_ID { get; set; }
        public string TASK_TYPE { get; set; }
        public decimal TASK_LEVEL { get; set; }
        public string BILL_NO { get; set; }
        public string PRODUCT_CODE { get; set; }
        public decimal REAL_WEIGHT { get; set; }
        public string PRODUCT_BARCODE { get; set; }
        public string PALLET_CODE { get; set; }
        public string CELL_CODE { get; set; }
        public string TARGET_CODE { get; set; }
        public string STATE { get; set; }
        public Nullable<System.DateTime> TASK_DATE { get; set; }
        public string TASKER { get; set; }
        public string PRODUCT_TYPE { get; set; }
        public string NEWCELL_CODE { get; set; }
        public Nullable<System.DateTime> FINISH_DATE { get; set; }
        public string IS_MIX { get; set; }
        public string RFID_CHECK { get; set; }
        public string CHECK_PALLET_CODE { get; set; }
        public string BARCODE_CHECK { get; set; }
        public string CHECK_PRODUCT_BARCODE { get; set; }
        public string SOURCE_BILLNO { get; set; }
        public Nullable<decimal> FORDER { get; set; }
        public string FORDERBILLNO { get; set; }
        public Nullable<decimal> ORDER_NO { get; set; }
        public string INITIAL_BARCODE { get; set; }

        public string BATCH_NO { get; set; }
        public Nullable<decimal> AMOUNT { get; set; }
        public Nullable<System.DateTime> CHECK_DATE { get; set; }
        public string CHECKER { get; set; }
        public string CHECK_RESULT { get; set; }
        public string CHECK_CHANNEL_NO { get; set; }
        public string RFIDERR_HANDLE_FLAG { get; set; }
        public string IS_SEND_PLC { get; set; }

    }
}
