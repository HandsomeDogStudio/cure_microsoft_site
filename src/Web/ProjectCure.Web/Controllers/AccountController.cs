using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using ProjectCure.Web.Models;
using ProjectCure.Web.Code;
using ProjectCureData;

namespace ProjectCure.Web.Controllers
{
    public class AccountController : ProjectCureControllerBase
    {
        public AccountController(IRepository repository) : base(repository)
        {
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.HideLogin = true;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && Repository.IsValidUser(model.UserName, model.Password))
            {
                var user = Repository.GetUserByUserName(model.UserName);
                if (user != null)
                {
                    var ticket = new FormsAuthenticationTicket(1,
                        model.UserName,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        false,
                        JsonConvert.SerializeObject(user.ToUserSerializable()),
                        FormsAuthentication.FormsCookiePath);

                    HttpCookie faCookie = new HttpCookie(
                        FormsAuthentication.FormsCookieName,
                        FormsAuthentication.Encrypt(ticket));

                    Response.Cookies.Add(faCookie);

                    return RedirectToLocal(returnUrl);
                }
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The email or password provided is incorrect.");
            ViewBag.HideLogin = true;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View(new ChangePasswordModel
            {
                UserName = User.Identity.Name
            });
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid && Repository.IsValidUser(model.UserName, model.OldPassword))
            {
                Repository.UpdatePassword(new ProjectCureData.Models.User
                {
                    UserEmail = model.UserName,
                    UserPassword = model.NewPassword
                });

                model.PasswordChanged = true;

                var notifier = new EmailNotifier();
                notifier.PasswordChangeConfirmationNotification(Repository, model.UserName);

                return View("ChangePasswordSuccess");
            }

            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
