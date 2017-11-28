using GuildCars.Models;
using GuildCars.Models.Interface;
using GuildCars.Models.ViewModels;
using GuildCars.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class ContactController : Controller
    {
        ICar _repo = CarFactory.Create();

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult List()
        {
            var model = _repo.GetContactVMList();
            return View(model);
        }
        
        [HttpGet]
        public ActionResult Add()
        {
            string query = Request.QueryString["id"];
            int id = 0;
            int.TryParse(query, out id);

            ContactVM model = new ContactVM();
            Car car = _repo.GetCarById(id);
            if (car != null)
            {
                model.Message = car.VinNo;
            }            
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ContactVM model)
        {
            if (ModelState.IsValid)
            {
                var tmp = _repo.AddContact(model);
                if (tmp != null && tmp.Result.Success)
                {
                    return RedirectToAction("Success");
                }
                else
                {
                    ModelState.AddModelError("Contact", "Cannot save contact");
                }
            }
            return View(model);
        }

        //[HttpGet]
        //public ActionResult Edit(int contactId)
        //{
        //    ContactVM model = _repo.ConvertContactToVM(_repo.GetContactById(contactId));
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult Edit(ContactVM model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (_repo.UpdateContact(model).Result.Success)
        //        {
        //            return RedirectToAction("Success");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("Contact", "Cannot edit contact");
        //        }
        //    }
        //    return View(model);
        //}

        //[HttpGet]
        //public ActionResult Delete(int contactId)
        //{
        //    ContactVM model = _repo.ConvertContactToVM(_repo.GetContactById(contactId));
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult Delete(ContactVM model)
        //{
        //    if (_repo.DeleteContact(model).Success)
        //    {
        //        return RedirectToAction("List");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("Contact", "Cannot delete contact");
        //        return View(model);
        //    }
        //}

        public ActionResult Success()
        {
            return View();
        }
    }
}