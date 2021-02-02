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

        EazisolsEntities4 db = new EazisolsEntities4();
        // GET: Employee
        public ActionResult Index()
        {
            
                return View();

        }

        [HttpGet]
        public ActionResult Employee()
        {
            int admin = Convert.ToInt32(Session["Userid"]);
            var cmplist = db.Companies.Where(a=>a.UserId== admin).ToList();
            var Cmplist = new SelectList(cmplist, "CmpId", "CmpName");
            ViewBag.cmp = Cmplist;
            var deptlist = db.Departments.ToList();
            var Deplist = new SelectList(deptlist, "DeptId", "DeptName");
            ViewBag.dept = Deplist;
            var role = db.Roles.ToList();
            var Role = new SelectList(role, "RoleId", "RoleType");
            ViewBag.role = Role;

            
            var emplist = db.Employees.Where(a => a.UserId == admin).ToList();
            var Emplist = new SelectList(emplist, "EmpId", "LName");
            ViewBag.emp = emplist;


            //List<Employee> Employee_List = db.Employees.ToList();

            //EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            //List<EmployeeViewModel> Employee_View_List = Employee_List.Select(x => new EmployeeViewModel

            //{
            //    EmpId = x.EmpId,
            //    FName = x.FName,
            //    LName = x.LName,
            //    Email = x.Email,
            //    Phone = x.Phone,
            //    IsHOD = x.IsHOD,
            //    EmpImage = x.EmpImage,
            //    CmpId = x.CmpId,
            //    DeptId = x.DeptId,
            //    UserId = x.UserId,
            //    RoleId = x.RoleId,
            //    SalaryId = x.SalaryId,
            //    R_Type = x.Role.RoleType


            //}).ToList();
            //ViewBag.employee = Employee_View_List;

            //var emplist = db.Employees.ToList();

            //ViewBag.emp = emplist;
            //var cmplist = db.Companies.ToList();
            //var Cmplist = new SelectList(cmplist, "CmpId", "CmpName");
            //ViewBag.cmp = Cmplist;
            //var deptlist = db.Departments.ToList();
            //ViewBag.dept = deptlist;
            //var emprole = db.Roles.ToList();
            //ViewBag.role = emprole;

            return View();
        }
        [HttpPost]
        [ActionName("Employee")]
        public ActionResult Employee(Employee emp)
        {
            //string empfirstname = Request["empfirstname"];
            //string emplastname = Request["emplastname"];
            //string empusername = Request["empusername"];
            //string empemail = Request["empemail"];
            //string emppassword = Request["emppassword"];
            //string empid = Request["empid"];
            //string joindate = Request["joindate"];
            //string empphone = Request["empphone"];
            //string empimage = Request["empimage"];
            //string empcomp = Request["empcompany"];
            //string empdept = Request["empdept"];
            //string emprole = Request["emprole"];
            //string ishod = Request["ishod"];

            emp.UserId = Convert.ToInt32(Session["Userid"]);
            var Company_Emaill = db.Employees.Where(a => a.CmpId == emp.CmpId && a.DeptId == emp.DeptId && a.UserId == emp.UserId && a.UserName==emp.UserName && a.Password==emp.Password && a.RoleId==emp.RoleId || a.UserName == emp.UserName && a.Password == emp.Password).FirstOrDefault();

            if (Company_Emaill != null)
            {
                ViewBag.exit = "Employee Already Exist";
            }
            else
            {
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
                        var filepath = System.IO.Path.Combine("\\Content", uniqueName);
                        file.SaveAs(fileSavePath);
                        emp.EmpImage = filepath;
                        // DBManager.AddEmployee(empfirstname, emplastname, empusername, emppassword, empemail, empphone, a, empcomp, empdept, emprole, ishod);
                    }
                }



                try
                {

                    db.Employees.Add(emp);

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
                    return RedirectToAction("Employee", "Employee");
                }
                catch
                {
                    return RedirectToAction("Employee", "Employee");
                }
            }
            return RedirectToAction("Employee", "Employee");

        }



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








        [HttpPost]
        public ActionResult EditEmployee(Employee emp)
        {

            var uniqueName = "";
            if (Request.Files["empimage1"] != null)
            {
                var file = Request.Files["empimage1"];
                if (file.FileName != "")
                {
                    var ext = System.IO.Path.GetExtension(file.FileName);
                    uniqueName = Guid.NewGuid().ToString() + ext;
                    var rootPath = Server.MapPath("/Content");
                    var fileSavePath = System.IO.Path.Combine(rootPath, uniqueName);
                    var filepath = System.IO.Path.Combine("\\Content", uniqueName);
                    file.SaveAs(fileSavePath);
                    emp.EmpImage = filepath;
                    // DBManager.AddEmployee(empfirstname, emplastname, empusername, emppassword, empemail, empphone, a, empcomp, empdept, emprole, ishod);
                }
            }


            emp.UserId = Convert.ToInt32(Session["Userid"]);
            db.Entry(emp).State = EntityState.Modified;
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




            //string id = Request["hiddenid"];
            //string fname = Request["editfname"];
            //string lname= Request["editlname"];
            //string user = Request["editusername"];
            //string password = Request["editpass"];
            //string mail = Request["editemail"];
            //string phone = Request["editphone"];
            //string image = Request["editimage"];
            //DBManager.UpdateEmployee(id, fname, lname, user, password, mail, phone);


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
                EmpAddress=list.EmpAddress,
                JoiningDate=list.JoiningDate,
                IsHOD=list.IsHOD,
                Email = list.Email,
                Phone = list.Phone,
                EmpImage = list.EmpImage,
                CmpId = list.CmpId,
                DeptId = list.DeptId,
                RoleId = list.RoleId,
                Gender = list.Gender

            };
            return Json(designation_list, JsonRequestBehavior.AllowGet);/*model*/
        }


        public JsonResult DeleteEmployee(int Employeeid)
        {



            bool result = false;
            var a = db.Employees.FirstOrDefault(y => y.EmpId == Employeeid);
            if (a != null)
            {
                db.Employees.Remove(a);
                db.SaveChanges();
                result = true;
                return Json(result, JsonRequestBehavior.AllowGet);
            }






            return Json(JsonRequestBehavior.AllowGet);



        }

        //[HttpGet]
        //public JsonResult DelEmployee(int DesignationiD)
        //{


        //    var list = db.Employees.Where(x => x.EmpId == DesignationiD).SingleOrDefault();


        //    Employee designation_list = new Employee
        //    {
        //        EmpId = list.EmpId,
        //        FName = list.FName,
        //        LName = list.LName,
        //        UserName = list.UserName,
        //        Password = list.Password,
        //        Email = list.Email,
        //        Phone = list.Phone,
        //        CmpId = list.CmpId,
        //        DeptId = list.DeptId,
        //        RoleId = list.RoleId,
        //        UserId=list.UserId,
        //        SalaryId=list.SalaryId



        //    };
        //    return Json(designation_list, JsonRequestBehavior.AllowGet);/*model*/
        //}

        //[HttpPost]
        //public ActionResult DelEmployee()
        //{
        //    string id = Request["empid1"];


        //    DBManager.DeleteEmployee(id);
        //    return RedirectToAction("Employee", "Employee");
        //}

        public ActionResult EmpProfile(int id)
        {
            //var complist = db.Companies.ToList();
            //ViewBag.comp = complist;
            var detail = db.Employees.Find(id);
            return View(detail);
        }

        public ActionResult Emp_Profile()
        {
            if (Session["EmployeeId"] == null)
            {
                return RedirectToAction("Login", "Pages");
            }
            else
            {
                var idddd = Convert.ToInt32(Session["EmployeeId"].ToString());
                var details = db.Employees.Find(idddd);

                return View(details);
            }
        }

        public ActionResult EmpSalarySlip(int sal)
        {

            return View(db.Salaries.Where(x =>x.EmpId == sal).FirstOrDefault());
        }



        public ActionResult Attendance()
        {



            return View();
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
    }

}