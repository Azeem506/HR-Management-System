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
    public class CompanyController : Controller
    {

        EazisolsEntities4 db = new EazisolsEntities4();
        // GET: Company
        //[HttpPost]
        //public ActionResult Index(Company model)
        //{

        //    List<Company> list = db.Companies.ToList();
        //    ViewBag.CompanyList = new SelectList(list, "CmpId", "CmpName");
        //    if (model.CmpId > 0)
        //    {
        //        Company cmp = db.Companies.SingleOrDefault(x => x.CmpId == model.CmpId);

        //        cmp.CmpName=model.CmpName;
        //        cmp.Email=model.Email;
        //        cmp.Password=model.Password;
        //        cmp.Phone=model.Phone;
        //        cmp.Image=model.Image;
        //        db.SaveChanges();
        //    }
        //    return View(model);
        //}
        [HttpGet]
        public ActionResult Company()
        {

            int admin = Convert.ToInt32(Session["Userid"]);
            var complist = db.Companies.Where(a => a.UserId == admin).ToList();
            ViewBag.comp = complist;
            return View();
        }
        [HttpPost]
        [ActionName("Company")]
        public ActionResult Company(Company cmp)
        {
            cmp.UserId = Convert.ToInt32(Session["Userid"]);
            var Company_Emaill = db.Companies.Where(a => a.UserName == cmp.UserName && a.Password==cmp.Password || a.CmpName == cmp.CmpName && a.UserId==cmp.UserId).FirstOrDefault();
            
            if (Company_Emaill != null)
            {
                ViewBag.exit = "Company Already Exist";
            }
            else
            {
                var uniqueName = "";
                if (Request.Files["ImageCompany"] != null)
                {
                    var file = Request.Files["ImageCompany"];
                    if (file.FileName != "")
                    {
                        var ext = System.IO.Path.GetExtension(file.FileName);
                        uniqueName = Guid.NewGuid().ToString() + ext;
                        var rootPath = Server.MapPath("/Content");
                        var fileSavePath = System.IO.Path.Combine(rootPath, uniqueName);
                        var filepath = System.IO.Path.Combine("\\Content", uniqueName);
                        file.SaveAs(fileSavePath);
                        cmp.Image = filepath;
                        // DBManager.AddEmployee(empfirstname, emplastname, empusername, emppassword, empemail, empphone, a, empcomp, empdept, emprole, ishod);
                    }
                }

                    
                    db.Companies.Add(cmp);

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
                    return RedirectToAction("Company", "Company");
                
            }
            return RedirectToAction("Company", "Company");

        }

        //public ActionResult CompanyProfile()
        //{
        //    return View();
        //}

        //public ActionResult EditProfile()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult AddCompany()
        //{
        //    string user = Request["username"];
        //    string password = Request["password"];
        //    string mail = Request["mail"];
        //    string companyName = Request["cname"];
        //    string phone = Request["phone"];
        //    string image = Request["Image"];

        //    var uniqueName = "";
        //    if (Request.Files["Image"] != null)
        //    {
        //        var file = Request.Files["Image"];
        //        if(file.FileName!="")
        //        {
        //            var ext = System.IO.Path.GetExtension(file.FileName);
        //            uniqueName = Guid.NewGuid().ToString() + ext;
        //            var rootPath = Server.MapPath("/Content");
        //            var fileSavePath = System.IO.Path.Combine(rootPath, uniqueName);
        //            var a= System.IO.Path.Combine("\\Content", uniqueName);
        //            file.SaveAs(fileSavePath);

        //            DBManager.AddCompany(companyName, user, password, mail, phone, a);
        //        }
        //    }


        //DBManager.AddCompany(companyName, user, password, mail, phone, image);
        //List<CompanyDTO> cmplist = new List<CompanyDTO>();
        //cmplist = DBManager.ShowCompany();
        //ViewBag.list = cmplist;
        //    return RedirectToAction("Company","Company");
        //}
        //public ActionResult Delete(int id)
        //{
        //    using (EazisolsEntities db = new EazisolsEntities())
        //    {
        //        Company std = db.Companies.Where(x => x.CmpId == id).FirstOrDefault<Company>();
        //        db.Companies.Remove(std);
        //        db.SaveChanges();
        //    }
        //    return Json(true, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult delete(string cmpidd)
        //{

        //    bool result = false;
        //    var cmp = db.Companies.FirstOrDefault(x => x.CmpId == cmpidd);
        //    if (cmp != null)
        //    {
        //        db.Companies.Remove(cmp);
        //        db.SaveChanges();
        //        result = true;
        //    }

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult ShowCompany()
        //{


        //    return RedirectToAction("Company", "Company");
        //}


        //public ActionResult EditCompany(string id)
        //{
        //    string user = Request["username"];
        //    string password = Request["password"];
        //    string confirmPass = Request["cpassword"];
        //    string mail = Request["mail"];
        //    string companyName = Request["cmpname"];
        //    string companyid = Request["cmpid"];
        //    string phone = Request["phone"];
        //    string image = Request["pic1"];
        //    string hid = id;
        //    ViewBag.id= companyid;
        //    if (companyid == hid) {   
        //    DBManager.UpdateCompany(companyid, companyName, user, password, mail, phone, image);
        //    }
        //    return RedirectToAction("Company", "Company");
        //}

        //Update Company

        //Show data in Pop Up Model from DB
        [HttpGet]
        public JsonResult EditCompany1(int DesignationiD)
        {


            var list = db.Companies.Where(x => x.CmpId == DesignationiD).SingleOrDefault();


            Company designation_list = new Company
            {
                CmpId = list.CmpId,
                CmpName = list.CmpName,
                UserName = list.UserName,
                Email = list.Email,
                Phone = list.Phone,
                Password = list.Password,
                Image = list.Image,
                UserId=list.UserId



            };
            return Json(designation_list, JsonRequestBehavior.AllowGet);/*model*/
        }


       




        [HttpPost]
        public ActionResult EditCompany(Company cmp)
        {

            var uniqueName = "";
            if (Request.Files["editCmpImage"] != null)
            {
                var file = Request.Files["editCmpImage"];
                if (file.FileName != "")
                {
                    var ext = System.IO.Path.GetExtension(file.FileName);
                    uniqueName = Guid.NewGuid().ToString() + ext;
                    var rootPath = Server.MapPath("/Content");
                    var fileSavePath = System.IO.Path.Combine(rootPath, uniqueName);
                    var filepath = System.IO.Path.Combine("\\Content", uniqueName);
                    file.SaveAs(fileSavePath);
                    cmp.Image = filepath;
                    cmp.UserId = Convert.ToInt32(Session["Userid"]);
                    // DBManager.AddEmployee(empfirstname, emplastname, empusername, emppassword, empemail, empphone, a, empcomp, empdept, emprole, ishod);
                }
            }



            db.Entry(cmp).State = EntityState.Modified;
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



            //string id = Request["cmpid"];
            //ViewBag.id = id;
            //string user = Request["UserName"];
            //string password = Request["password"];
            //string mail = Request["Email"];
            //string companyName = Request["CmpName"];
            //string phone = Request["Phone"];
            ////string image = Request["Image"];

            //DBManager.UpdateCompany(id,companyName, user, password, mail, phone);
            //List<CompanyDTO> cmplist = new List<CompanyDTO>();
            //cmplist = DBManager.ShowCompany();
            //ViewBag.list = cmplist;
            
            return RedirectToAction("Company", "Company");
        }

        public JsonResult DeleteCompany(int Companyid)
        {



            bool result = false;
            var a = db.Companies.FirstOrDefault(y => y.CmpId == Companyid);
            if (a != null)
            {
                db.Companies.Remove(a);
                db.SaveChanges();
                result = true;
                
            }






            return Json(result, JsonRequestBehavior.AllowGet);



        }
        public ActionResult CompanyProfile(int id)
        {

           
            var Departments_List = db.Departments.Where(x => x.CmpId == id).ToList();

            ViewBag.Departmentss = Departments_List;

            //var complist = db.Companies.ToList();
            //ViewBag.comp = complist;

            var detail = db.Companies.Find(id);
            return View(detail);
        }


        public ActionResult Company_Profile()
        {


            if (Session["CompanyId"] == null)
            {
                return RedirectToAction("Login", "Pages");
            }
            else
            {
                var idddd = Convert.ToInt32(Session["CompanyId"].ToString());
                var Departments_List = db.Departments.Where(x => x.CmpId == idddd).ToList();

                ViewBag.Departmentss = Departments_List;

                

                var details = db.Companies.Find(idddd);

                return View(details);
            }




        }

       
        public ActionResult Company_Departments()
        {
            if (Session["Userid"] != null)
            {
                var Company_List = db.Companies.ToList();

                var Companyyy = new SelectList(Company_List, "CmpId", "CmpName");
                ViewBag.listtt = Companyyy;

                var company_id = Convert.ToInt32(Session["CompanyId"]);
                var Departments_List = db.Departments.Where(x => x.CmpId == company_id).ToList();

                ViewBag.Departmentss = Departments_List;

                
            }

            else if (Session["CompanyId"] == null)
            {
                return RedirectToAction("Login", "Pages");
            }
           
            return View();

        }
        public ActionResult Company_Employees()
        {

            if (Session["CompanyId"] == null)
            {
                var Company_List = db.Companies.ToList();

                var Companyyy = new SelectList(Company_List, "CmpId", "CmpName");
                ViewBag.listtt = Companyyy;

                var company_iddd = Convert.ToInt32(Session["CompanyId"]);
                var Employee_List = db.Employees.Where(x => x.CmpId == company_iddd).ToList();

                ViewBag.Employeees = Employee_List;
            }
            else if (Session["CompanyId"] == null)
            {
                return RedirectToAction("Login", "Pages");
            }
                

                return View();

        }

        //public ActionResult EditCompany(int CmpId) {
        //    var complist = db.Companies.ToList();
        //    ViewBag.comp = complist;

        //    Company model = new Company();
        //   if (CmpId > 0)
        //    {
        //        Company cmp = db.Companies.SingleOrDefault(x => x.CmpId == CmpId);
        //        model.CmpId = cmp.CmpId;
        //        model.CmpName = cmp.CmpName;
        //        model.Email = cmp.Email;
        //        model.Password = cmp.Password;
        //        model.Phone = cmp.Phone;
        //        model.Image = cmp.Image;

        //    }

        //    return View();
        //    //return PartialView("Partial1", model);
        //    //string user = Request["username"];
        //    //string password = Request["password"];
        //    //string mail = Request["mail"];
        //    //string companyName = Request["cmpname"];
        //    //string companyid = Request["cmpid"];
        //    //string phone = Request["phone"];
        //    //string image = Request["pic1"];

        //    //ViewBag.id = companyid;
        //    //DBManager.UpdateCompany(companyid, companyName, user, password, mail, phone, image);


        //}

        //[HttpGet]
        //public JsonResult DelCompany(int DesignationiD)
        //{


        //    var list = db.Companies.Where(x => x.CmpId == DesignationiD).SingleOrDefault();


        //    Company designation_list = new Company
        //    {
        //        CmpId = list.CmpId,
        //        CmpName = list.CmpName,
        //        UserName = list.UserName,
        //        Email = list.Email,
        //        Phone = list.Phone



        //    };
        //    return Json(designation_list, JsonRequestBehavior.AllowGet);/*model*/
        //}

        //[HttpPost]
        //public ActionResult DelCompany()
        //{
        //    string id=Request["cmpid1"];


        //    DBManager.DeleteCompany(id);
        //    return RedirectToAction("Company", "Company");
        //}




        public ActionResult Meeting()
        {

            int admin = Convert.ToInt32(Session["Userid"]);
            var cmplist = db.Companies.Where(a=>a.UserId== admin).ToList();
            var Cmplist = new SelectList(cmplist, "CmpId", "CmpName");
            ViewBag.cmpppp = Cmplist;
            var deptlist = db.Departments.Where(a => a.UserId == admin).ToList();
            var Deplist = new SelectList(deptlist, "DeptId", "DeptName");
            ViewBag.dept = Deplist;
            var emplist = db.Employees.Where(a => a.UserId == admin).ToList();
            var Emplist = new SelectList(emplist, "EmpId", "LName");
            ViewBag.emp = Emplist;

            List<Meeting> Meeting_List = db.Meetings.Where(a => a.UserId == admin).ToList();
            MeetingViewModel meetingViewModel = new MeetingViewModel();
            List<MeetingViewModel> MeetingList = Meeting_List.Select(x => new MeetingViewModel
            {
                MeetingId = x.MeetingId,
                C_Name = x.Company.CmpName,
                D_Name = x.Department.DeptName,
                E_Name = x.Employee.LName,
                E_Image=x.Employee.EmpImage,
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
        public ActionResult AddMeeting()
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
            return RedirectToAction("Meeting", "Company");
        }

        [HttpPost]
        //[ActionName("Meeting")]
        public ActionResult AddMeeting(Meeting meeting)
        {
            meeting.UserId = Convert.ToInt32(Session["Userid"]);

            var Company_Emaill = db.Meetings.Where(a => a.CmpId == meeting.CmpId && a.DeptId == meeting.DeptId && a.UserId == meeting.UserId && a.EmpId==meeting.EmpId && a.Time==meeting.Time && a.Date==meeting.Date).FirstOrDefault();

            if (Company_Emaill != null)
            {
                ViewBag.exit = "Employee Already Exist";
            }
            else
            {
                try
                {

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
                    return RedirectToAction("Meeting", "Company");
                }
                catch
                {
                    return RedirectToAction("Meeting", "Company");
                }
                
            }
            return RedirectToAction("Meeting", "Company");


        }


        [HttpGet]
        public JsonResult EditMeeting(int DesignationiD)
        {
            var list = db.Meetings.Where(x => x.MeetingId == DesignationiD).SingleOrDefault();


            Meeting meeting_list = new Meeting
            {
                MeetingId = list.MeetingId,
                Date = list.Date,
                Time = list.Time,
                CmpId = list.CmpId,
                DeptId = list.DeptId,
                EmpId =list.EmpId,
                Message=list.Message

            };
            return Json(meeting_list, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ActionName("EditMeeting")]
        public ActionResult EditMeeting1(Meeting meeting)
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




            

            return RedirectToAction("Meeting", "Company");
        }




        public JsonResult DeleteMeeting(int Meetingid)
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




        //public JsonResult Delete(int Salaryid)
        //{



        //    bool result = false;
        //    var a = db.Meetings.FirstOrDefault(y => y.MeetingId == Salaryid);
        //        if (a != null)
        //        {
        //            db.Meetings.Remove(a);
        //            db.SaveChanges();
        //    result = true;
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }






        //    return Json(JsonRequestBehavior.AllowGet);



        //}




        //List<Meeting> Meeting_List = db.Meetings.ToList();
        //MeetingViewModel meetingViewModel = new MeetingViewModel();
        //List<MeetingViewModel> MeetingList = Meeting_List.Select(x => new MeetingViewModel
        //{
        //    MeetingId=x.MeetingId,
        //    C_Name=x.Company.CmpName,
        //    D_Name=x.Department.DeptName,
        //    E_Name = x.Employee.LName,
        //    Date=x.Date,
        //    Time=x.Time,
        //    Message=x.Message,
        //    CmpId = x.CmpId,
        //    DeptId = x.DeptId,
        //    EmpId = x.EmpId,
        //    UserId = x.UserId



        //}).ToList();

        //ViewBag.Meeting = MeetingList;

        //    return View();
        //}



    }
}