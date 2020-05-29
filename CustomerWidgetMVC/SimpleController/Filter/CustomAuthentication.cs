using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Routing;

namespace SimpleController.Filter
{
    public class customeFilter : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            Controller controller = filterContext.Controller as Controller;

            if (controller != null)
            {
                if (session["Email"] == null)
                {
                    filterContext.Result =
                           new RedirectToRouteResult(
                               new RouteValueDictionary{{ "controller", "Customer" },
                                          { "action", "Register" }

                                                             });
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}