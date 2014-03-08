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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var u = Repository.GetUserById(id);

            return PartialView(
                new EditUserModel
                {
                    UserId = u.UserId,
                    FirstName = u.UserFirstName,
                    LastName = u.UserLastName,
                    UserName = u.UserEmail,
                    RoleId = u.UserRoleId,
                    Notify5Days = u.UserNotifyFiveDays,
                    Notify10Days = u.UserNotifyTenDays,
                    IsActive = u.UserActiveIn,
                    Roles = Repository.GetRoleList(),
                });
        }

        [HttpPost]
        public ActionResult Edit(EditUserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ProjectCureData.Models.User
                {
                    UserId = model.UserId,
                    UserEmail = model.UserName,
                    UserFirstName = model.FirstName,
                    UserLastName = model.LastName,
                    UserRoleId = model.RoleId,
                    UserActiveIn = model.IsActive,
                    UserNotifyFiveDays = model.Notify5Days,
                    UserNotifyTenDays = model.Notify10Days,
                };

                //TODO: if inactivating, cancel associated future events


                Repository.SaveUser(user);
            }

            model.Roles = Repository.GetRoleList();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult ResetPassword(int id)
        {
            var success = false;

            var user = Repository.GetUserById(id);
            if (user != null)
            {
                var passwordGenerator = new PasswordGenerator();
                var tempPassword = passwordGenerator.GeneratePassword(10);

                user.UserPassword = tempPassword;

                Repository.UpdatePassword(user);

                success = true;

                var notifier = new EmailNotifier();
//                notifier.GiveTemporaryPassword(Repository, user.UserEmail, tempPassword);

                return Json(new
                {
                    UserId = user.UserId,
                    UserName = user.UserFirstName + " " + user.UserLastName,
                    UserEmail = user.UserEmail,
                    success,
                });
            }

            return Json(new { success });
        }
    }
}
