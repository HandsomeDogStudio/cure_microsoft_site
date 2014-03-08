using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectCure.Web.Code;
using ProjectCure.Web.Models;
using ProjectCureData;

namespace ProjectCure.Web.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class UserController : ProjectCureControllerBase
    {
        public UserController(IRepository repository) : base(repository)
        {
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            return View(
                new UserListModel
                {
                    Users = Repository.GetUserList()
                });
        }
    }
}
