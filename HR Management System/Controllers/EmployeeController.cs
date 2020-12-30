using HR_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace HR_Management_System.Controllers
{
    public class EmployeeController : Controller
    {

        EazisolsEntities3 db = new EazisolsEntities3();
        // GET: Employee
        public ActionResult Index()
        {
            
                return View();

        }
        public ActionResult Employee()
        {
            var emplist = db.Employees.ToList();
            ViewBag.emp = emplist;
            var cmplist = db.Companies.ToList();
            ViewBag.cmp = cmplist;
            var deptlist = db.Departments.ToList();
            ViewBag.dept = deptlist;
            return View();
        }

        public ActionResult AddEmployee()
        {
            string empfirstname = Request["empfirstname"];
            string emplastname = Request["emplastname"];
            string empusername = Request["empusername"];
            string empemail = Request["empemail"];
            string emppassword = Request["emppassword"];
            //string empid = Request["empid"];
            //string joindate = Request["joindate"];
            string empphone = Request["empphone"];
            //string empimage = Request["empimage"];
            string empcomp = Request["empcompany"];
            string empdept = Request["empdept"];



            var uniqueName = "";
            if (Request.Files["empimage"] != null)
            {
                var file = Request.Files["empimage"];
                if (file.FileName != "")
                {
                    var ext = System.IO.Path.GetExtension(file.FileName);
                    uniqueName = Guid.NewGuid().ToString() + ext;
                    var rootPath = Server.MapPath("/Content");
                    var fileSavePath = System.IO.Path.Combine(rootPath, uniqueName);
                    var a = System.IO.Path.Combine("\\Content", uniqueName);
                    file.SaveAs(fileSavePath);

                    DBManager.AddEmployee(empfirstname, emplastname, empusername, emppassword, empemail, empphone, a, empcomp, empdept);
                }
            }
            return RedirectToAction("Employee", "Employee");
            //DBManager.GetCmpName();

            //List<Company> list = new List<Company>();
            //SelectList cmplist = new SelectList(list, "CmpName", "CmpName");
            //ViewBag.dropdowncmp = cmplist;
            //List<SelectListItem> osrListItems = db.Companies.Where(x => x.CmpName != null).Select(osr => new SelectListItem { Value = osr.CmpName, Text = osr.CmpName }).Distinct().ToList();
            //ViewBag.OSRddl = new SelectList(osrListItems, "Value", "Text").Distinct();


            //List<Company> list = db.Companies.ToList();
            //Company cmpname = new Company();

            //var complist = db.Companies.ToList();
            //ViewBag.comp = complist;

            //ViewBag.CompanyList = new SelectList(list, "CmpId", "CmpName");
            //string empphone = Request["empphone"];
            //string empphone = Request["empphone"];

            //var uniqueName = "";
            //if (Request.Files["Image"] != null)
            //{
            //    var file = Request.Files["Image"];
            //    if (file.FileName != "")
            //    {
            //        var ext = System.IO.Path.GetExtension(file.FileName); s
            //         uniqueName = Guid.NewGuid().ToString() + ext;
            //        var rootPath = Server.MapPath("/Content");
            //        var fileSavePath = System.IO.Path.Combine(rootPath, uniqueName);
            //        var a = System.IO.Path.Combine("\\Content", uniqueName);
            //        file.SaveAs(fileSavePath);

            //        DBManager.AddCompany(companyName, user, password, mail, phone, a);
            //    }
            //}


            //DBManager.AddCompany(companyName, user, password, mail, phone, image);
            //List<CompanyDTO> cmplist = new List<CompanyDTO>();
            //cmplist = DBManager.ShowCompany();
            //ViewBag.list = cmplist;

        }
        [HttpPost]
        public ActionResult EditEmployee()
        {
            string id = Request["hiddenid"];
            string fname = Request["editfname"];
            string lname= Request["editlname"];
            string user = Request["editusername"];
            string password = Request["editpass"];
            string mail = Request["editemail"];
            string phone = Request["editphone"];
            string image = Request["editimage"];
            
            //var uniqueName = "";
            //if (Request.Files["editimage"] != null)
            //{
            //    var file = Request.Files["editimage"];
            //    if (file.FileName != "")
            //    {
            //        var ext = System.IO.Path.GetExtension(file.FileName);
            //        uniqueName = Guid.NewGuid().ToString() + ext;
            //        var rootPath = Server.MapPath("/Content");
            //        var fileSavePath = System.IO.Path.Combine(rootPath, uniqueName);
            //        var a = System.IO.Path.Combine("\\Content", uniqueName);
            //        file.SaveAs(fileSavePath);
            //        DBManager.UpdateEmployee(id, fname, lname, user, password, mail, phone, a);
            //    }
            //    // return RedirectToAction("Employee", "Employee");
            //}
            DBManager.UpdateEmployee(id, fname, lname, user, password, mail, phone);
            //List<CompanyDTO> cmplist = new List<CompanyDTO>();
            //cmplist = DBManager.ShowCompany();
            //ViewBag.list = cmplist;

            return RedirectToAction("Employee", "Employee");
        }


        //Show data in Pop Up Model from DB
        [HttpGet]
        public JsonResult EditEmployee1(int DesignationiD)
        {


            var list = db.Employees.Where(x => x.EmpId == DesignationiD).SingleOrDefault();


            Employee designation_list = new Employee
            {
                EmpId = list.EmpId,
                FName = list.FName,
                LName=list.LName,
                UserName = list.UserName,
                Password = list.Password,
                Email = list.Email,
                Phone = list.Phone,
                EmpImage=list.EmpImage
                
            };
            return Json(designation_list, JsonRequestBehavior.AllowGet);/*model*/
        }

        [HttpGet]
        public JsonResult DelEmployee(int DesignationiD)
        {


            var list = db.Employees.Where(x => x.EmpId == DesignationiD).SingleOrDefault();


            Employee designation_list = new Employee
            {
                EmpId = list.EmpId,
                FName = list.FName,
                LName = list.LName,
                UserName = list.UserName,
                Password = list.Password,
                Email = list.Email,
                Phone = list.Phone



            };
            return Json(designation_list, JsonRequestBehavior.AllowGet);/*model*/
        }

        [HttpPost]
        public ActionResult DelEmployee()
        {
            string id = Request["empid1"];


            DBManager.DeleteEmployee(id);
            return RedirectToAction("Employee", "Employee");
        }

        public ActionResult EmpProfile(int id)
        {
            //var complist = db.Companies.ToList();
            //ViewBag.comp = complist;
            var detail = db.Employees.Find(id);
            return View(detail);
        }

        //public ActionResult GetCompany()
        //{
        //    var cmplist = db.Companies.ToList();
        //    ViewBag.company = cmplist;

        //    return View();
        //}
        //public ActionResult EmpProfile()
        //{
        //    return View();
        //}
        //public ActionResult EditEmployee()
        //{
        //    return View();
        //}
        //public ActionResult AddEmployee()
        //{
        //    return View();
        //}

        //public ActionResult Leave()
        //{
        //    return View();
        //}


        //public ActionResult EditLeave()
        //{
        //    return View();
        //}
        //public ActionResult AddLeave()
        //{
        //    return View();
        //}

        //public ActionResult Holiday()
        //{
        //    return View();
        //}

        //public ActionResult EditHoliday()
        //{
        //    return View();
        //} 


        //public ActionResult AddHoliday()
        //{
        //    return View();
        //}

        //public ActionResult Attendance()
        //{
        //    return View();
        //}


    }

}