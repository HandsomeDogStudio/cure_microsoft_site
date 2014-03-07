using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectCureData.Models;

namespace ProjectCureData
{
	public class Repository : IRepository
	{
		public bool IsValidUser(string userName, string password)
		{
			using (var ctx = new ProjectCureContext())
			{
				return ctx.Users.Any(u => u.UserEmail == userName && u.UserPassword == password);
			}
		}

		public User GetUserByUserName(string userName)
		{
			using (var ctx = new ProjectCureContext())
			{
				var user = ctx.Users
                    .Include("Role")
                    .FirstOrDefault(u => u.UserEmail == userName);
				return user;
			}
		}
	}
}
