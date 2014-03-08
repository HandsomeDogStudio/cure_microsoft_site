using System;
using System.Collections.Generic;

namespace ProjectCureData.Models
{
    public partial class User
    {
        public User()
        {
            this.Events = new List<Event>();
        }

        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public int UserRoleId { get; set; }
        public bool UserActiveIn { get; set; }
        public bool UserNotifyFiveDays { get; set; }
        public bool UserNotifyTenDays { get; set; }
        public string UserPassword { get; set; }
        public ICollection<Event> Events { get; set; }
        public virtual Role Role { get; set; }
    }
}
