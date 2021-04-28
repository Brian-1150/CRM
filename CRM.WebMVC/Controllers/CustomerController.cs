using CRM.Models;
using CRM.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace CRM.WebMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomerController : Controller
    {
        private CalendarEventService _calSvc = new CalendarEventService();
        private CustomerService _svc = new CustomerService();
        private InvoiceService _invoiceSvc = new InvoiceService();

        //[C]RUD
        //GET:  CreateCustomer View

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CustomerCreate();
            return View(model);
            //passing in model so it will default to PersonState.INDIANA
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName ( "Create")]
        public ActionResult Create(CustomerCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);
            //check for duplicate and ask for confirm before proceeding ticket # 38

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
        public ActionResult Index(string sort, string filter, string search, int? page)
        {
            var custList = _svc.GetCustomers();
            
            ViewBag.Sort = sort;
            ViewBag.SortByName = string.IsNullOrEmpty(sort) ? "nameDescending" : "";
            ViewBag.SortByStatus = sort == "Status" ? "statusDescending" : "Status";
            ViewBag.SortByID = sort == "ID" ? "idDescending" : "ID";

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = filter;
            }

            ViewBag.Filter = search;

            if (!string.IsNullOrEmpty(search))
            {
                custList = custList.Where(c => c.LastName.ToLower().Contains(search.ToLower())
                                 || c.FirstName.ToLower().Contains(search.ToLower()) ||  c.StreetAddress.ToLower().Contains(search.ToLower()) ||
                                 c.PhoneNumber.Contains(search));
                    }
             switch (sort)
            {
                case "nameDescending":
                    custList = custList.OrderByDescending(s => s.LastName);
                    break;
                case "Status":
                    custList = custList.OrderBy(s => s.StatusOfCustomer);
                    break;
                case "statusDescending":
                    custList = custList.OrderByDescending(s => s.StatusOfCustomer);
                    break;
                case "ID":
                    custList = custList.OrderBy(c => c.CustomerID);
                    break;
                case "idDescending":
                    custList = custList.OrderByDescending(c => c.CustomerID);
                    break;
                default:
                    custList = custList.OrderBy(s => s.LastName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(custList.ToPagedList(pageNumber, pageSize));
            //Add button to show deleted customers ticket #39

        }

        //GET:  Customer Details
        public ActionResult Details(int id)
        {
            ViewBag.CalEventList = _calSvc.GetCalendarEvents(id);
            ViewBag.InvoiceList = _invoiceSvc.GetInvoices(id);
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