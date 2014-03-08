using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using log4net;
using Newtonsoft.Json;
using ProjectCure.Web.Code;
using ProjectCure.Web.Models;

namespace ProjectCure.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
		private static readonly ILog _log = LogManager.GetLogger(typeof(MvcApplication));

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            Bootstrapper.Initialise();

			log4net.Config.XmlConfigurator.Configure();
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                var user = JsonConvert.DeserializeObject<UserSerializable>(authTicket.UserData);

                var principal = new CustomPrincipal(authTicket.Name)
                {
                    User = user
                };

                HttpContext.Current.User = principal;
                Thread.CurrentPrincipal = principal;
            }
        }

		protected void Application_Error()
		{
			if (HttpContext.Current.AllErrors != null)
			{

				foreach (var exception in HttpContext.Current.AllErrors)
				{
					_log.Info(HttpContext.Current.Request.ServerVariables["LOGON_USER"] + "," +
							  exception.Message + "," +
							  exception.StackTrace);
				}
			}
		}
    }
}