using GuildCars.Datas;
using GuildCars.Models.Interface;
using GuildCars.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    
    public class HomeController : Controller
    {
        ICar _repo = CarFactory.Create();
        public ActionResult Home()
        {
            List<SpecialVM> model = _repo.GetSpecialVMList(DateTime.Now.Date);
            return View(model);
        }
    }
}