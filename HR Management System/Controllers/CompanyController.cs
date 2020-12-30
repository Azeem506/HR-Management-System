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

        EazisolsEntities3 db = new EazisolsEntities3();
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

        public ActionResult Company()
        {
            var complist = db.Companies.ToList();
            ViewBag.comp = complist;
            Session["cmplist"] = complist;
            return View();
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
        public ActionResult AddCompany()
        {
            string user = Request["username"];
            string password = Request["password"];
            string mail = Request["mail"];
            string companyName = Request["cname"];
            string phone = Request["phone"];
            string image = Request["Image"];

            var uniqueName = "";
            if (Request.Files["Image"] != null)
            {
                var file = Request.Files["Image"];
                if(file.FileName!="")
                {
                    var ext = System.IO.Path.GetExtension(file.FileName);
                    uniqueName = Guid.NewGuid().ToString() + ext;
                    var rootPath = Server.MapPath("/Content");
                    var fileSavePath = System.IO.Path.Combine(rootPath, uniqueName);
                    var a= System.IO.Path.Combine("\\Content", uniqueName);
                    file.SaveAs(fileSavePath);

                    DBManager.AddCompany(companyName, user, password, mail, phone, a);
                }
            }


            //DBManager.AddCompany(companyName, user, password, mail, phone, image);
            //List<CompanyDTO> cmplist = new List<CompanyDTO>();
            //cmplist = DBManager.ShowCompany();
            //ViewBag.list = cmplist;
            return RedirectToAction("Company","Company");
        }
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
        [HttpPost]
        public ActionResult EditCompany()
        {
            string id = Request["cmpid"];
            ViewBag.id = id;
            string user = Request["UserName"];
            string password = Request["password"];
            string mail = Request["Email"];
            string companyName = Request["CmpName"];
            string phone = Request["Phone"];
            //string image = Request["Image"];

            DBManager.UpdateCompany(id,companyName, user, password, mail, phone);
            //List<CompanyDTO> cmplist = new List<CompanyDTO>();
            //cmplist = DBManager.ShowCompany();
            //ViewBag.list = cmplist;
            
            return RedirectToAction("Company", "Company");
        }


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
                Password=list.Password,
                Image=list.Image
                


            };
            return Json(designation_list, JsonRequestBehavior.AllowGet);/*model*/
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
       
        [HttpGet]
        public JsonResult DelCompany(int DesignationiD)
        {


            var list = db.Companies.Where(x => x.CmpId == DesignationiD).SingleOrDefault();


            Company designation_list = new Company
            {
                CmpId = list.CmpId,
                CmpName = list.CmpName,
                UserName = list.UserName,
                Email = list.Email,
                Phone = list.Phone



            };
            return Json(designation_list, JsonRequestBehavior.AllowGet);/*model*/
        }

        [HttpPost]
        public ActionResult DelCompany()
        {
            string id=Request["cmpid1"];


            DBManager.DeleteCompany(id);
            return RedirectToAction("Company", "Company");
        }


        

        public ActionResult Meeting()
        {
            return View();
        }



        public ActionResult EditMeeting()
        {
            return View();
        }

        public ActionResult AddMeeting()
        {
            return View();
        }

        public ActionResult CompanyProfile(int id)
        {
            //var complist = db.Companies.ToList();
            //ViewBag.comp = complist;
            var detail = db.Companies.Find(id);
            return View(detail);
        }
        
    }
}