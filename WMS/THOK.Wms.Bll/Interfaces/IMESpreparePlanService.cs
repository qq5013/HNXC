﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using THOK.Wms.DbModel;

namespace THOK.Wms.Bll.Interfaces
{
    public  interface IMESpreparePlanService : IService<MES_PREPARER_PLAN>
    {

        object GetDetails(int page, int rows, Dictionary<string, string> paramers);
    }
}
