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
        private CalendarEventService _svc = new CalendarEventService();
        private CalendarEventService NewCalEventService() //remove unnecessary Guid parsing ticket #31
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CalendarEventService(userId);
            return service;
        }

        //CREATE
        public ActionResult Create()
        {
            var service = NewCalEventService();
            var model = service.CalendarEventCreateView();

            return View(model);
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
        //READ:  Event Details
        public ActionResult Details(int id)
        {
            var svc = NewCalEventService();
            var model = svc.GetEventById(id);

            return View(model);
        }

        //UPDATE
        public ActionResult Edit(int id)
        {
            var detail = _svc.GetEventById(id);
            var model = new CalendarEventEdit
            {
                CalEventID = detail.CalEventID,
                Title = detail.Title,
                Start = detail.Start,
                End = detail.End,
                Details = detail.Details,
                ColorOfEvent = detail.ColorOfEvent
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CalendarEventEdit model) {
            if (!ModelState.IsValid) return View(model);
            if (model.CalEventID != id)
            {
                TempData["message"] = "Sorry there was a problem with the id";
                ModelState.AddModelError("", "Id Mismatch");
                return RedirectToAction("Index");
            }
            if (_svc.UpdateCalendarEvent(model))
            {
                TempData["SaveResult"] = "Event was updated successfully!";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Event could not be updated");
            return View(model);
    } }
}