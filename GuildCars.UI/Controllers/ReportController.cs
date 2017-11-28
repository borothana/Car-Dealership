using GuildCars.Models.Interface;
using GuildCars.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuildCars.Models.ViewModels;

namespace GuildCars.UI.Controllers
{
    [Authorize(Roles = "admin")]
    public class ReportController : Controller
    {
        ICar _repo = CarFactory.Create();
        public ActionResult Sale()
        {
            SaleReportVMPage model = new SaleReportVMPage();
            model.SetUserItems(_repo.GetUserList());
            DateTime N = DateTime.Now;
            model.FDate = null;// DateTime.MinValue;// DateTime.Parse(N.Month + "/01/" + N.Year);
            model.TDate = null;// DateTime.MaxValue;// DateTime.Parse(N.Month + "/01/" + N.Year);
            model.SaleReportVM = _repo.GetSaleReport("", DateTime.Parse("01/01/1900"), DateTime.Parse("01/01/2500"));
            return View(model);
        }
        [HttpPost]
        public ActionResult Sale(SaleReportVMPage model)
        {
            if(model.FDate < DateTime.Parse("01/01/1900"))
            {
                model.FDate = DateTime.Parse("01/01/1900");
            }

            if (model.TDate > DateTime.Parse("12/31/2500"))
            {
                model.TDate = DateTime.Parse("12/31/2500");
            }

            if (string.IsNullOrEmpty(model.UserId))
            {
                model.UserId = "";
            }
            model.SetUserItems(_repo.GetUserList());
            model.SaleReportVM = _repo.GetSaleReport(model.UserId, DateTime.Parse("01/01/1900"), DateTime.Parse("12/31/2500"));
            return View(model);
        }

        public ActionResult Inventory()
        {
            var result = _repo.GetInventoryReport();
            return View(result);
        }
        
    }
}