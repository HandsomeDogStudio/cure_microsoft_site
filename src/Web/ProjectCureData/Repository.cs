using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
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
				var user = ctx.Users.FirstOrDefault(u => u.UserEmail == userName);
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
				var user = ctx.Users
                    .Include("Role")
                    .FirstOrDefault(u => u.UserEmail == userName);
				return user;
			}
		}

		public User GetUserById(int userId)
		{
			using (var ctx = new ProjectCureContext())
			{
				var user = ctx.Users
					.Include("Role")
					.Include("Events")
					.FirstOrDefault(u => u.UserId == userId);
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
				var userExists = ctx.Users.Any(u => u.UserId == user.UserId);
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

		public IEnumerable<User> GetUserList()
		{
			using (var ctx = new ProjectCureContext())
			{
				var users = ctx.Users
					.Include("Role")
                    .OrderBy(u => u.UserActiveIn ? 0 : 1)
                    .ThenBy(u => u.UserLastName)
                    .ThenBy(u => u.UserFirstName)
					.ToList();

				foreach (var user in users.Where(u => u.UserActiveIn))
				{
					var closureUser = user;
					user.Events = ctx.Events
						.Where(e => e.User.UserId == closureUser.UserId && e.EventStartDateTime > DateTime.Now)
						.OrderBy(e => e.EventStartDateTime)
						.Take(1).ToList();
				}

				return users;
			}
		}

		public IEnumerable<Role> GetRoleList()
		{
			using (var ctx = new ProjectCureContext())
			{
				var roles = ctx.Roles.ToList();
				return roles;
			}
		}

		public IEnumerable<User> GetAdminList()
		{
			using (var ctx = new ProjectCureContext())
			{
				var users = ctx.Users
					.Include("Role")
					.Where(u => u.Role.RoleName.ToUpper().StartsWith("ADMIN"))
					.ToList();

				return users;
			}
		}

		public IEnumerable<Event> GetEventsBetweenDates(DateTime startDate, DateTime endDate)
        {
            endDate = endDate.AddDays(1);
            using (var ctx = new ProjectCureContext())
            {
                return ctx.Events.Include("User").Where(e => e.EventStartDateTime >= startDate && e.EventEndDateTime < endDate).ToList();
            }
        }


		public IEnumerable<Event> RemoveManagerFromEvents(int userId)
		{
			using (var ctx = new ProjectCureContext())
			{
				var userEvents = ctx.Events.Where(e => e.User.UserId == userId && e.EventStartDateTime > DateTime.Now);
				var returnedEvents = userEvents.ToList();
				foreach (var userEvent in userEvents)
					userEvent.EventManagerId = null;
				ctx.SaveChanges();
				return returnedEvents;
			}
		}

		public Template GetTemplateByName(string templateName)
		{
			using (var ctx = new ProjectCureContext())
			{
				return ctx.Templates.SingleOrDefault(t => t.TemplateName == templateName);
			}
		}

	    public Event GetEventById(int eventId)
	    {
	        using (var ctx = new ProjectCureContext())
	        {
                return ctx.Events.Include("User").FirstOrDefault(e => e.EventId == eventId);
	        }
	    }

	    public void DeleteEventById(int eventId)
	    {
	        using (var ctx = new ProjectCureContext())
	        {
	            ctx.Events.Remove(ctx.Events.First(e => e.EventId == eventId));
	            ctx.SaveChanges();
	        }
	    }

	    public void SaveEvent(Event @event)
	    {
	        using (var ctx = new ProjectCureContext())
	        {
	            bool eventExists = ctx.Events.Any(e => e.EventId == @event.EventId);
	            if (eventExists)
	            {
                    ctx.Entry(@event).State = EntityState.Modified;
	            }
	            else
	            {
	                ctx.Entry(@event).State = EntityState.Added;
	            }

	            ctx.SaveChanges();
	        }
	    }

	    public void AssignManager(int eventId, string username)
	    {
	        using (new ProjectCureContext())
	        {
	            Event e = GetEventById(eventId);
                if(e == null) throw new ArgumentException();
	            User u = null;
	            int? managerId = null;
	            if (username != null)
	            {
	                u = GetUserByUserName(username);
                    if(u == null) throw new ArgumentException();
	                managerId = u.UserId;
	            }
	            e.User = u;
	            e.EventManagerId = managerId;
                SaveEvent(e);
	        }
	    }
	}
}
