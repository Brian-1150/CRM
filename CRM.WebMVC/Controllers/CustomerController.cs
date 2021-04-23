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
    [Authorize(Roles = "Admin")]
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
            var model = new CustomerCreate();
            return View(model);
            //passing in model so it will default to PersonState.INDIANA
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
            ModelState.AddModelError("", "Customer could not be created.");

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
        //GET:  Customer Details
        public ActionResult Details(int id)
        {
            var svc = NewCustomerService();
            var model = svc.GetCustomerDetailByID(id);

            return View(model);
        }


        //CR[U]D
        //Edit 
        public ActionResult Edit(int id)
        {
            var service = NewCustomerService();
            var detail = service.GetCustomerDetailByID(id);
            
            var model = new CustomerEdit
            {
                CustomerID = detail.CustomerID,
                FirstName = detail.FirstName,
                LastName = detail.LastName,
                PhoneNumber = detail.PhoneNumber,
                Email = detail.Email,
                StreetAddress = detail.StreetAddress,
                City = detail.City,
                StateOfPerson = detail.StateOfPerson,
                InitialDateOfContact = detail.InitialDateOfContact,
                StatusOfCustomer = detail.StatusOfCustomer

            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CustomerEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.CustomerID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);

            }
            var service = NewCustomerService();
            if (service.UpdateCustomer(model))
            {
                TempData["SaveResult"] = "Customer info was updated successfully!";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Customer info could not be updated");
            return View(model);
        }
        //CRU[D]
        //GET:  Customer Delete View
        //Ticket # 16(NEED TO FIX ENTIRE DELETE METHOD)
        public ActionResult Delete(int id)
        {
            var service = NewCustomerService();
            var detail = service.GetCustomerDetailByID(id);
            var model = new CustomerDelete
            {
                CustomerID = detail.CustomerID,
                FirstName = detail.FirstName,
                LastName = detail.LastName,
                PhoneNumber = detail.PhoneNumber,
                Email = detail.Email,
                StreetAddress = detail.StreetAddress,
                City = detail.City,
                StateOfPerson = detail.StateOfPerson,
                InitialDateOfContact = detail.InitialDateOfContact,
                StatusOfCustomer = detail.StatusOfCustomer,
                IsOnDoNotContactList = detail.IsOnDoNotContactList

            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CustomerDelete model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.CustomerID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);

            }
            var service = NewCustomerService();
            if (service.DeleteCustomer(model))
            {if (model.IsOnDoNotContactList)
                {
                    TempData["SaveResult"] = "Customer has been placed on 'DO NOT CONTACT LIST' and status is changed to 'INACTIVE'";
                    return RedirectToAction("Index");
                }
            if (!model.IsOnDoNotContactList && model.StatusOfCustomer == Data.CustomerStatus.Inactive)
                {
                    TempData["SaveResult"] = "Customer status is changed to 'INACTIVE'";
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("", "Customer info could not be updated");
            return View(model);
        }
    }
}