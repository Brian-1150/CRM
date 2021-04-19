
using CRM.Models.Paycheck;
using CRM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.WebMVC.Controllers
{
    public class PayCheckController : Controller
    {
        private PayCheckService _svc = new PayCheckService();

        //CREATE
        public ActionResult Create()
        {
             return View(_svc.GetInvoiceCreateView());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? employeeID, List<int> listOfSelectedJobs)
        {
            if (employeeID.HasValue && listOfSelectedJobs != null)
            {
                var model = new PayCheckCreate
                {
                    EmployeeID = (int)employeeID,
                    ListOfSelectedJobs = listOfSelectedJobs,

                };
                _svc.CreatePayCheck(model);

                TempData["SaveResult"] = "Event Added";
                return RedirectToAction("Index");

            }
            return View(_svc.GetInvoiceCreateView(employeeID));

        }

        // READ: List
        public ActionResult Index()
        {
            var model = _svc.GetPayChecks();
            return View(model);
        }

        //Paycheck Details
        public ActionResult Details(int id)//add adjustment notes to detail view ticket # 30
        {
            return View(_svc.GetPayCheckByID(id));
        }

        //UPDATE:
        public ActionResult Edit(int id)
        {
            var detail = _svc.GetPayCheckByID(id);
            if (detail.Paid)
            {
                TempData["message"] = "Paycheck may not be edited once it has been marked as Paid";
                return RedirectToAction("Index");
            }
            List<int> listOfJobsOnPayCheck = _svc.GetJobIDs(id);
            List<int> listOfJobsAvailable = _svc.GetJobIDs(detail.EmployeeID, detail.PayCheckID);
            var model = new PayCheckEdit
            {
                PayCheckID = detail.PayCheckID,
                ListOfJobsOnPayCheck = listOfJobsOnPayCheck,
                ListOfJobsAvailable = listOfJobsAvailable,
                EmployeeID = detail.EmployeeID,
                PayCheckAmount = detail.PayCheckAmount,
                Paid = detail.Paid,
                AdjustmentNotes = detail.AdjustmentNotes.ToString(),
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PayCheckEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.PayCheckID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            List<int> listOfJobsOnPayCheck = _svc.GetJobIDs(id);
            if (_svc.UpdatePayCheck(model, listOfJobsOnPayCheck))
            {
                TempData["SaveResult"] = "Paycheck was updated successfully!";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Info could not be updated.  If you do not wish to make changes, return back to list");
            TempData["message"] = "Edits were not saved to database.  Perhaps you did not make changes to any of the fields";
            return RedirectToAction("Index");
        }
    }
}