using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuildCars.Models.ViewModels;
using GuildCars.Models.Interface;
using GuildCars.Datas;

namespace GuildCars.UI.Controllers
{
    public class AuthController : Controller
    {
        ICar _repo = CarFactory.Create();

        public ActionResult Login()
        {
            var model = new LoginVM();
            model.Result = _repo.ReturnSuccess();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            model.Result = _repo.ReturnSuccess();
            if (ModelState.IsValid)
            {
                if (_repo.Login(model.UserName, model.PasswordHash))
                {
                    return Redirect(Url.Action("Home", "Home"));
                    
                }
                else
                {
                    ModelState.AddModelError("Auth", "Incorrect username or password!");
                    model.Result.ErrorMessage = "Incorrect username or password!";
                }
            }
            
            return View(model);
        }

        public ActionResult LogOut()
        {
            _repo.Logout();
            return RedirectToAction("Home", "Home");
        }
    }
}