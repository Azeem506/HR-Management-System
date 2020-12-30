using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Management_System.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Home()
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Pages/Login");
            }
            return View();
        }
        public ActionResult Eazisols()
        {

            return View();
        }

    }
}