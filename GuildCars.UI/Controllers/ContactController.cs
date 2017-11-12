using GuildCars.Models;
using GuildCars.Models.Interface;
using SCMS.Datas;
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

        [HttpGet]
        public ActionResult List()
        {
            var model = _repo.GetContactList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new Contact());
        }

        [HttpPost]
        public ActionResult Add(Contact model)
        {
            if (ModelState.IsValid)
            {
                if (_repo.AddContact(model) > 0)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    ModelState.AddModelError("Contact", "Cannot add contact");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int contactId)
        {
            Contact model = _repo.GetContactById(contactId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Contact model)
        {
            if (ModelState.IsValid)
            {
                if (_repo.UpdateContact(model))
                {
                    return RedirectToAction("List");
                }
                else
                {
                    ModelState.AddModelError("Contact", "Cannot edit contact");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int contactId)
        {
            Contact model = _repo.GetContactById(contactId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Contact model)
        {
            if (_repo.DeleteContact(model.ContactId))
            {
                return RedirectToAction("List");
            }
            else
            {
                ModelState.AddModelError("Category", "Cannot delete contact");
                return View(model);
            }

        }
    }
}