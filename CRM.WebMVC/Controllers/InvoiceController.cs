using CRM.Models.Invoice;
using CRM.Models.Job;
using CRM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.WebMVC.Controllers
{
    public class InvoiceController : Controller
    {
        private InvoiceService _svc = new InvoiceService();
        private JobService _jobSvc = new JobService();
        //Create
        public ActionResult Create()
        {

            var model = _svc.GetInvoiceCreatView();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InvoiceCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (_svc.CreateInvoice(model))
            {
                TempData["SaveResult"] = "Event Added";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Job create failed");
            return View(model);
        }

        // READ: List of Invoices
        public ActionResult Index()
        {
            var model = _svc.GetInvoices();
            return View(model);
        }

        //Invoice Detials
        public ActionResult Details(int id)
        {
            var model = _svc.GetInvoiceByID(id);
            return View(model);
        }

        //UPDATE: 
        public ActionResult Edit(int id)
        {
           
            var detail = _svc.GetInvoiceByID(id);
            if (detail.Paid)
            {
                TempData["message"] = "Invoice may not be edited once it has been marked as Paid";
                return RedirectToAction("Index");
            }

            List<int> listOfJobsOnInvoice = _svc.GetJobIDs(id);
            List<int> listOfJobsAvailable = _svc.GetJobIDs(detail.CustomerID, detail.InvoiceID);
            var model = new InvoiceEdit
            {
                InvoiceID = detail.InvoiceID,
                ListOfJobsOnInvoice = listOfJobsOnInvoice,
                ListOfJobsAvailable = listOfJobsAvailable,
                CustomerID = detail.CustomerID,
                InvoiceAmount = detail.InvoiceAmount,
                Paid = detail.Paid
                

            };
            return View(model);
        }
        public ActionResult Edit(int id, InvoiceEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.InvoiceID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            
            if (_svc.UpdateInvoice(model))
            {
                TempData["SaveResult"] = "Job info was updated successfully!";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "JOb info could not be updated");
            return View(model);
        }
    }
}