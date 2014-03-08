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
        public ActionResult Add()
        {
            return PartialView("Edit",
                new EditUserModel
                {
                    IsNew = true,
                    IsActive = true,
                    Roles = Repository.GetRoleList(),
                });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var u = Repository.GetUserById(id);

            return PartialView("Edit",
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

                if (!model.IsNew && !user.UserActiveIn)
                {
                    var oldUser = Repository.GetUserById(user.UserId);
                    if (oldUser != null && oldUser.UserActiveIn)
                    {
                        //remove manager from future events if being inactivated
                        Repository.RemoveManagerFromEvents(user.UserId);

                        //TODO: send notifications?
                    }
                }

                Repository.SaveUser(user);
            }

            model.Roles = Repository.GetRoleList();

            return PartialView("Edit", model);
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
                notifier.GiveTemporaryPasswordNotification(Repository, user.UserEmail, tempPassword);

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
