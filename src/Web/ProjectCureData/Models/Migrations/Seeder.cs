using System.Data.Entity.Migrations;
using System;

namespace ProjectCureData.Models.Migrations
{
    /// <summary>
    /// Static class that seeds the database on a migration
    /// </summary>
    internal static class Seeder
    {
        /// <summary>
        /// Seeds data into the database
        /// </summary>
        /// <param name="context"></param>
        public static void Seed(ProjectCureContext context)
        {
            //DbSets
            var events = context.Set<Event>();
            var templates = context.Set<Template>();
            var users = context.Set<User>();
            var roles = context.Set<Role>();

            //Templates
            templates.AddOrUpdate(t => t.TemplateName,
                new Template() { TemplateId = 1, TemplateName = "Password Reset Email", TemplateSubject = "Project C.U.R.E - Password reset confirmation", TemplateText = "Dear {name},\r\n\r\nYour account password at Project C.U.R.E has been reset and you have been issued with a new temporary password {temp password}.\r\nPlease go to this page and change your password:\r\nhttp://projectcure.azurewebsites.net/\r\n\r\nThanks,\r\nThe Project C.U.R.E. Team" },
                new Template() { TemplateId = 2, TemplateName = "Confirmation Email", TemplateSubject = "Project C.U.R.E - Registration Confirmation", TemplateText = "Dear {name},\r\n\r\nThank you for signing up to lead {title} on {date} from {start time} to {end time}.  We appreciate your service to our organization.\r\n\r\nThanks,\r\nThe Project C.U.R.E. Team" },
                new Template() { TemplateId = 3, TemplateName = "Cancellation Email", TemplateSubject = "Project C.U.R.E - Cancellation", TemplateText = "Dear {name},\r\n\r\nYou have successfully canceled your reservation to lead the event for {title} on {date} from {start time} to {end time}.  Please sign up again soon!\r\n\r\nThanks,\r\nThe Project C.U.R.E. Team" },
                new Template() { TemplateId = 4, TemplateName = "Unfilled Group Lead Email", TemplateSubject = "Project C.U.R.E - Unfilled Group Lead", TemplateText = "Dear Sort Team Leaders,\r\n\r\nThe groups below are the current groups who are in need of a Sort Team Leader. If you are able to lead a group, please go to the calendar using the link and then click on the \"I will lead\" button.  We appreciate your service to our organization.\r\n\r\n{events}\r\n\r\nhttp://projectcure.azurewebsites.net/Home/Calendar\r\n\r\nThanks,\r\nThe Project C.U.R.E. Team" },
                new Template() { TemplateId = 5, TemplateName = "Reminder Email", TemplateSubject = "Project C.U.R.E - Reminder", TemplateText = "Dear {name},\r\n\r\nThis is a reminder that you are scheduled to lead {title} on {date} from {start time} to {end time}.  Thank you for your service to our organization.\r\n\r\nThanks,\r\nThe Project C.U.R.E. Team" },
                new Template() { TemplateId = 6, TemplateName = "Password Change Confirmation Email", TemplateSubject = "Project C.U.R.E - Password change confirmation", TemplateText = "Dear {name},\r\n\r\nThank you for visiting the Project C.U.R.E website. As per your request, we have successfully changed your password.\r\n\r\nThanks,\r\nThe Project C.U.R.E. Team" }
            );

            //Roles
            roles.AddOrUpdate(c => c.RoleId,
                new Role() { RoleId = 1, RoleName = "Admin" },
                new Role() { RoleId = 2, RoleName = "Manager" }
            );

            //Users
            users.AddOrUpdate(u => u.UserId,
                new User() { UserId = 1, UserEmail = "louisfischer@gmail.com", UserFirstName = "Louis", UserLastName = "Fischer", UserRoleId = 1, UserActiveIn = true, UserNotifyFiveDays = true, UserNotifyTenDays = true, UserPassword = "8de3964d8a9f08e323ccd7185ddad9bfc706cf31822b146f7672b5c6bfdeed51" },
                new User() { UserId = 2, UserEmail = "brian.avent@gmail.com", UserFirstName = "Brian", UserLastName = "Avent", UserRoleId = 1, UserActiveIn = true, UserNotifyFiveDays = false, UserNotifyTenDays = false, UserPassword = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08" },
                new User() { UserId = 3, UserEmail = "jpresa_2000@hotmail.com", UserFirstName = "Jose", UserLastName = "Presa", UserRoleId = 1, UserActiveIn = true, UserNotifyFiveDays = true, UserNotifyTenDays = true, UserPassword = null },
                new User() { UserId = 4, UserEmail = "annieellement@projectcure.org", UserFirstName = "Annie", UserLastName = "Ellement", UserRoleId = 1, UserActiveIn = true, UserNotifyFiveDays = false, UserNotifyTenDays = false, UserPassword = "6d08fed825a74ce267e94d9569a8df1c798a4df1927a1d2f2f285986d688c357" },
                new User() { UserId = 5, UserEmail = "lindseymoore@projectcure.org", UserFirstName = "Lindsey", UserLastName = "Moore", UserRoleId = 1, UserActiveIn = true, UserNotifyFiveDays = true, UserNotifyTenDays = true, UserPassword = "d95c3086066012ccd19bc644765a6692086c227a3c356e58ea4c586caa501d6f" },
                new User() { UserId = 6, UserEmail = "annellement@gmail.com", UserFirstName = "Annie", UserLastName = "Ellement", UserRoleId = 2, UserActiveIn = true, UserNotifyFiveDays = false, UserNotifyTenDays = false, UserPassword = "9be5aeec150a752a0de87ab04c13e1e238268872cdacb02bf74ae94a9086e651" },
                new User() { UserId = 6, UserEmail = "m.ellement24@gmail.com", UserFirstName = "Matthew", UserLastName = "Ellement", UserRoleId = 1, UserActiveIn = true, UserNotifyFiveDays = false, UserNotifyTenDays = false, UserPassword = null }
            );

            //Events
            events.AddOrUpdate(p => p.EventId,
                new Event() { EventId = 1, EventDescription = "", EventEndDateTime = DateTime.Parse("2014-03-15 12:00:00.0000000"), EventTitle = "West Franklin Baptist", EventStartDateTime = DateTime.Parse("2014-03-15 09:00:00.0000000"), EventStatus = false, EventManagerId = null },
                new Event() { EventId = 2, EventDescription = "", EventEndDateTime = DateTime.Parse("2014-03-28 12:00:00.0000000"), EventTitle = "Beta Sigma Phi", EventStartDateTime = DateTime.Parse("2014-03-28 09:00:00.0000000"), EventStatus = false, EventManagerId = null },
                new Event() { EventId = 3, EventDescription = "", EventEndDateTime = DateTime.Parse("2014-04-09 16:00:00.0000000"), EventTitle = "Lipscomb Nursing", EventStartDateTime = DateTime.Parse("2014-04-09 13:00:00.0000000"), EventStatus = false, EventManagerId = null },
                new Event() { EventId = 4, EventDescription = "", EventEndDateTime = DateTime.Parse("2014-04-12 12:00:00.0000000"), EventTitle = "Hermitage UMC", EventStartDateTime = DateTime.Parse("2014-04-12 09:00:00.0000000"), EventStatus = false, EventManagerId = null },
                new Event() { EventId = 5, EventDescription = "", EventEndDateTime = DateTime.Parse("2014-04-19 12:00:00.0000000"), EventTitle = "Mount Nebo Church", EventStartDateTime = DateTime.Parse("2014-04-19 09:00:00.0000000"), EventStatus = false, EventManagerId = null },
                new Event() { EventId = 6, EventDescription = "", EventEndDateTime = DateTime.Parse("2014-04-10 12:00:00.0000000"), EventTitle = "Westminster Ladies", EventStartDateTime = DateTime.Parse("2014-04-10 09:00:00.0000000"), EventStatus = false, EventManagerId = null },
                new Event() { EventId = 7, EventDescription = "", EventEndDateTime = DateTime.Parse("2014-04-23 19:30:00.0000000"), EventTitle = "Hands on Nashville", EventStartDateTime = DateTime.Parse("2014-04-23 17:30:00.0000000"), EventStatus = false, EventManagerId = null }
            );
        }
    }
}
