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
    [Authorize]
    public class EmployeeController : Controller
    {
        private EmployeeService NewEmployeeService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new EmployeeService(userId);
            return service;
        }

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
            var service = NewEmployeeService();
            if (service.CreateEmployee(model))
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
            var service = NewEmployeeService();
            var model = service.GetEmployees();
            return View(model);
        }
        //GET:  Employee Details
        public ActionResult Details(int id)
        {
            var service = NewEmployeeService();
            var model = service.GetEmployeeByID(id);
            return View(model);
        }
        //CR[U]D
        //GET: Edit view
        public ActionResult Edit(int id)
        {
            var service = NewEmployeeService();
            var detail = service.GetEmployeeByID(id);
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
                Current = detail.Current
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (int id, EmployeeEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.EmployeeID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);

            }
            var service = NewEmployeeService();
            if (service.UpdateEmployee(model))
            {
                TempData["SaveResult"] = "Customer info was updated successfully!";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Customer info could not be updated");
            return View(model);
        }
    }
}