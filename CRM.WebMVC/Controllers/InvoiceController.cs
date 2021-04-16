using CRM.Models.Invoice;
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

        // GET: Invoice
        public ActionResult Index()
        {
            return View();
        }
    }
}