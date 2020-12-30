using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using HR_Management_System.Models;

namespace HR_Management_System.Controllers
{
    public class DepartmentController : Controller
    {

        EazisolsEntities3 db = new EazisolsEntities3();
        // GET: Department
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Department()
        {
            var deptlist = db.Departments.ToList();
            ViewBag.dept = deptlist;
            var cmplist = db.Companies.ToList();
            ViewBag.cmp = cmplist;
            return View();
            
        }
        public ActionResult AddDepartment()
        {
            string user = Request["username"];
            string password = Request["password"];
            string mail = Request["email"];
            string departmentName = Request["dname"];
            string phone = Request["phone"];
            string empcomp = Request["empcompany"];
            

            DBManager.AddDepartment(user,password,mail, phone, departmentName, empcomp);
            
            return RedirectToAction("Department","Department");
        }
        //public ActionResult EditDepartment()
        //{
        //    return View();

        //}
        [HttpPost]
        public ActionResult EditDepartment()
        {
            string id = Request["did1"];
            ViewBag.id = id;
            string user = Request["usernameedit"];
            string password = Request["password1"];
            string mail = Request["email1"];
            string companyName = Request["dname1"];
            string phone = Request["phone1"];
            

            DBManager.UpdateDepartment(id, user, password, mail, phone, companyName);
            //List<CompanyDTO> cmplist = new List<CompanyDTO>();
            //cmplist = DBManager.ShowCompany();
            //ViewBag.list = cmplist;

            return RedirectToAction("Department", "Department");
        }


        //Show data in Pop Up Model from DB
        [HttpGet]
        public JsonResult EditDepartment1(int DesignationiD)
        {


            var list = db.Departments.Where(x => x.DeptId == DesignationiD).SingleOrDefault();


            Department designation_list = new Department
            {
                DeptId = list.DeptId,
                DeptName = list.DeptName,
                UserName = list.UserName,
                Email = list.Email,
                Phone = list.Phone,
                Password = list.Password



            };
            return Json(designation_list, JsonRequestBehavior.AllowGet);/*model*/
        }

        [HttpGet]
        public JsonResult DelDepartment(int DesignationiD)
        {


            var list = db.Departments.Where(x => x.DeptId == DesignationiD).SingleOrDefault();


            Department designation_list = new Department
            {
                DeptId = list.DeptId,
                DeptName = list.DeptName,
                UserName = list.UserName,
                Email = list.Email,
                Phone = list.Phone



            };
            return Json(designation_list, JsonRequestBehavior.AllowGet);/*model*/
        }

        [HttpPost]
        public ActionResult DelDepartment()
        {
            string id = Request["delete1"];


            DBManager.DeleteDepartment(id);
            return RedirectToAction("Department", "Department");
        }
    }
}