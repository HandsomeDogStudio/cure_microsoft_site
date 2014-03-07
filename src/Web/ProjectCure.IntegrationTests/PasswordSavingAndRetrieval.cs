using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectCureData;
using ProjectCureData.Models;

namespace ProjectCure.IntegrationTests
{
	[TestClass]
	public class PasswordSavingAndRetrieval
	{
		[TestMethod]
		public void SaveNewUserLeavesPasswordNull()
		{
			var userEmail = DateTime.Today.Ticks.ToString();
			var user = new User
			{
				UserEmail = userEmail,
				UserPassword = "hello there",
				UserRoleId = 1,
				UserActiveIn = true,
				UserFirstName = "TestF",
				UserLastName = "TestL",
				UserNotifyFiveDays = true,
				UserNotifyTenDays = false
			};

			var repo = new Repository();
			repo.SaveUser(user);

			var retrievedUser = repo.GetUserByUserName(userEmail);
			Assert.IsNull(retrievedUser.UserPassword);
		}

		[TestMethod]
		public void SaveExistingUserLeavesPasswordSame()
		{
			var repo = new Repository();
			var user = repo.GetUserByUserName("brian.avent@gmail.com");
			user.UserFirstName = "John";
			user.UserPassword = "GoodBye";

			repo.SaveUser(user);
			var retrieveUser = repo.GetUserByUserName("brian.avent@gmail.com");
			Assert.AreEqual("John", retrieveUser.UserFirstName);
			Assert.IsTrue(repo.IsValidUser("brian.avent@gmail.com", "HelloThere"));
		}

		[TestMethod]
		public void UpdatePasswordSetsPassword()
		{
			var repo = new Repository();
			var user = new User {UserEmail = "brian.avent@gmail.com", UserPassword = "HelloThere"};
			repo.UpdatePassword(user);

			Assert.IsTrue(repo.IsValidUser( "brian.avent@gmail.com", "HelloThere" ));
		}
	}
}
