using HR_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Management_System.Controllers
{
    public class SalaryController : Controller
    {

        EazisolsEntities4 db = new EazisolsEntities4();
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
            int admin = Convert.ToInt32(Session["Userid"]);
            var cmplist = db.Companies.Where(a=>a.UserId== admin).ToList();
            var Cmplist = new SelectList(cmplist, "CmpId", "CmpName");
            ViewBag.cmp = Cmplist;

            var deptlist = db.Departments.Where(a => a.UserId == admin).ToList();
            var Deplist = new SelectList(deptlist, "DeptId", "DeptName");
            ViewBag.dept = Deplist;
            var emplist = db.Employees.Where(a => a.UserId == admin).ToList();
            var Emplist = new SelectList(emplist, "EmpId", "LName");
            ViewBag.emp = Emplist;
            var role = db.Roles.ToList();
            var Role = new SelectList(role, "RoleId", "RoleType");
            ViewBag.role = Role;

            if (Session["Userid"] == null)
            {
                return RedirectToAction("Login", "Pages");
            }
            else
            {
                //List<Salary> Salary_List = db.Salaries.Where(a => a.UserId == admin).ToList();
                var Salary_List = (from q in db.Employees
                                   join s in db.Salaries on q.EmpId equals s.EmpId
                                   where q.UserId == admin
                                   select new { s.SalaryId, s.Basic, s.Allowance, s.Medical, s.Tax, s.LabourWelfare, s.Fund, s.NetSalary, s.Company.CmpName, s.Department.DeptName, s.Employee.LName, s.UserId, q.Role.RoleType,q.EmpImage}).ToList();
                List<SalaryViewmodell> Salary_View_Model = new List<SalaryViewmodell>();
                foreach (var x in Salary_List)
                {
                    Salary_View_Model.Add(new SalaryViewmodell()
                    {
                        SalaryId = x.SalaryId,
                        Basic = x.Basic,
                        Allowance = x.Allowance,
                        Medical = x.Medical,
                        Tax = x.Tax,
                        LabourWelfare = x.LabourWelfare,
                        Fund = x.Fund,
                        NetSalary = x.NetSalary,
                        C_Name = x.CmpName,
                        D_Name = x.DeptName,
                        E_Name = x.LName,
                        E_Image = x.EmpImage,

                        //U_Id = x.User.Login,
                        RType = x.RoleType,
                        UserId = x.UserId
                    });
                }
                
                //List<SalaryViewmodell> Salary_View_List = Salary_List.Select(x => new SalaryViewmodell

                //{

                //    SalaryId = x.SalaryId,
                //    Basic = x.Basic,
                //    Allowance = x.Allowance,
                //    Medical = x.Medical,
                //    Tax = x.Tax,
                //    LabourWelfare = x.LabourWelfare,
                //    Fund = x.Fund,
                //    NetSalary = x.NetSalary,
                //    C_Name = x.Company.CmpName,
                //    D_Name = x.Department.DeptName,
                //    E_Name = x.Employee.LName,
                //    E_Image = x.Employee.EmpImage,

                //    //U_Id = x.User.Login,
                //    //RType = x.Role.RoleType,
                //    CmpId = x.CmpId,
                //    DeptId = x.DeptId,
                //    EmpId = x.EmpId,
                //    //RoleId = x.RoleId,
                //    UserId = x.UserId




                //}).ToList();

                ViewBag.sal = Salary_View_Model;
                return View();
            }
           

        }
        [HttpPost]
        [ActionName("Salary")]
        public ActionResult Salary(Salary salary)
        {
            salary.UserId = Convert.ToInt32(Session["Userid"]);
            var Company_Emaill = db.Salaries.Where(a => a.CmpId == salary.CmpId && a.DeptId == salary.DeptId && a.UserId == salary.UserId ).FirstOrDefault();

            if (Company_Emaill != null)
            {
                ViewBag.exit = "Employee Already Exist";
            }
            else
            {
                try
                { 
                    db.Salaries.Add(salary);

                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        ViewBag.insert = "<script>alert('Data Inserted!!')</script>";
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.insert = "<script>alert('Data not Inserted!!')</script>";
                    }
                    return RedirectToAction("Salary", "Salary");
                }
                catch
                {
                    return RedirectToAction("Salary", "Salary");
                }

            }
            return RedirectToAction("Salary", "Salary");



        }
        [HttpGet]
        public JsonResult Edit(int id)
        {
            var list = db.Salaries.Where(model => model.SalaryId == id).FirstOrDefault();
            Salary sal = new Salary
            {
                SalaryId = list.SalaryId,
                Basic = list.Basic,
                Allowance = list.Allowance,
                Medical = list.Medical,
                Tax = list.Tax,
                LabourWelfare = list.LabourWelfare,
                Fund = list.Fund,
                NetSalary = list.NetSalary,
                CmpId = list.CmpId,
                DeptId =list.DeptId,
                EmpId=list.EmpId,
                //RoleId = list.RoleId
            };


            return Json(sal, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(Salary sal)
        {
            db.Entry(sal).State = EntityState.Modified;
            int a = db.SaveChanges();
            if (a > 0)
            {
                ViewBag.edit = "<script>alert('Data Updated!!')</script>";
                ModelState.Clear();
            }
            else
            {
                ViewBag.edit = "<script>alert('Data not Updated!!')</script>";
            }


            return RedirectToAction("Salary", "Salary");
        }

        public JsonResult delete(int Salaryid)
        {


           
                bool result = false;
                var a = db.Salaries.FirstOrDefault(y => y.SalaryId == Salaryid);
                if (a != null)
                {
                    db.Salaries.Remove(a);
                    db.SaveChanges();
                    result = true;
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            

                


            
            return Json(JsonRequestBehavior.AllowGet);



        }

        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    //return View(db.Salaries.Where(model => model.SalaryId == id).FirstOrDefault());
        //    var list = db.Salaries.Where(model => model.SalaryId == id).FirstOrDefault();
        //    Salary salary_list = new Salary
        //    {
        //        SalaryId = list.SalaryId,
        //        Basic = list.Basic,
        //        Allowance = list.Allowance,
        //        Medical = list.Medical,
        //        Tax = list.Tax,
        //        LabourWelfare = list.LabourWelfare,
        //        Fund = list.Fund,
        //        NetSalary = list.NetSalary
        //        //CmpId = list.CmpId,
        //        //DeptId = list.DeptId,
        //        //EmpId = list.EmpId


        //    };
        //    return Json(salary_list, JsonRequestBehavior.AllowGet);
        //    //return View(list);
        //}

        //[HttpPost]
        //[ActionName("Delete")]
        //public ActionResult Delete1()
        //{
        //    //string id = Request["cmpid1"];
        //    //Salary sal= db.Salaries.Where(model => model.SalaryId == id).FirstOrDefault();
        //    //db.Salaries.Remove(sal);
        //    //db.SaveChanges();
        //    //db.Entry(sal).State = EntityState.Deleted;
        //    //db.SaveChanges();
        //    //if (a > 0)
        //    //{
        //    //    ViewBag.delete = "<script>alert('Data Deleted!!')</script>";
        //    //    ModelState.Clear();
        //    //}
        //    //else
        //    //{
        //    //    ViewBag.delete = "<script>alert('Data not Deleted!!')</script>";
        //    //}

        //    string id = Request["empid2"];


        //    DBManager.DeleteSalary(id);
        //    return RedirectToAction("Salary", "Salary");


        //}

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

            List<Salary> Salary_List = db.Salaries.ToList();

            SalaryViewmodell Salary_View_Model = new SalaryViewmodell();
            List<SalaryViewmodell> Salary_View_List = Salary_List.Select(x => new SalaryViewmodell

            {

                SalaryId = x.SalaryId,
                Basic = x.Basic,
                Allowance = x.Allowance,
                Medical = x.Medical,
                Tax = x.Tax,
                LabourWelfare = x.LabourWelfare,
                Fund = x.Fund,
                NetSalary = x.NetSalary,
                C_Name = x.Company.CmpName,
                D_Name = x.Department.DeptName,
                E_Name = x.Employee.LName,
                //U_Id = x.User.Login,
                //RType = x.Role.RoleType,
                CmpId = x.CmpId,
                DeptId = x.DeptId,
                EmpId = x.EmpId,
                //RoleId = x.RoleId,
                UserId = x.UserId




            }).ToList();

            ViewBag.salary = Salary_View_List;
            return View();
        }
        [HttpGet]
        public ActionResult EmpSalarySlip(int sal)
        {
            //List<Salary> Salary_List = db.Salaries.ToList();

            //SalaryViewmodell Salary_View_Model = new SalaryViewmodell();
            //List<SalaryViewmodell> Salary_View_List = Salary_List.Select(x => new SalaryViewmodell

            //{

            //    SalaryId = x.SalaryId,
            //    Basic = x.Basic,
            //    Allowance = x.Allowance,
            //    Medical = x.Medical,
            //    Tax = x.Tax,
            //    LabourWelfare = x.LabourWelfare,
            //    Fund = x.Fund,
            //    NetSalary = x.NetSalary,
            //    C_Name = x.Company.CmpName,
            //    D_Name = x.Department.DeptName,
            //    E_Name = x.Employee.LName,
            //    //U_Id = x.User.Login,
            //    RType = x.Role.RoleType,
            //    CmpId = x.CmpId,
            //    DeptId = x.DeptId,
            //    EmpId = x.EmpId,
            //    RoleId = x.RoleId,
            //    UserId = x.UserId




            //}).ToList();

            //ViewBag.EmpSalary = Salary_View_List;

            return View(db.Salaries.Where(x=>x.SalaryId==sal).FirstOrDefault());
        }


        public JsonResult Getdept(int CompanyIDf)

        {

            // var kashiiiiii = DB.Table_User.Where(x => x.User_Company_fk == CompanyIDf).ToList();

            DepartmentViewModel Departtttttt = new DepartmentViewModel();

            List<Department> Leader_Li = db.Departments.Where(x => x.CmpId == CompanyIDf).ToList();
            List<DepartmentViewModel> Leader_Listtt = Leader_Li.Select(x => new DepartmentViewModel

            {
                DeptId = x.DeptId,
                DeptName = x.DeptName

            }).ToList();


            return Json(Leader_Listtt, JsonRequestBehavior.AllowGet);

        }



        public JsonResult GetEmp(int DepartmentIDf)

        {

            // var kashiiiiii = DB.Table_User.Where(x => x.User_Company_fk == CompanyIDf).ToList();

            EmployeeViewModel Employeeeee = new EmployeeViewModel();

            List<Employee> Emp_Li = db.Employees.Where(x => x.DeptId == DepartmentIDf).ToList();
            List<EmployeeViewModel> Employee_Listtt = Emp_Li.Select(x => new EmployeeViewModel

            {
                EmpId = x.EmpId,
                LName = x.LName

            }).ToList();


            return Json(Employee_Listtt, JsonRequestBehavior.AllowGet);

        }

    }
}
