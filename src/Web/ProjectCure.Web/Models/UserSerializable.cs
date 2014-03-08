using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectCure.Web.Models
{
    public class UserSerializable
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] Roles { get; set; }
    }
}