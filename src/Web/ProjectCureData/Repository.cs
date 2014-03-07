using System;
using System.Collections.Generic;
using System.Data.Entity;
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
				var user = ctx.Users.First(u => u.UserEmail == userName);
				if (user == null)
					return false;

				var hashedPassword = password == null ? null : SHA256Encryption.ComputeSHA256Hash(password);
				return (hashedPassword == user.UserPassword);
			}
		}

		public User GetUserByUserName(string userName)
		{
			using (var ctx = new ProjectCureContext())
			{
<<<<<<< HEAD
				var user = ctx.Users.FirstOrDefault(u => u.UserEmail == userName);
				if (user != null)
					user.UserPassword = null;
=======
				var user = ctx.Users
                    .Include("Role")
                    .FirstOrDefault(u => u.UserEmail == userName);
>>>>>>> f6bd98bed217a290de533a637afd7af89cf5b50f
				return user;
			}
		}

		public void UpdatePassword(User user)
		{
			using (var ctx = new ProjectCureContext())
			{
				var dbUser = ctx.Users.FirstOrDefault(u => u.UserEmail == user.UserEmail);
				if (dbUser == null)
					throw new ArgumentException();
				dbUser.UserPassword = SHA256Encryption.ComputeSHA256Hash(user.UserPassword);
				ctx.SaveChanges();
			}
		}

		public void SaveUser(User user)
		{
			using (var ctx = new ProjectCureContext())
			{
				var userExists = ctx.Users.Any(u => u.UserEmail == user.UserEmail);
				if (userExists)
				{
					ctx.Entry(user).State = EntityState.Modified;
					ctx.Entry(user).Property(x => x.UserPassword).IsModified = false;
				}
				else
				{
					user.UserPassword = null;
					ctx.Entry(user).State = EntityState.Added;
				}

				ctx.SaveChanges();
			}
		}
	}
}
