using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectCure.Web.Models
{
    public static class Extensions
    {
        public static UserSerializable ToUserSerializable(this ProjectCureData.Models.User u)
        {
            return u != null
                ? new UserSerializable
                {
                    UserId = u.UserId,
                    UserName = u.UserEmail,
                    LastName = u.UserLastName,
                    FirstName = u.UserFirstName,
                    Roles = u.Role != null ? new[] { u.Role.RoleName } : new string[] { }
                }
                : null;
        }

        public static string FirstEventText(this ProjectCureData.Models.User u)
        {
            if (u != null)
            {
                var firstEvent = u.Events.FirstOrDefault();
                if (firstEvent != null)
                {
                    return string.Format("{0} - {1:M} {1:t}", firstEvent.EventTitle, firstEvent.EventStartDateTime);
                }
            }
            return "";
            return u != null
                ? string.Format("", u.Events.FirstOrDefault())
                : "";
        }
    }
}