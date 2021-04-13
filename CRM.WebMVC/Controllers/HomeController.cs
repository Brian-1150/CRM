using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "This is a CRM and scheduler app designed for small service based businesses";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "We want to hear from you.  Please feel free to reach out with questions or feedback";

            return View();
        }
    }
}