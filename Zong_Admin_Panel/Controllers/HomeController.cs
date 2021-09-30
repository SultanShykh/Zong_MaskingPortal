using Zong_Admin_Panel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zong_Admin_Panel.Interaces;

namespace Zong_Admin_Panel.Controllers
{
   
    public class HomeController : Controller
    {
        // GET: Home
        private IDashboard dashboard;
        public HomeController(IDashboard dashboard)
        {
            this.dashboard = dashboard;
        }
        public ActionResult Index()
        {
            if (Request.Cookies["SMSCookies"] != null)
            {
                int userId;
                LoginModel val;
                val = Cookies.getCookieValue();
                userId = val.UserId;
                var result = dashboard.GetDashboard(userId);
                ViewBag.Message =result;

                return View();
            }
            else if (!string.IsNullOrEmpty(Session["Username"] as string))
            {

                return View();
            }
            return RedirectToAction("Index", "Account");
        }

     
    }
}