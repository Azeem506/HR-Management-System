using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using HR_Management_System.Models;

namespace HR_Management_System.Controllers
{
    public class PagesController : Controller
    {
        // GET: Pages
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult HomePage()
        {


            return View();

        }
        
        

        [ActionName("Login")]
        [HttpPost]
        public ActionResult Login(UserDTO user)
        {
            string login = Request["txtLogin"];
            string password = Request["pswd"];
            bool a = DBManager.Validate(login, password);
            Session["login"] = login;

            if (a == true)
            {
                ViewBag.alert = "Valid User";
                return Redirect("~/Home/Index");

            }

            ViewBag.alert = "InValid User";

            return View();



        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [ActionName("SignUp")]
        [HttpPost]
        public ActionResult SignUp2()
        {

            String login = Request["txtLogin"];
            String first = Request["fname"];
            String last = Request["lname"];
            String mail = Request["txtmail"];
            String password = Request["pswd"];
            String confirm = Request["cpswd"];
            if (password == confirm)
            {
                DBManager.AddUser(login, password, mail, first, last, confirm);
            }
            else
            {
                ViewBag.invalidpass = "Password is not correct";
            }

            return RedirectToAction("Login");

        }


        public ActionResult Logout()
        {

            Session["login"] = null;
            Session.Abandon();

            return Redirect("~/Pages/Login");
        }

        public ActionResult ForgetPass()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ForgetPass(string email)
        {
            bool m = DBManager.ValidateEmail(email);
            if (m == true)
            {

                if (SendEmail(email) == true)
                {
                    ViewBag.message = "Email send Successfully";
                }
            }
            return View();
        }



        public bool SendEmail(string email)
        {
            try
            {

                //var sender = "restock06@gmail.com";
                //var Password = "Developer@123";
                //var senderEmail = new MailAddress(sender, Password);
                //var receiverEmail = new MailAddress(email, "Receiver");
                //var password = "Your Email Password here";
                //var sub = "Change password";
                //var body = "Your Link";
                //var smtp = new SmtpClient
                //{
                //    Host = "smtp.gmail.com",
                //    Port = 587,
                //    EnableSsl = true,
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    UseDefaultCredentials = false,
                //    Credentials = new NetworkCredential(senderEmail.Address, password)
                //};
                //using (var mess = new MailMessage(senderEmail, receiverEmail)
                //{
                //    Subject = "Change password",
                //    Body = body
                //})
                //{
                //    smtp.Send(mess);
                //}

                using (MailMessage mm = new MailMessage("restock06@gmail.com", email))
                {
                    string link = Request.Url.ToString();
                    link = link.Replace("ForgetPass", "ChangePassword");
                    mm.Subject = "Password reset";
                    string body = "Hello ";
                    body += "<br /><br />Please click the following link to reset your password";
                    body += "<br /><a href = '" + link + "?'>Click here to activate your account.</a>";
                    body += "<br /><br />Thanks";
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("restock06@gmail.com", "Developer@123");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                }
                return true;


            }

            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }

            return false;
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(UserDTO u)
        {
            string mail = Request["email"];
            string newpass = Request["newPass"];
            string cnfrmpass = Request["cnfrmPass"];
            DBManager.UpdatePass(mail, newpass, cnfrmpass);
            //ViewBag.success = "Password Change Successfuly";
            return View();
        }


    }


}