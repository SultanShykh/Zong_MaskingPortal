using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zong_Admin_Panel.Models
{
    public static class Cookies
    {
        public static LoginModel getCookieValue()
        {
            var json = HttpContext.Current.Request.Cookies["SMSCookies"].Value;
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var result = serializer.Deserialize<LoginModel>(json);
            return result;
        }
    }
}