using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectCure.Web.Code;
using ProjectCureData;

namespace ProjectCure.Web.Controllers
{
    public class HomeController : ProjectCureControllerBase
    {
	    public HomeController(IRepository repository) : base(repository)
		{
		}

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Calendar()
        {
            return View();
        }
    }
}
