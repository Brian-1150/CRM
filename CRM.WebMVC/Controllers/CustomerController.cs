using CRM.Models;
using CRM.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.WebMVC.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerService NewCustomerService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CustomerService(userId);
            return service;
        }

        //[C]RUD
        //GET:  CreateCustomer View
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);
            //have EnumDropDownList default to have IN selected, but still be editable ** Ticket#15
            //model.StateOfPerson = Data.PersonState.IN;
            var service = NewCustomerService();
            if (service.CreateCustomer(model))
            {
                TempData["SaveResult"] = " Customer was successfully added to database! ";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Note could not be created.");

            return View(model);
        }
        //C[R]UD
        // GET: Index - List of customers
        public ActionResult Index()
        {
            var service = NewCustomerService();
            var model = service.GetCustomers();
            return View(model);
        }
    }
}