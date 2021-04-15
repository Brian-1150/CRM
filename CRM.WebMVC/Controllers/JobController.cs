using CRM.Models.Employee;
using CRM.Models.Job;
using CRM.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CRM.WebMVC.Controllers
{
    public class JobController : Controller
    {
        private EmployeeService _empSvc = new EmployeeService();
        private JobService NewJobServcie()
        {
            try
            {
                var userId = Guid.Parse(User.Identity.GetUserId());
                var service = new JobService(userId);
                return service;
            }
            catch (ArgumentNullException e)
            {
               
                return null;

                throw;
            }
           
        }

        //CREATE
         public ActionResult Create()
        {
            var service = NewJobServcie();
            if (service is null)
                return View("Error");
            var model = service.GetJobCreateView();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JobCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var service = NewJobServcie();
            if (service.CreateJob(model))
            { TempData["SaveResult"] = "Event Added";
                return RedirectToAction("Index");
                            }
            ModelState.AddModelError("", "Job create failed");
            return View(model);
        }



        // READ:  List
        public ActionResult Index()
        {
            var service = NewJobServcie();
            var model = service.GetJobs();
            return View(model);
        }
        //READ: Details
        public ActionResult Details(int id)
        {
            var service = NewJobServcie();
            var model = service.GetJobByID(id);
            return View(model);
        }
       
        //UPDATE: 
        public ActionResult Edit(int id)
        {
            var service = NewJobServcie();
            var detail = service.GetJobByID(id);
            if (detail.InvoiceID.HasValue || detail.PayCheckID.HasValue)
            {
                TempData["message"] = "Job may not be edited once it has been added to invoice or paycheck";
                return RedirectToAction("Index");
            }
                
            EmployeeListItem[] listOfEmployees = _empSvc.GetEmployees().ToArray();
            var model = new JobEdit
            {
                JobID = detail.JobID,
                CalendarEventID = detail.CalendarEventID,
                CustomerID = detail.CustomerID,
                EmployeeID = detail.EmployeeID,
                ListOfEmployees = listOfEmployees,
                EmployeePay = detail.EmployeePay,
                CustomerCharge = detail.CustomerCharge,

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, JobEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.JobID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);

            }
            var service = NewJobServcie();
            if (service.UpdateJob(model))
            {
                TempData["SaveResult"] = "Job info was updated successfully!";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "JOb info could not be updated");
            return View(model);
        }
    }
}