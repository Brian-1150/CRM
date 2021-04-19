
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
        public ActionResult Create(int? employeeID, int? jobID)
        {

            return View(_svc.GetInvoiceCreateView(employeeID));          

        }




        // READ: List
        public ActionResult Index()
        {
            var model = _svc.GetPayChecks();
            return View(model);
        }
    }
}