using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectCureData.Models;

namespace ProjectCureData
{
	public interface IRepository
	{
		// User Methods
		bool IsValidUser(string userName, string password);
		User GetUserByUserName(string userName);
		User GetUserById(int userId);
		void UpdatePassword(User user);
		void SaveUser(User user);
		IEnumerable<User> GetUserList();
		
		// Event Methods
		IEnumerable<Event> GetEventsBetweenDates(DateTime startDate, DateTime endDate);

		// Templae Methods
		Template GetTemplateByName(string templateName);
	}
}
