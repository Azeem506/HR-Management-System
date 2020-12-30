using HR_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Management_System.Controllers
{
    public class SalaryController : Controller
    {

        EazisolsEntities3 db = new EazisolsEntities3();
        // GET: Salary
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Salary()
        //{

            //var emplist = db.Employees.ToList();
            //ViewBag.emp = emplist;
            //var cmplist = db.Companies.ToList();
            //ViewBag.cmp = cmplist;
            //var deptlist = db.Departments.ToList();
            //ViewBag.dept = deptlist;
            
        //    return View();
        //}

        //public ActionResult AddSalary()
        //{
            [HttpGet]
            public ActionResult Salary()
            {
            var cmplist = db.Companies.ToList();
            var Cmplist = new SelectList(cmplist, "CmpId", "CmpName");
            ViewBag.cmp = Cmplist;
            var deptlist = db.Departments.ToList();
            var Deplist = new SelectList(deptlist, "DeptId", "DeptName");
            ViewBag.dept = Deplist;
            var emplist = db.Employees.ToList();
            var Emplist = new SelectList(emplist, "EmpId", "LName");
            ViewBag.emp = Emplist;
            return View();
            }
            [HttpPost]
            [ActionName("Salary")]
            public ActionResult Salary(Salary user)
            {
                try
                {
                    
                        db.Salaries.Add(user);
                        db.SaveChanges();

                return RedirectToAction("Salary","Salary");
            }
                catch
                {
                return RedirectToAction("Salary", "Salary");
            }
            }

            //string empcompany = Request["empcompany"];
            //string empdept = Request["empdept"];
            //string empsal = Request["empsal"];
            //string basicsal = Request["basicsal"];
            //string allowance = Request["allowance"];
            //string medallowance = Request["medallowance"];
            //string tax = Request["tax"];
            //string labour = Request["labour"];
            //string fund = Request["fund"];
            //string totalsal = Request["totalsal"];


            //DBManager.AddSalary(basicsal, allowance, medallowance, tax, labour, fund, totalsal, empcompany, empdept, empsal);

            //return View();
        //}

        public ActionResult PaySlip()
        {
            return View();
        }
    }
}