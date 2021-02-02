using HR_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Management_System.Controllers
{
    public class CmpEmployeeController : Controller
    {

        EazisolsEntities4 db = new EazisolsEntities4();
        // GET: CmpEmployee
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CmpEmployee()
        {
            int cmp = Convert.ToInt32(Session["CompanyId"]);
            //var cmplist = db.Companies.Where(a=>a.CmpId== cmp).ToList();
            //var Cmplist = new SelectList(cmplist, "CmpId", "CmpName");
            //ViewBag.cmp = Cmplist;
           var deptlist = db.Departments.Where(a => a.CmpId == cmp).ToList();
            var Deplist = new SelectList(deptlist, "DeptId", "DeptName");
            ViewBag.dept = Deplist;
            //var emplist = db.Employees.Where(a => a.CmpId == cmp).ToList();
            //var Emplist = new SelectList(emplist, "EmpId", "LName");
            //ViewBag.emp = emplist;
            var role = db.Roles.ToList();
            var Role = new SelectList(role, "RoleId", "RoleType");
            ViewBag.role = Role;


            if (Session["CompanyId"] == null)
            {
                return RedirectToAction("Login", "Pages");
            }
            else
            {
                List<Employee> Salary_List = db.Employees.Where(a => a.CmpId == cmp).ToList();
                //var Salary_List = (from q in db.Employees
                //                   join s in db.Salaries on q.EmpId equals s.EmpId
                //                   where q.UserId == admin
                //                   select new { s.SalaryId, s.Basic, s.Allowance, s.Medical, s.Tax, s.LabourWelfare, s.Fund, s.NetSalary, s.Company.CmpName, s.Department.DeptName, s.Employee.LName, s.UserId, q.Role.RoleType, q.EmpImage }).ToList();
                List<EmployeeViewModel> Employee_View_Model = new List<EmployeeViewModel>();
                foreach (var x in Salary_List)
                {
                    Employee_View_Model.Add(new EmployeeViewModel()
                    {
                        EmpId = x.EmpId,
                        EmpImage = x.EmpImage,
                        FName = x.FName,
                        LName = x.LName,
                        R_Type=x.Role.RoleType,
                        
                    });
                }

                //var company_iddd = Convert.ToInt32(Session["CompanyId"].ToString());
                //var Employee_List = db.Employees.Where(x => x.CmpId == company_iddd).ToList();

                ViewBag.Employeees = Employee_View_Model;

               
            }


            return View();
        }
        [HttpPost]
        [ActionName("CmpEmployee")]
        public ActionResult CmpEmployee(Employee emp)
        {

            emp.CmpId = Convert.ToInt32(Session["CompanyId"].ToString());
            var Company_Emaill = db.Employees.Where(a => a.CmpId == emp.CmpId && a.DeptId == emp.DeptId  && a.UserName == emp.UserName && a.Password == emp.Password && a.RoleId==emp.RoleId || a.UserName == emp.UserName && a.Password == emp.Password).FirstOrDefault();

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
                    
                    //emp.UserId = Convert.ToInt32(Session["Userid"]);
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
                    return RedirectToAction("CmpEmployee", "CmpEmployee");
                }
                catch
                {
                    return RedirectToAction("CmpEmployee", "CmpEmployee");
                }
                
            }
            return RedirectToAction("CmpEmployee", "CmpEmployee");





        }
        [HttpPost]
        public ActionResult EditCmpEmployee(Employee emp)
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


            return RedirectToAction("CmpEmployee", "CmpEmployee");
        }


        //Show data in Pop Up Model from DB
        [HttpGet]
        public JsonResult EditCmpEmployee1(int DesignationiD)
        {


            var list = db.Employees.Where(x => x.EmpId == DesignationiD).SingleOrDefault();


            Employee designation_list = new Employee
            {
                EmpId = list.EmpId,
                FName = list.FName,
                LName = list.LName,
                UserName = list.UserName,
                Password = list.Password,
                EmpAddress = list.EmpAddress,
                JoiningDate = list.JoiningDate,
                IsHOD = list.IsHOD,
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


        public JsonResult DeleteCmpEmployee(int Employeeid)
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

        public ActionResult CmpEmpProfile(int id)
        {
            //int cmp = Convert.ToInt32(Session["CompanyId"]);

            //var detail = (from q in db.Employees
            //                   join s in db.Roles on q.RoleId equals s.RoleId
            //                   where q.CmpId == cmp
            //                   select new { s.RoleType,q.EmpId,q.FName,q.LName,q.EmpImage,q.Phone,q.Email,q.EmpAddress,q.JoiningDate,q.Gender }).ToList();
            //ViewBag.cmpempprofile = detail;
            var detail = db.Employees.Find(id);
            
            return View(detail);
        }

        
        [HttpGet]
        public ActionResult CmpEmpSalary()
        {
            var cmplist = db.Companies.ToList();
            var Cmplist = new SelectList(cmplist, "CmpId", "CmpName");
            ViewBag.cmp = Cmplist;
            int CmpId = Convert.ToInt32(Session["CompanyId"].ToString());
            var deptlist = db.Departments.Where(a=>a.CmpId== CmpId).ToList();
            var Deplist = new SelectList(deptlist, "DeptId", "DeptName");
            ViewBag.dept = Deplist;
            var emplist = db.Employees.ToList();
            var Emplist = new SelectList(emplist, "EmpId", "LName");
            ViewBag.emp = Emplist;
            var role = db.Roles.ToList();
            var Role = new SelectList(role, "RoleId", "RoleType");
            ViewBag.role = Role;
           
            
            
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
                E_Image = x.Employee.EmpImage,

                //U_Id = x.User.Login,
                //RType = x.Role.RoleType,
                CmpId = x.CmpId,
                DeptId = x.DeptId,
                EmpId = x.EmpId,
                //RoleId = x.RoleId,
                UserId = x.UserId

            }).ToList();

            ViewBag.sal = Salary_View_List;

            return View();
        }
        [HttpPost]
        [ActionName("CmpEmpSalary")]
        public ActionResult CmpEmpSalary(Salary salary)
        {

            //try
            //{
                salary.CmpId = Convert.ToInt32(Session["CompanyId"].ToString());
                salary.UserId = Convert.ToInt32(Session["Userid"]);

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
                return RedirectToAction("CmpEmpSalary", "CmpEmployee");
            //}
            //catch(Exception e)
            //{
                
            //    return RedirectToAction("CmpEmpSalary", "CmpEmployee");
            //}
        }
        [HttpGet]
        public JsonResult EditCmpEmpSalary(int id)
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
                DeptId = list.DeptId,
                EmpId = list.EmpId,
                //RoleId = list.RoleId
            };


            return Json(sal, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditCmpEmpSalary(Salary sal)
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


            return RedirectToAction("CmpEmpSalary", "CmpEmployee");
        }

        public JsonResult DeleteCmpEmpSal(int Salaryid)
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

        

        public ActionResult CmpEmpPaySlip()
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

            ViewBag.CmpEmpsalary = Salary_View_List;
            return View();
        }
        [HttpGet]
        public ActionResult CmpEmpSalarySlip(int sal)
        {
            
            return View(db.Salaries.Where(x => x.SalaryId == sal).FirstOrDefault());
        }



        public ActionResult CmpMeeting()
        {
            var cmplist = db.Companies.ToList();
            var Cmplist = new SelectList(cmplist, "CmpId", "CmpName");
            ViewBag.cmpppp = Cmplist;
            int CmpId = Convert.ToInt32(Session["CompanyId"].ToString());
            var deptlist = db.Departments.Where(a=>a.CmpId== CmpId).ToList();
            var Deplist = new SelectList(deptlist, "DeptId", "DeptName");
            ViewBag.dept = Deplist;
            var emplist = db.Employees.ToList();
            var Emplist = new SelectList(emplist, "EmpId", "LName");
            ViewBag.emp = Emplist;

            List<Meeting> Meeting_List = db.Meetings.ToList();
            MeetingViewModel meetingViewModel = new MeetingViewModel();
            List<MeetingViewModel> MeetingList = Meeting_List.Select(x => new MeetingViewModel
            {
                MeetingId = x.MeetingId,
                C_Name = x.Company.CmpName,
                D_Name = x.Department.DeptName,
                E_Name = x.Employee.LName,
                E_Image = x.Employee.EmpImage,
                Date = x.Date,
                Time = x.Time,
                Message = x.Message,
                CmpId = x.CmpId,
                DeptId = x.DeptId,
                EmpId = x.EmpId,
                UserId = x.UserId



            }).ToList();

            ViewBag.Meeting = MeetingList;

            return View();
        }


        [HttpGet]
        public ActionResult AddCmpMeeting()
        {
            var cmplist = db.Companies.ToList();
            var Cmplist = new SelectList(cmplist, "CmpId", "CmpName");
            ViewBag.cmpppp = Cmplist;
            var deptlist = db.Departments.ToList();
            var Deplist = new SelectList(deptlist, "DeptId", "DeptName");
            ViewBag.dept = Deplist;
            var emplist = db.Employees.ToList();
            var Emplist = new SelectList(emplist, "EmpId", "LName");
            ViewBag.emp = Emplist;
            return RedirectToAction("CmpMeeting", "CmpEmployee");
        }

        [HttpPost]
        //[ActionName("Meeting")]
        public ActionResult AddCmpMeeting(Meeting meeting)
        {


            try
            {
                meeting.CmpId = Convert.ToInt32(Session["CompanyId"].ToString());
                //meeting.UserId = Convert.ToInt32(Session["Userid"]);
                db.Meetings.Add(meeting);

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
                return RedirectToAction("CmpMeeting", "CmpEmployee");
            }
            catch
            {
                return RedirectToAction("CmpMeeting", "CmpEmployee");
            }
        }


        [HttpGet]
        public JsonResult EditCmpMeeting(int DesignationiD)
        {
            var list = db.Meetings.Where(x => x.MeetingId == DesignationiD).SingleOrDefault();


            Meeting meeting_list = new Meeting
            {
                MeetingId = list.MeetingId,
                Date = list.Date,
                Time = list.Time,
                CmpId = list.CmpId,
                DeptId = list.DeptId,
                EmpId = list.EmpId,
                Message = list.Message

            };
            return Json(meeting_list, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ActionName("EditCmpMeeting")]
        public ActionResult EditCmpMeeting1(Meeting meeting)
        {
            db.Entry(meeting).State = EntityState.Modified;
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






            return RedirectToAction("CmpMeeting", "CmpEmployee");
        }




        public JsonResult DeleteCmpMeeting(int Meetingid)
        {



            bool result = false;
            var a = db.Meetings.FirstOrDefault(y => y.MeetingId == Meetingid);
            if (a != null)
            {
                db.Meetings.Remove(a);
                db.SaveChanges();
                result = true;
                return Json(result, JsonRequestBehavior.AllowGet);
            }






            return Json(JsonRequestBehavior.AllowGet);



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