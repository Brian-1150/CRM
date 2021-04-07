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