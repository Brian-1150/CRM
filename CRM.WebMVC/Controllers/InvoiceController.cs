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
    [Authorize]
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, InvoiceEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.InvoiceID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            List<int> listOfJobsOnInvoice = _svc.GetJobIDs(id); //grab the same list again as in the Get EditView because it is not passing through properly to this Post method
            if (_svc.UpdateInvoice(model, listOfJobsOnInvoice))  
            {
                TempData["SaveResult"] = "Job info was updated successfully!";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Job info could not be updated.  If you do not wish to make changes, return back to list");
            TempData["message"] = "Edits were not saved to database.  Perhaps you did not make changes to any of the fields";
            return RedirectToAction("Index");
        }

        [ActionName("Delete")]
        public ActionResult NoDelete()
        {
            TempData["message"] = "You may not delete an invoice. Just remove assocaited jobs to zero out the balance.";
            return RedirectToAction("Index");
        }
    }
}