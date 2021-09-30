using Zong_Admin_Panel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zong_Admin_Panel.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        // GET: Home
       
        public ActionResult Index()
        {
            if (Request.Cookies["SMSCookies"] != null)
            {
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