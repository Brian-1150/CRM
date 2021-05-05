using CRM.Data;
using CRM.Models.CalendarEvent;
using CRM.Models.Job;
using CRM.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.WebMVC.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    public class CalendarEventController : Controller
    {
        private CalendarEventService _svc = new CalendarEventService();
        private JobService _jobSvc = new JobService();
        private EmployeeService _empService = new EmployeeService();

        //CREATE
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View(_svc.CalendarEventCreateView());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CalendarEventCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (model.TypeOfEvent is EventType.Job)
            {
                if (_svc.CreateCalendarEvent(model))
                    return Redirect("~/Job/CreateFromCalEvent");
                else
                {
                    TempData["message"] = "That did not work";
                    return View(model);
                }
            }
            if (_svc.CreateCalendarEvent(model))
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
            ViewBag.JobInfo = _jobSvc.GetJobsFromDB();
            return View(_svc.GetCalendarEvents());
        }

        public ActionResult CustomIndexView()
        {
            var list = _svc.GetCalendarEvents();
            var newList = new List<CalendarEventListItem>();
            foreach (var x in list)
            {
                if (x.End > DateTime.Now)
                {

                    newList.Add(x);  //model.Remove(x);
                }

            }
            return View(newList);
        }

        //READ:  Event Details
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            var detail = _svc.GetEventById(id);
            if (detail.TypeOfEvent == EventType.Job)
                ViewBag.JobInfo = _jobSvc.GetJobByID(id);
            return View(detail);
        }

        //UPDATE
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var jobDetail = new JobDetail { };
            var detail = _svc.GetEventById(id);
            if (detail.TypeOfEvent == EventType.Job)
            {
                var jobValues = _jobSvc.GetJobByID(id);
                if (jobValues.InvoiceID.HasValue || jobValues.PayCheckID.HasValue)
                {
                    TempData["message"] = "Job may not be edited once it has been added to invoice or paycheck";
                    return RedirectToAction("Index");
                }
                jobDetail = jobValues;
            }

            var model = new CalendarEventEdit
            {
                CustomerID = jobDetail.CustomerID,
                CalEventID = detail.CalEventID,
                Title = detail.Title,
                Start = detail.Start,
                End = detail.End,
                Details = detail.Details,
                ColorOfEvent = detail.ColorOfEvent,
                CustomerFullName = jobDetail.CustomerFullName,
                EmployeeID = jobDetail.EmployeeID,
                EmployeeFullName = jobDetail.EmployeeFullName,
                CustomerCharge = jobDetail.CustomerCharge,
                EmployeePay = jobDetail.EmployeePay,
                Location = detail.Location,
                ListOfEmployees = _empService.GetEmployees().ToList()

            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CalendarEventEdit model)
        {
            if (!ModelState.IsValid)
            {
                TempData["message"] = "Sorry there was a problem.";
                return RedirectToAction("Index");
            }

            if (model.CalEventID != id)
            {
                TempData["message"] = "Sorry there was a problem with the id";
                ModelState.AddModelError("", "Id Mismatch");
                return RedirectToAction("Index");
            }

            var jobEditModel = new JobEdit { };
            if (model.EmployeeFullName != null)
            {
                jobEditModel.CustomerID = model.CustomerID;
                jobEditModel.CalendarEventID = model.CalEventID;
                jobEditModel.JobID = model.CalEventID;
                jobEditModel.EmployeeID = model.EmployeeID;
                jobEditModel.EmployeePay = model.EmployeePay;
                jobEditModel.CustomerCharge = model.CustomerCharge;
                _jobSvc.UpdateJob(jobEditModel);
            }

            
            _svc.UpdateCalendarEvent(model);

            TempData["SaveResult"] = "Event was updated successfully!";
            return RedirectToAction("Index");

        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            return View(_svc.GetEventById(id));
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEvent(int id)
        {
            _svc.DeleteEvent(id);
            TempData["SaveResult"] = "Your event was deleted";

            return RedirectToAction("Index", "CalendarEvent");
        }
    }
}
