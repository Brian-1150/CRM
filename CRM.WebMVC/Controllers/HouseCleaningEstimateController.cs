using CRM.Models.Estimate;
using CRM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.WebMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HouseCleaningEstimateController : Controller
    {
        private HouseCleaningEstimateService _svc = new HouseCleaningEstimateService();
        private CustomerService _custSvc = new CustomerService();

        //Create
        public ActionResult Create()
        {
            ViewBag.CustomerList = _custSvc.GetCustomerFromDB();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HouseCleaningEstimateCreate model)
        {
            _svc.CreateEstimate(model);
            return RedirectToAction("Details", "Customer", new { id = model.CustomerID });
        }


        // GET: HouseCleaningEstimate
        public ActionResult Index()
        {
            return View(_svc.GetEstimates());
        }
    }
}