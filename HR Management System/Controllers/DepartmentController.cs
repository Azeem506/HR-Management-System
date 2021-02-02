using HR_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace HR_Management_System.Controllers
{
    public class DepartmentController : Controller
    {

        EazisolsEntities4 db = new EazisolsEntities4();
        // GET: Department
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Department()
        {

            int admin = Convert.ToInt32(Session["Userid"]);
            var deptlist = db.Departments.ToList();
            var Deplist = new SelectList(deptlist, "DeptId", "DeptName");
            ViewBag.dept = deptlist;
            var cmplist = db.Companies.Where(a => a.UserId == admin).ToList();
            var Cmplist = new SelectList(cmplist, "CmpId", "CmpName");
            ViewBag.cmp = Cmplist;
           
            
            List<Department> Department_List = db.Departments.Where(a => a.UserId == admin).ToList();
            DepartmentViewModel Department_View_Model = new DepartmentViewModel();
            List<DepartmentViewModel> Department_View_List = Department_List.Select(x => new DepartmentViewModel

            {

                DeptId = x.DeptId,
                DeptName = x.DeptName,
                CmpName = x.Company.CmpName


            }).ToList();

            ViewBag.deptviewlist = Department_View_List;





            return View();

        }

        [HttpPost]
        [ActionName("Department")]
        public ActionResult Department(Department dep)
        {
            dep.UserId = Convert.ToInt32(Session["Userid"]);
            var Company_Emaill = db.Departments.Where(a => a.CmpId == dep.CmpId && a.DeptName == dep.DeptName && a.UserId==dep.UserId).FirstOrDefault();

            if (Company_Emaill != null)
            {
                ViewBag.exit = "Department Already Exist";
            }
            else
            {
                
                    //if (Session["CompanyId"] != null)
                    //{

                    //    db.Departments.Add(dep);
                    //    dep.CmpId = Convert.ToInt32(Session["CompanyId"]);
                    //    dep.UserId = 0;
                    //    db.SaveChanges();

                    //    return RedirectToAction("Company_Departments", "Company");


                    //}
                    if (Session["Userid"] != null)
                    {

                        db.Departments.Add(dep);
                        
                        db.SaveChanges();

                    }
                    return RedirectToAction("Department", "Department");
 
            }
            return RedirectToAction("Department", "Department");
        }

        //    try
        //    {
        //        department.CmpId = Convert.ToInt32(Session["CompanyId"]);
        //        department.UserId = null;
        //        db.Departments.Add(department);

        //        int a = db.SaveChanges();
        //        if (a > 0)
        //        {
        //            ViewBag.insert = "<script>alert('Data Inserted!!')</script>";
        //            ModelState.Clear();
        //        }
        //        else
        //        {
        //            ViewBag.insert = "<script>alert('Data not Inserted!!')</script>";
        //        }
        //        return RedirectToAction("Department", "Department");
        //    }
        //    catch
        //    {
        //        return RedirectToAction("Department", "Department");
        //    }





        //public ActionResult AddDepartment()
        //{




        //string user = Request["username"];
        //string password = Request["password"];
        //string mail = Request["email"];
        //string departmentName = Request["dname"];
        //string phone = Request["phone"];
        //string empcomp = Request["empcompany"];


        //DBManager.AddDepartment(user,password,mail, phone, departmentName, empcomp);

        //    return RedirectToAction("Department", "Department");
        //}
        //public ActionResult EditDepartment()
        //{
        //    return View();

        //}
        [HttpPost]
        public ActionResult EditDepartment(Department dept)
        {
            db.Entry(dept).State = EntityState.Modified;
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



            //string id = Request["did1"];
            //ViewBag.id = id;
            //string user = Request["usernameedit"];
            //string password = Request["password1"];
            //string mail = Request["email1"];
            //string companyName = Request["dname1"];
            //string phone = Request["phone1"];


            //DBManager.UpdateDepartment(id, user, password, mail, phone, companyName);
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
                CmpId = list.CmpId



            };
            return Json(designation_list, JsonRequestBehavior.AllowGet);/*model*/
        }



        public JsonResult DeleteDepartment(int Departmentid)
        {



            bool result = false;
            var a = db.Departments.FirstOrDefault(y => y.DeptId == Departmentid);
            if (a != null)
            {
                db.Departments.Remove(a);
                db.SaveChanges();
                result = true;

            }

            return Json(result, JsonRequestBehavior.AllowGet);


        }



        //[HttpGet]
        //public JsonResult DelDepartment(int DesignationiD)
        //{


        //    var list = db.Departments.Where(x => x.DeptId == DesignationiD).SingleOrDefault();


        //    Department designation_list = new Department
        //    {
        //        DeptId = list.DeptId,
        //        DeptName = list.DeptName,
        //        UserName = list.UserName,
        //        Email = list.Email,
        //        Phone = list.Phone



        //    };
        //    return Json(designation_list, JsonRequestBehavior.AllowGet);/*model*/
        //}

        //[HttpPost]
        //public ActionResult DelDepartment()
        //{
        //    string id = Request["delete1"];


        //    DBManager.DeleteDepartment(id);
        //    return RedirectToAction("Department", "Department");
        //}
    }
}