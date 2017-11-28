using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuildCars.Models.ViewModels;
using GuildCars.Models.Interface;
using GuildCars.Models;
using GuildCars.Datas;

namespace GuildCars.UI.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        ICar _repo = CarFactory.Create();

        public ActionResult List()
        {
            List<User> model = _repo.GetUserList();
            return View(model);
        }

        public ActionResult Add()
        {
            UserVM model = new UserVM();
            model.GetRoleItems();
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(UserVM model)
        {
            if (!ModelState.IsValid)
            {
                model.GetRoleItems();
                return View(model);
            }
            
            if( _repo.AddUser(model, model.Role).Result.Success)
            {
                return RedirectToAction("List");
            }
            return View(model);            
        }

        [HttpGet]
        public ActionResult Edit(string userName)
        {
            User user = _repo.GetUserByUserName(userName);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User model)
        {
            if (model.IsActive == true)
            {
                _repo.DeactivateUser(model.UserName);
            }
            else{
                _repo.ReactivateUser(model.UserName);
            }
            return RedirectToAction("List");
        }

        //[HttpGet]
        //public ActionResult Delete(string userName)
        //{
        //    User user = _repo.GetUserByUserName(userName);
        //    return View(user);
        //}

        //[HttpPost]
        //public ActionResult Delete(User model)
        //{
        //    _repo.DeleteUser(model.Id);
        //    return RedirectToAction("List"); 
        //}
    }
}