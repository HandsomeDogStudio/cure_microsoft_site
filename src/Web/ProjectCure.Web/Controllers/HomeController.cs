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
            return View();
        }
        
        [CustomAuthorize]
        public ActionResult Calendar()
        {
            return View();
        }
    }
}
