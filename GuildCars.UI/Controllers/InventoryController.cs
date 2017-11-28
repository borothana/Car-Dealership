using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class InventoryController : Controller
    {
        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Used()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Detail()
        {
            return View();
        }
    }
}