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
        private CustomerService _svc = new CustomerService();

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

            if (_svc.CreateCustomer(model))
            {
                TempData["SaveResult"] = " Customer was successfully added to database! ";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Customer could not be created.");

            return View(model);
        }
        //C[R]UD
        // GET: Index - List of customers
        public ActionResult Index(string sortOrder)
        {
            var custList = _svc.GetCustomers();
            ViewBag.SortByName = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SortByDate = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.SortByID = sortOrder == "ID" ? "custID" : "ID";

             switch (sortOrder)
            {
                case "name_desc":
                    custList = custList.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    custList = custList.OrderBy(s => s.InitialDateOfContact);
                    break;
                case "date_desc":
                    custList = custList.OrderByDescending(s => s.InitialDateOfContact);
                    break;
                case "ID":
                    custList = custList.OrderBy(c => c.CustomerID);
                    break;
                case "custID":
                    custList = custList.OrderByDescending(c => c.CustomerID);
                    break;
                default:
                    custList = custList.OrderBy(s => s.LastName);
                    break;
            }
                    return View(custList);
        }

        //GET:  Customer Details
        public ActionResult Details(int id)
        {
            return View(_svc.GetCustomerDetailByID(id));
        }


        //CR[U]D
        //Edit 
        public ActionResult Edit(int id)
        {

            var detail = _svc.GetCustomerDetailByID(id);

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

            if (_svc.UpdateCustomer(model))
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
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var detail = _svc.GetCustomerDetailByID(id);
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
                ZipCode = detail.ZipCode,
                InitialDateOfContact = detail.InitialDateOfContact,
                StatusOfCustomer = detail.StatusOfCustomer,
                IsOnDoNotContactList = detail.IsOnDoNotContactList

            };
            return View(model);
        }
        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCustomer(int id)
        {

            _svc.DeleteCustomer(id);
           
                TempData["SaveResult"] = "Customer has been placed on 'DO NOT CONTACT LIST' and status is changed to 'INACTIVE'";
                return RedirectToAction("Index");
            

            //TempData["Message"] = "Customer info could not be updated";
            //return RedirectToAction("Index");
        }
    }
}