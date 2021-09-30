
using System.Web.Mvc;
using System.Web.Routing;

namespace ITSSMSInventory.Codebase
{
    public class SessionValidatorActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                if ( filterContext.HttpContext.Session["UserId"] == null
                    || filterContext.HttpContext.Session["Username"] == null)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary { { "Controller", "Account" }, { "Action", "Login" } });
                }
            }
        }
    }

}