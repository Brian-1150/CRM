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
        [Authorize(Roles = "Admin")]
    
    public class JobController : Controller
    {
        private EmployeeService _empSvc = new EmployeeService();
        private JobService _svc = new JobService();

        //CREATE
        public ActionResult Create()
        {
            if (_svc is null)
                return View("Error");
            return View(_svc.GetJobCreateView());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JobCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (_svc.CreateJob(model))
            {
                TempData["SaveResult"] = "Event Added";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Job create failed");
            return View(model);
        }


        // READ:  List
        [Authorize(Roles = "Admin, Employee")]
        public ActionResult Index()
        {
            return View(_svc.GetJobs());
        }

        //READ: Details
        [Authorize(Roles = "Admin, Employee")]
        public ActionResult Details(int id)
        {
            return View(_svc.GetJobByID(id));
        }

        //UPDATE: 
        public ActionResult Edit(int id)
        {

            var detail = _svc.GetJobByID(id);
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

            if (_svc.UpdateJob(model))
            {
                TempData["SaveResult"] = "Job info was updated successfully!";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "JOb info could not be updated");
            return View(model);
        }

        //DELETE
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            //prevent delete if job has FK of invoice or paycheck ticket # 29

            return View(_svc.GetJobByID(id));
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteJob(int id)
        {

            _svc.DeleteJob(id);
            TempData["SaveResult"] = "Your Job was deleted";

            return RedirectToAction("Index");
        }
    }



}