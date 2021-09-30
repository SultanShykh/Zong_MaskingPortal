using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zong_Admin_Panel.Codebase
{
    public static class UrlHelperExtensions
    {
        public static string IsLinkActive(this UrlHelper url, string actionName, string controllerName)
        {
             return  url.RequestContext.RouteData.Values["controller"].ToString() == controllerName && url.RequestContext.RouteData.Values["action"].ToString() == actionName ? "active" : "";
           
        }  
        //public static string IsTreeviewActive(this UrlHelper url, Dictionary<string, string> actions)
        //{
        //    string controller = url.RequestContext.RouteData.Values["controller"].ToString();
        //    string action = url.RequestContext.RouteData.Values["action"].ToString();
        //    return actions.Any(a =>
        //    {
        //        if (a.Key == action)
        //            return a.Value == controller;
        //        return false;
        //    }) ? "active" : "";
        //}

        public static string IsTreeviewActive(this UrlHelper url, string controllerName)
        {
            string controller = url.RequestContext.RouteData.Values["controller"].ToString();
            return controllerName == controller ? "active" : "";
        }
    }
}