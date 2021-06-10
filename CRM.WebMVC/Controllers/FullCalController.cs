using CRM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.WebMVC.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    public class FullCalController : Controller
    {
        FullCalService _svc = new FullCalService();
        CalendarEventService _calEventSvc = new CalendarEventService();





        //CREATE
        [HttpGet]
        public ActionResult CreateFullCalEvent(string start)
        {
            //DateTime x = DateTime.Parse(start);
            //return RedirectToAction("CreateFromFullCal", "CalendarEvent", x);
            var model = _calEventSvc.FullCalendarEventCreateView(DateTime.Parse(start));
            return View(model);
        }



        ////CREATE
        //[HttpGet]
        //public ActionResult CreateFullCalEvent(string start)
        //{
        //    //return RedirectToAction("Create", "CalendarEvent");
        //    var model = _calEventSvc.CalendarEventCreateView();
        //    return View("myView", model);
        //}



        // GET: FullCal
        public ActionResult TheIndex()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetEventData()
        {
            return new JsonResult { Data = _svc.GetFullCalEvents(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public ActionResult GetFullCalEventByID(string id)
        {
            return new JsonResult { Data = _svc.GetByID(Convert.ToInt32(id)), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public void UpdateFullCalEvent(string id, string start, string end)
        {

            _svc.UpdateEvent(id, DateTime.Parse(start), DateTime.Parse(end));
        }
    }
}