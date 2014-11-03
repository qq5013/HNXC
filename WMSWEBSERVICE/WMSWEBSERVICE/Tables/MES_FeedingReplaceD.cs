﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMSWEBSERVICE.Tables
{
    /// <summary>
    /// 补料替换申请从表对象
    /// </summary>
    public class MES_FeedingReplaceD
    {
        [DBField("FEED_REPLACE_APPLY_BILL_NO", EnumDBFieldUsage.PrimaryKey, "补料替换申请单据号", "CHAR", "50", "必传")]
        public string FEED_REPLACE_APPLY_BILL_NO { get; set; }
        [DBField("TROUBLE_MATERIAL_SMOKEBOX_CODE", EnumDBFieldUsage.None , "问题原料烟箱条码", "CHAR", "200", "必传")]
        public string TROUBLE_MATERIAL_SMOKEBOX_CODE { get; set; }
        [DBField("TROUBLE_MATERIAL", EnumDBFieldUsage.None, "问题原料", "CHAR", "50", "必传")]
        public string TROUBLE_MATERIAL { get; set; }
        [DBField("TROUBLE_MATERIAL_AMOUNT_KG", EnumDBFieldUsage.None, "问题原料公斤数", "NUMBER", "10.4", "必传")]
        public string TROUBLE_MATERIAL_AMOUNT_KG { get; set; }


    }
}