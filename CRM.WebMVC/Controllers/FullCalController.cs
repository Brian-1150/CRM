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
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetEventData()
        {
            return new JsonResult { Data = _svc.GetFullCalEvents(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //public ActionResult GetEventDetailByEventId(string EventId)
        //{

        //}
    }
}