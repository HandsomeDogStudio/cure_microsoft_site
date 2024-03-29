﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectCureData.Models;

namespace ProjectCure.Web.Models
{
    public class EditUserModel
    {
        public bool IsNew { get; set; }

        public int UserId { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(256)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Role")]
        public int RoleId { get; set; }
        
        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }
        
        [Display(Name = "Every 5 days")]
        public bool Notify5Days { get; set; }

        [Display(Name = "Every 10 days")]
        public bool Notify10Days { get; set; }

        public IEnumerable<Role> Roles { get; set; }

        public IEnumerable<Event> FutureEvents { get; set; }

        public IEnumerable<SelectListItem> RoleListItems
        {
            get
            {
                return 
                    (new[] { new SelectListItem { Text = "", Value = "" } })
                    .Union(
                        Roles.Select(r => new SelectListItem
                        {
                            Selected = r.RoleId == RoleId,
                            Text = r.RoleName,
                            Value = r.RoleId.ToString(),
                        })
                    );
            }
        }
    }
}