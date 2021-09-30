using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simple.Data;
using Zong_Admin_Panel.Codebase;
using Zong_Admin_Panel.Models;
using Microsoft.Ajax.Utilities;

namespace Zong_Admin_Panel.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        HttpCookie cookies = new HttpCookie("SMSCookies");

        AccountProcessing account = new AccountProcessing();
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
            return RedirectToAction("Index", "Database");
        }

        //POST: Login View 
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection collection)
        {
            try
            {
                var user = account.UserLogin(collection["username"], collection["password"]);
                string message = string.Empty;
                //var remember = collection["rememberme"];
                LoginModel lg = new LoginModel();
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
                            // lg.Approver = user.FirstOrDefault().Approver == null ? "" : user.FirstOrDefault().Approver.ToString();
                            lg.UserGroup = user.FirstOrDefault().GroupName;
                            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                            var json = serializer.Serialize(lg);
                            HttpCookie cook = new HttpCookie("SMSCookies", json);
                            cook.Expires = DateTime.Now.AddHours(12);
                            Response.SetCookie(cook);
                            Session["UserId"] = (int)user.FirstOrDefault().id;
                            Session["Username"] = user.FirstOrDefault().username.ToString();
                            Session["UserType"] = user.FirstOrDefault().userType.ToString();
                            user.NextResult();
                            var list = new List<dynamic>();
                            CommonProcessing.SetPermissions(user, null, out list);
                            Session["Authorize"] = list;
                            var jsonList = serializer.Serialize(list);
                            Session["PermissionCookies"] = jsonList;
                            HttpCookie permissionCookie = new HttpCookie("Permissions", jsonList);
                            permissionCookie.Expires = DateTime.Now.AddHours(12);
                            Response.SetCookie(permissionCookie);

                            return RedirectToAction("Index", "Group");

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

        public ActionResult ResetPassword(string username)
        {
            Session["ChangeUserName"] = username;
            ViewBag.form = "ResetPassword";
            return RedirectToAction("Index", "Account", new { form = "ResetPassword" });
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