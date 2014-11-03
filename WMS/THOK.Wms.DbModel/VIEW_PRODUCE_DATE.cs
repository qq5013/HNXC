using System;
using System.Collections.Generic;

namespace THOK.Wms.DbModel
{
    public partial class VIEW_PRODUCE_DATE
    {
        public string BILL_NO { get; set; }
        public decimal ITEM_NO { get; set; }
        public Nullable<System.DateTime> FIRST_DATE { get; set; }
        public Nullable<System.DateTime> LAST_DATE { get; set; }

    }
}
