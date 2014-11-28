using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wms.Security;
using Microsoft.Practices.Unity;
using THOK.Wms.Bll.Interfaces;
using THOK.WebUtil;
using THOK.Wms.DbModel;
using THOK.Security;


namespace WMS.Controllers.Wms.WMS
{
    [SystemEventLog]
    public class BatchSearchController : Controller
    {
        //
        // GET: /BatchSearch/
        [Dependency]
        public IBatchSearchService BatchSearchService { get; set; }

        public ActionResult Index(string moduleID)
        {
            ViewBag.hasSearch = true;
            ViewBag.ModuleID = moduleID;
            return View();
        }

        public ActionResult Details(int page, int rows, FormCollection collection)
        {
            string BILL_DATE = collection["BILL_DATE"] ?? ""; //单据日期
            string BILL_NO = collection["BILL_NO"] ?? "";  //单据编号
            string FORMULA_CODE = collection["FORMULA_CODE"] ?? "";

            var storage = BatchSearchService.GetDetails(page, rows, BILL_NO, BILL_DATE, FORMULA_CODE);
            return Json(storage, "text/html", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSubDetail(int page, int rows, string BILL_NO)
        {
            var storage = BatchSearchService.GetSubDetails(page, rows, BILL_NO);
            return Json(storage, "text/html", JsonRequestBehavior.AllowGet);
        }
    }
}
