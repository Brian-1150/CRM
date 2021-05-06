using CRM.Models.Employee;
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
    public class EmployeeController : Controller
    {
        private CalendarEventService _calSvc = new CalendarEventService();
        private EmployeeService _svc = new EmployeeService();
        private PayCheckService _payCheckSvc = new PayCheckService();

        //[C]RUD
        //GET:  CreateEmployee View
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (_svc.CreateEmployee(model))
            {
                TempData["SaveResult"] = "Employee Added";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Employee could not be created");
            return View(model);
        }
        //C[R]UD
        //GET:  Index - List of Employees

        public ActionResult Index()
        {
            return View(_svc.GetEmployees());
        }

        //GET:  Employee Details
        public ActionResult Details(int id)
        {
            ViewBag.CalEventList = _calSvc.GetCalendarEventsByEmpID(id);
            ViewBag.PayCheckList = _payCheckSvc.GetPayChecks(id);
            return View(_svc.GetEmployeeByID(id));
        }

        //CR[U]D
        //GET: Edit view
        public ActionResult Edit(int id)
        {
            var detail = _svc.GetEmployeeByID(id);
            var model = new EmployeeEdit
            {
                EmployeeID = detail.EmployeeID,
                FirstName = detail.FirstName,
                LastName = detail.LastName,
                PhoneNumber = detail.PhoneNumber,
                Email = detail.Email,
                StreetAddress = detail.StreetAddress,
                City = detail.City,
                StateOfPerson = detail.StateOfPerson,
                HireDate = detail.HireDate,
                Current = detail.Current
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EmployeeEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.EmployeeID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            if (_svc.UpdateEmployee(model))
            {
                TempData["SaveResult"] = "Customer info was updated successfully!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Customer info could not be updated");
            return View(model);
        }
    }
}