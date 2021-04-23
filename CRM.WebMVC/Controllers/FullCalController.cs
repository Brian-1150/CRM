using CRM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.WebMVC.Controllers
{
    public class FullCalController : Controller
    {
        FullCalService _svc = new FullCalService();
        CalendarEventService _calEventSvc = new CalendarEventService();


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