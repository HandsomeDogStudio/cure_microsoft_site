using ProjectCure.Web.Code;
using ProjectCure.Web.Models;
using ProjectCureData;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace ProjectCure.Web.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class UserController : ProjectCureControllerBase
    {
        private static readonly int UnfilledNotificationEmailDays = Convert.ToInt32(ConfigurationManager.AppSettings["UnfilledNotificationEmailDays"]);

        public UserController(IRepository repository) : base(repository)
        {
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            return View(new UserListModel
                {
                    Users = Repository.GetUserList()
                });
        }

        public ActionResult ListPartial()
        {
            return PartialView(
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
                    FutureEvents = u.Events.Where(e => e.EventStartDateTime > DateTime.Now).ToList(),
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

                var existingUser = Repository.GetUserById(user.UserId);

                Repository.SaveUser(user);

                var notifier = new EmailNotifier();
                
                if (model.IsNew)
                {
                    //set password for new user and notify via email
                    var newPassword = GetNewPassword();
                    user.UserPassword = newPassword;
                    Repository.UpdatePassword(user);

                    notifier.GiveTemporaryPasswordNotification(Repository, user.UserEmail, newPassword);
                }
                else if (!model.IsNew && !user.UserActiveIn)
                {
                    if (existingUser != null && existingUser.UserActiveIn)
                    {
                        //unassign from events, and send notifications
                    
                        //remove manager from future events if being inactivated
                        var unassociatedEvents = Repository.RemoveManagerFromEvents(user.UserId);

                        foreach (var evt in unassociatedEvents)
                        {
                            notifier.EventCancellationNotification(Repository, evt, user.UserEmail);
                        }
                    }
                }
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
                var newPassword = GetNewPassword();
                user.UserPassword = newPassword;
                Repository.UpdatePassword(user);

                success = true;

                var notifier = new EmailNotifier();
                notifier.GiveTemporaryPasswordNotification(Repository, user.UserEmail, newPassword);

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

        [HttpPost]
        public ActionResult UnfilledEventsNotification()
        {
            try
            {
                var events = Repository.GetEventsBetweenDates(DateTime.Today, DateTime.Today.AddDays(UserController.UnfilledNotificationEmailDays))
                                       .Where(c => c.EventManagerId == null)
                                       .OrderBy(c => c.EventStartDateTime)
                                       .ToList();

                var notifier = new EmailNotifier();
                notifier.UnfilledEventsNotification(Repository, events);

                return Json(new
                {
                    success = true,
                    message = string.Format("{0} notification emails went out.", events.Count)
                });
            }
            catch (Exception ex) 
            {
                return Json(new
                {
                    success = false,
                    message = ex.GetBaseException().Message
                });
            }

        }


        private string GetNewPassword(int length = 10)
        {
            var passwordGenerator = new PasswordGenerator();
            var tempPassword = passwordGenerator.GeneratePassword(length);
            return tempPassword;
        }
    }
}
