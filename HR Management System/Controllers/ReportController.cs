using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Management_System.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExpReport()
        {
            return View();
        }

        public ActionResult EditReport()
        {
            return View();
        }
    }
}