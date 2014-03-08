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
        
    }
}