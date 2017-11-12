using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuildCars.Models.ViewModels;
using GuildCars.Models.Interface;
using GuildCars.Datas;
using SCMS.Datas;

namespace SCMS.UI.Controllers
{
    public class ResetPasswordController : Controller
    {
        ICar _repo = CarFactory.Create();

        // GET: ResetPassword
        public ActionResult ResetPassword()
        {
            ResetPasswordVM model = new ResetPasswordVM();
            model.UserName = CurrentUser.User.UserName;
            return View(model);
        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordVM model)
        { 
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _repo.ChangePassword(model.UserName, model.Password, model.NewPassword);
            return RedirectToAction("Home","Home");
        }
    }
}