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
    public class JobController : Controller
    {

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
       
    }
}