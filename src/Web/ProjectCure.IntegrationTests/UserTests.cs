using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectCureData;

namespace ProjectCure.IntegrationTests
{
	[TestClass]
	public class UserTests
	{
		[TestMethod]
		public void MiscellaneousTests()
		{
			var repo = new Repository();

			var user = repo.GetUserById(2);
			Assert.AreEqual("Chris", user.UserFirstName);

			var users = repo.GetUserList();
			Assert.AreNotEqual(0, users.Count());

			var roles = repo.GetRoleList();
			Assert.AreNotEqual(0, roles.Count());

			var admins = repo.GetAdminList();
			Assert.AreNotEqual(0, admins.Count());

			var user2 = repo.GetUserById(1);
			Assert.AreNotEqual(0, user2.Events.Count());

			var user3 = repo.GetUserById(3);
			Assert.AreEqual(0, user3.Events.Count());
		}

		[TestMethod]
		public void RemoveManagerFromFutureEvents()
		{
			var repo = new Repository();
			var events = repo.RemoveManagerFromEvents(2);
			Assert.AreNotEqual(0, events.Count());
		}
	}
}
