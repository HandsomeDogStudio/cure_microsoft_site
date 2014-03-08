using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjectCure.Web.Code
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected virtual CustomPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as CustomPrincipal; }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Request.IsAuthenticated)
            {
                if (string.IsNullOrEmpty(Roles) || CurrentUser.IsInRole(Roles))
                {
                    return true;
                    // base.OnAuthorization(filterContext); //returns to login url
                }
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                filterContext.Result = new ContentResult();
                //base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                var route = filterContext.HttpContext.Request.IsAuthenticated
                    ? new RouteValueDictionary
                        {
                            {"action", "Index"},
                            {"controller", "Home"}
                        }
                        : new RouteValueDictionary
                        {
                            {"action", "Login"},
                            {"controller", "Account"}
                        };
                
                    filterContext.Result = new RedirectToRouteResult(route);
            }
        }
        
    }
}