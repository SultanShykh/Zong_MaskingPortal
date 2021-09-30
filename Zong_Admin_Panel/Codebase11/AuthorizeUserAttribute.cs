using Zong_Admin_Panel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Zong_Admin_Panel.Codebase
{

    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        // Custom property
        //public string AccessLevel { get; set; }
        LoginModel val;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            if (HttpContext.Current.Request.Cookies["SMSCookies"] != null)
            {
                val = Cookies.getCookieValue();
                return GetUserRights(val.UserId);
            }

            return false;
             

           
        }

        public bool GetUserRights(int userId)
        {
            string controllerName = "Home";
            string actionName = "Index";
            HttpContextBase context = new HttpContextWrapper(HttpContext.Current);
            RouteData rd = RouteTable.Routes.GetRouteData(context);

            if (rd != null)
            {
                controllerName = rd.GetRequiredString("controller");
                actionName = rd.GetRequiredString("action");
            }
            bool flag = true;
            var listdata = new List<dynamic>();
            List<dynamic> list = new List<dynamic>();
            if (System.Web.HttpContext.Current.Session["Authorize"] != null)
            {
                var strErrore = System.Web.HttpContext.Current.Session["Authorize"];
                list.Add(strErrore);
            }
            else
            {
                CommonProcessing.SetPermissions("", userId.ToString(), out listdata);
                if(listdata.Count() > 0)
                {
                    System.Web.HttpContext.Current.Session["Authorize"] = listdata;
                    list.Add(listdata);
                }
                else
                {
                    return flag = false;
                }
                
            }
            
            foreach (var v in list)
            {
                foreach (var d in v)
                {
                    if (d.Controller == controllerName && d.Action == actionName && d.AllowView == false)
                    {
                        return flag = false;
                    }
                }

            }

            return flag;
        }
    }

    }

   