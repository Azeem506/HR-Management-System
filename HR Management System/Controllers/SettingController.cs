using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Management_System.Controllers
{
    public class SettingController : Controller
    {
        // GET: Setting
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Setting()
        {
            return View();
        }

        



        public ActionResult RolePermission()
        {
            return View();
        }

        public ActionResult SalarySetting()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}