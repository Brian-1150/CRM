using CRM.Models.CalendarEvent;
using CRM.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.WebMVC.Controllers
{
    public class CalendarEventController : Controller
    {
        private CalendarEventService NewCalEventService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CalendarEventService(userId);
            return service;
        }

        //CREATE
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CalendarEventCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var service = NewCalEventService();
            if (service.CreateCalendarEvent(model))
            {
                TempData["SaveResult"] = "Event Added";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Event could not be created");
            return View(model);
        }

        // READ:  list of events
        public ActionResult Index()
        {
            var service = NewCalEventService();
            var model = service.GetCalendarEvents();
            return View(model);
        }
    }
}