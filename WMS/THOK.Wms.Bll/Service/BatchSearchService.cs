using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using THOK.Wms.DbModel;
using THOK.Wms.Bll.Interfaces;
using Microsoft.Practices.Unity;
using THOK.Wms.Dal.Interfaces;
using System.Data;


namespace THOK.Wms.Bll.Service
{
    class BatchSearchService : ServiceBase<CMD_CELL>, IBatchSearchService
    {

        protected override Type LogPrefix
        {
            get { return this.GetType(); }
        }
        [Dependency]
        public IViewCmdProductRepository ViewCmdProductRepository { get; set; }
        [Dependency]
        public IViewWcsTaskRepository ViewWcsTaskRepository { get; set; }

        [Dependency]
        public IVIEWSTORAGERepository ViewstorageRepository { get; set; }
        [Dependency]
        public IViewbillmastRepository ViewbillmastRepository { get; set; }
        [Dependency]
        public IWMSBillDetailHRepository BillDetailRepository { get; set; }
        [Dependency]
        public ICMDCigaretteRepository CMDCigaretteRepository { get; set; }
        [Dependency]
        public IWMSFormulaMasterRepository FormulMasterRepository { get; set; }
        [Dependency]
        public IWMSFormulaDetailRepository FormulDetailRepository { get; set; }
        [Dependency]
        public ICMDCraneRepository CraneRepository { get; set; }
        [Dependency]
        public ICMDShelfRepository ShelfRepository { get; set; }
        [Dependency]
        public ICMDCellRepository CellRepository { get; set; }

        //批次库存查询

        public object GetDetails(int page, int rows, string BILL_NO, string BILL_DATE, string FORMULA_CODE)
        {
            IQueryable<VIEW_STORAGE> storagequery = ViewstorageRepository.GetQueryable();
            var billmast = ViewbillmastRepository.GetQueryable();
            var cigaret = CMDCigaretteRepository.GetQueryable();
            var formula = FormulMasterRepository.GetQueryable();

            if (!string.IsNullOrEmpty(BILL_NO))
            {
                storagequery = storagequery.Where(i => i.BILL_NO == BILL_NO);
            }
            if (BILL_DATE != string.Empty && BILL_DATE != null)
            {
                DateTime operatedt = DateTime.Parse(BILL_DATE);
                DateTime operatedt2 = operatedt.AddDays(1);
                billmast = billmast.Where(i => i.BILL_DATE.CompareTo(operatedt) >= 0);
                billmast = billmast.Where(i => i.BILL_DATE.CompareTo(operatedt2) < 0);
            }
            //if (!string.IsNullOrEmpty(FORMULA_CODE))
            //{
            //    billmast = billmast.Where(i => i.FORMULA_CODE == FORMULA_CODE);
            //}

            var billnos = from a in storagequery
                          group a by new {  a.BILL_NO } into g
                          select new{ g.Key };
            var bilLstorage = from a in billmast
                              join b in billnos on a.BILL_NO equals b.Key.BILL_NO
                              join c in cigaret on a.CIGARETTE_CODE equals c.CIGARETTE_CODE
                              join f in formula on a.FORMULA_CODE equals f.FORMULA_CODE
                              select new
                              {
                                  BILL_NO = b.Key.BILL_NO,
                                  a.BILL_DATE,
                                  a.CIGARETTE_CODE,
                                  c.CIGARETTE_NAME,
                                  a.FORMULA_CODE,
                                  f.FORMULA_NAME
                              };

            bilLstorage = bilLstorage.OrderByDescending(i => i.BILL_DATE);
            int total = bilLstorage.Count();
            bilLstorage = bilLstorage.Skip((page - 1) * rows).Take(rows);
            var temp = bilLstorage.ToArray().Select(i => new
            {
                i.BILL_NO,
                BILL_DATE = i.BILL_DATE.ToString("yyyy-MM-dd"),
                i.CIGARETTE_NAME,
                i.CIGARETTE_CODE,
                i.FORMULA_NAME,
            });
            return new { total, rows = temp };
        }


        //获取所有包含该等级的入库单的库存。
        public object GetSubDetails(int page, int rows, string BILL_NO)
        {
            var storagequery = ViewstorageRepository.GetQueryable();
            var billmast = ViewbillmastRepository.GetQueryable();
            var cigaret = CMDCigaretteRepository.GetQueryable();
            var formula = FormulMasterRepository.GetQueryable();

            if (!string.IsNullOrEmpty(BILL_NO))
            {
                storagequery = storagequery.Where(i => i.BILL_NO == BILL_NO);
            }

            var bilLstorage = from a in billmast
                              join b in storagequery on a.BILL_NO equals b.BILL_NO
                              join c in cigaret on a.CIGARETTE_CODE equals c.CIGARETTE_CODE
                              join f in formula on a.FORMULA_CODE equals f.FORMULA_CODE
                              select new
                              {
                                  GRADE_CODE = b.GRADE_CODE,
                                  b.PRODUCT_NAME,
                                  b.GRADE_NAME,
                                  b.CELL_CODE,
                                  b.IS_ACTIVE,
                                  b.IS_LOCK,
                                  b.PRODUCT_BARCODE,
                                  b.ORIGINAL_CODE,
                                  b.ORIGINAL_NAME,
                                  b.YEARS,
                                  b.REAL_WEIGHT,
                                  BILL_NO = b.BILL_NO,
                                  a.BILL_DATE,
                                  a.CIGARETTE_CODE,
                                  c.CIGARETTE_NAME,
                                  a.FORMULA_CODE,
                                  f.FORMULA_NAME,
                                  b.IN_DATE
                              };
            bilLstorage = bilLstorage.OrderByDescending(i => i.IN_DATE);
            int total = bilLstorage.Count();
            bilLstorage = bilLstorage.Skip((page - 1) * rows).Take(rows);
            var temp = bilLstorage.ToArray().Select(i => new
            {
                i.CELL_CODE,
                IS_ACTIVE = i.IS_ACTIVE == "1" ? "可用" : "禁用",
                IS_LOCK = i.IS_LOCK == "1" ? "可用" : "锁定",
                i.PRODUCT_NAME,
                i.PRODUCT_BARCODE,
                i.GRADE_CODE,
                i.GRADE_NAME,
                i.ORIGINAL_CODE,
                i.ORIGINAL_NAME,
                i.YEARS,
                i.REAL_WEIGHT,
                BARCODEID = (i.PRODUCT_BARCODE).Substring(55, 10),//条码标示符
                i.BILL_NO,
                IN_DATE = i.IN_DATE.ToString("yyyy-MM-dd HH:mm:ss"),
                i.CIGARETTE_NAME,
                i.FORMULA_NAME
            });
            return new { total, rows = temp };
        }

        //public object GetSubDetails(int page, int rows, string BILL_NO)
        //{
        //    var storagequery = ViewstorageRepository.GetQueryable();
        //    var billmast = ViewbillmastRepository.GetQueryable();
        //    var cigaret = CMDCigaretteRepository.GetQueryable();
        //    var formula = FormulMasterRepository.GetQueryable();
        //    var billnos = from a in storagequery
        //                  where a.BILL_NO == BILL_NO
        //                  group a by new { a.CELL_CODE, a.PRODUCT_BARCODE, a.BILL_NO, a.GRADE_CODE, a.ORIGINAL_CODE, a.YEARS, a.IN_DATE } into g
        //                  select new
        //                  {
        //                      g.Key,
        //                      CELLSTATU = storagequery.FirstOrDefault(n => n.CELL_CODE == g.Key.CELL_CODE).IS_ACTIVE,
        //                      TOTALPACKAGE = g.Count(),
        //                      TOTALWEIGHT = g.Sum(i => i.REAL_WEIGHT),
        //                      g.Key.IN_DATE
        //                  };

        //    var bilLstorage = from a in billmast
        //                      join b in billnos on a.BILL_NO equals b.Key.BILL_NO
        //                      join c in cigaret on a.CIGARETTE_CODE equals c.CIGARETTE_CODE
        //                      join f in formula on a.FORMULA_CODE equals f.FORMULA_CODE
        //                      select new
        //                      {
        //                          GRADE_CODE = b.Key.GRADE_CODE,
        //                          b.Key.CELL_CODE,
        //                          b.CELLSTATU,
        //                          b.Key.PRODUCT_BARCODE,
        //                          BILL_NO = b.Key.BILL_NO,
        //                          a.BILL_DATE,
        //                          a.CIGARETTE_CODE,
        //                          c.CIGARETTE_NAME,
        //                          a.FORMULA_CODE,
        //                          f.FORMULA_NAME,
        //                          b.TOTALPACKAGE,
        //                          b.TOTALWEIGHT,
        //                          b.IN_DATE
        //                      };
        //    bilLstorage = bilLstorage.OrderByDescending(i => i.IN_DATE);
        //    int total = bilLstorage.Count();
        //    bilLstorage = bilLstorage.Skip((page - 1) * rows).Take(rows);
        //    var temp = bilLstorage.ToArray().Select(i => new
        //    {
        //        i.CELL_CODE,
        //        CELLSTATU = i.CELLSTATU == "1" ? "可用" : "禁用",
        //        i.PRODUCT_BARCODE,
        //        i.GRADE_CODE,
        //        BARCODEID = (i.PRODUCT_BARCODE).Substring(55, 10),//条码标示符
        //        i.BILL_NO,
        //        BILL_DATE = i.BILL_DATE.ToString("yyyy-MM-dd HH"),
        //        IN_DATE = i.IN_DATE.ToString("yyyy-MM-dd HH:mm:ss"),
        //        i.CIGARETTE_NAME,
        //        i.FORMULA_NAME,
        //        i.TOTALWEIGHT,
        //        i.TOTALPACKAGE
        //    });
        //    return new { total, rows = temp };
        //}



    }
}
