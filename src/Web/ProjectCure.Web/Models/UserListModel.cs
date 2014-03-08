using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectCureData.Models;

namespace ProjectCure.Web.Models
{
    public class UserListModel
    {
        public IEnumerable<User> Users { get; set; }
    }
}