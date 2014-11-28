using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using THOK.Wms.DbModel;

namespace THOK.Wms.Bll.Interfaces
{
    public interface IBatchSearchService : IService<CMD_CELL>
    {
        //批次库存查询
        object GetDetails(int page, int rows, string BILL_NO, string BILL_DATE, string FORMULA_CODE);

        object GetSubDetails(int page, int rows, string BILL_NO);
    }
}
