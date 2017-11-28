using GuildCars.Datas;
using GuildCars.Models;
using GuildCars.Models.Interface;
using GuildCars.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    [Authorize(Roles = "sale")]
    public class SaleController : Controller
    {
        ICar _repo = CarFactory.Create();
        
        public ActionResult List()
        {
            return View();
        }

        public ActionResult Purchase()
        {
            SaleVM model = new SaleVM();
            model.SetStateItems();
            model.SetPurchaseTypeItems();
            return View(model);
        }        
    }
}