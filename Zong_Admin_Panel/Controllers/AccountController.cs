using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simple.Data;
using Zong_Admin_Panel.Codebase;
using Zong_Admin_Panel.Models;
using Microsoft.Ajax.Utilities;
using System.Net;
using System.Net.Mail;

namespace Zong_Admin_Panel.Controllers
{
   [AllowAnonymous]
    public class AccountController : Controller
    {
        dynamic AppDB = Database.OpenNamedConnection("MainDB");
        HttpCookie cookies = new HttpCookie("SMSCookies");
        LoginModel lg = new LoginModel();
        AccountProcessing account = new AccountProcessing();
        string message = string.Empty;
        DateTime otpexpire = DateTime.Now.AddSeconds(90);
         // GET: Account
        public ActionResult Index(string form = null)
          {
            if (form == null)
            {
                ViewBag.form = "login";
            }
            else
            {
                ViewBag.message = TempData["message"];
                ViewBag.form = form;
            }
             if (Request.Cookies["SMSCookies"] == null)
            {
                return View();
            }
            else if (!string.IsNullOrEmpty(Session["Username"] as string))
            {
                 return View();
            }
            return RedirectToAction("Index", "Sender");
        }

        //POST: Login View 
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection collection)
        {
            try
            {
                var user = account.UserLogin(collection["username"], collection["password"]);

                //var remember = collection["rememberme"];

                switch (user.FirstOrDefault().Response)
                {
                    case -1:
                        message = "Username and/or password is incorrect.";
                        break;
                    case -2:
                        message = "Account has not been activated.";
                        break;
                    case -3:
                        
                        message = "Account has been expired.";
                         break;
                         
                    default:
                        {

                            lg.Username = user.FirstOrDefault().username.ToString();
                            lg.UserType = user.FirstOrDefault().userType.ToString();
                            lg.UserId = (int)user.FirstOrDefault().id;
                            lg.RegionId = (int)user.FirstOrDefault().RegionId;
                            lg.email = user.FirstOrDefault().Email.ToString();
                            lg.Counter = 90;
                            //lg.Approver = user.FirstOrDefault().Approver == null ? "" : user.FirstOrDefault().Approver.ToString();
                            lg.UserGroup = user.FirstOrDefault().GroupName;
                            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                            var json = serializer.Serialize(lg);
                            HttpCookie cook = new HttpCookie("SMSCookies", json);
                            cook.Expires = DateTime.Now.AddHours(12);
                            Response.SetCookie(cook);

                            Session["UserId"] = (int)user.FirstOrDefault().id;
                            Session["Username"] = user.FirstOrDefault().username.ToString();
                            Session["UserType"] = user.FirstOrDefault().userType.ToString();
                            Session["email"] = user.FirstOrDefault().Email.ToString();
                            user.NextResult();

                            var list = new List<Permissions>();
                            CommonProcessing.SetPermissions(user, null, out list);
                            Session["Authorize"] = list;
                            var jsonList = serializer.Serialize(list);
                            Session["PermissionCookies"] = jsonList;
                            HttpCookie permissionCookie = new HttpCookie("Permissions", jsonList);
                            permissionCookie.Expires = DateTime.Now.AddHours(12);
                            Response.SetCookie(permissionCookie);

                            string usertypess = Session["UserType"].ToString();
                            account.SendOTP(lg.email, lg.UserId.ToString());

                            return RedirectToAction("OTP", "Account");
                         
                        }
                }
                ViewBag.form = "login";
                ViewBag.Message = message;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }



        //Logout
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Response.Cookies["Permissions"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["SMSCookies"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Index", "Account");
        }

        public string snyc()
        {
            try
            {
                Session["Authorize"] = Session["Authorize"];
            }
            catch
            {
                return "Deactive";
            }
            return "Active";
        }
        public ActionResult ForgetPassword()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ForgetPassword(FormCollection collection)
        {
            string host = Request.Url.GetLeftPart(UriPartial.Authority);
            var checkEmail = account.CheckEmail(collection["email"], host);
            TempData["message"] = checkEmail;
            return RedirectToAction("Index", "Account", new { form = "ForgetPassword" });

        }

        public ActionResult OTP()
        {
             return View();
        }
        [HttpPost]
        public ActionResult OTP(FormCollection form)
        {
          
           var otpcheck = form["otp"].ToString();
           var checkEmail = account.OTPCheck(otpcheck);
            if (checkEmail.FirstOrDefault() != null)
            {
                var otp = checkEmail.FirstOrDefault().OTP;
                //var otptime = checkEmail.FirstOrDefault().OTP_Time;
                //TimeSpan time = DateTime.Now - otptime;

                if (otp == otpcheck)
                {
                    //if (time.TotalSeconds <= 90)
                    //{
                        //lg.Username = checkEmail.FirstOrDefault().Username.ToString();
                        //lg.UserType = checkEmail.FirstOrDefault().userType.ToString();
                        //lg.UserId = (int)checkEmail.FirstOrDefault().id;
                        //lg.RegionId = checkEmail.FirstOrDefault().RegionId.ToString();
                        //lg.email = checkEmail.FirstOrDefault().Email.ToString();
                        ////lg.Approver = user.FirstOrDefault().Approver == null ? "" : user.FirstOrDefault().Approver.ToString();
                        //lg.UserGroup = checkEmail.FirstOrDefault().userType.ToString();

                        //var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                        //var json = serializer.Serialize(lg);
                        //HttpCookie cook = new HttpCookie("SMSCookies", json);
                        //cook.Expires = DateTime.Now.AddHours(12);
                        //Response.SetCookie(cook);

                        //Session["UserId"] = (int)checkEmail.FirstOrDefault().id;
                        //Session["Username"] = checkEmail.FirstOrDefault().username.ToString();
                        //Session["UserType"] = checkEmail.FirstOrDefault().userType.ToString();
                        //Session["email"] = checkEmail.FirstOrDefault().Email.ToString();
                        //checkEmail.NextResult();

                        //var list = new List<Permissions>();
                        //CommonProcessing.SetPermissions(checkEmail, null, out list);
                        //Session["Authorize"] = list;
                        //var jsonList = serializer.Serialize(list);
                        //Session["PermissionCookies"] = jsonList;
                        //HttpCookie permissionCookie = new HttpCookie("Permissions", jsonList);
                        //permissionCookie.Expires = DateTime.Now.AddHours(12);
                        //Response.SetCookie(permissionCookie);
                        return RedirectToAction("Index", "Home");

                    //}
                    //else
                    //{
                    //    message = "OTP Code Exprie Please Resend OTP";
                    //}
                }
                else
                {
                    message = "Please Enter Correct OTP";
                }
            }
            else
            {
                message = " OTP is Null Please Check Email and Write a OTP Code ";
            }
           
            ViewBag.Message = message;
            return View();
        }

            public ActionResult ResetPassword(string username)
        {
            Session["ChangeUserName"] = username;
            ViewBag.form = "ResetPassword";
            return RedirectToAction("Index", "Account", new { form = "ResetPassword" });
        }
        public ActionResult ResendOTP()
        {
            Random random = new Random();
            var useremail = Session["email"].ToString();
            var randomnumber = random.Next(0, 1000);
            string otpnumber = "ITS-" + randomnumber.ToString();
            AppDB.OTPUpdate(OTPs: otpnumber, otptime: DateTime.Now.AddSeconds(90), Id: Session["UserId"].ToString());
            NetworkCredential cred = new NetworkCredential("dummytest387@gmail.com", "Standard10");
            MailMessage msgs = new MailMessage();
            msgs.To.Add(Session["email"].ToString());
            msgs.Subject = "Zong OTP Alert !!!";
            msgs.Body = " Please Enter this OTP Code " + otpnumber.ToString() + " to access the masking portal ";
            msgs.From = new MailAddress("dummytest387@gmail.com","ZongPortal !!!"); // Your Email Id
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = cred;
            client.EnableSsl = true;
            client.Send(msgs);
            return RedirectToAction("OTP","Account");
        }


        [HttpPost]
        public ActionResult ResetPassword(FormCollection collection)
        {
            string password = collection["Password"];
            string confirmpassword = collection["confirmpassword"];
            if (password.Equals(confirmpassword))
            {
                var changepassword = account.ChangePassword(collection["Password"], collection["username"]);
                TempData["message"] = changepassword;
                return RedirectToAction("Index", "Account", new { form = "ResetPassword" });
            }
            else
            {
                TempData["message"] = "Password and confirm password are not same";
                return RedirectToAction("Index", "Account", new { form = "ResetPassword" });
            }
           
  }
    }
}