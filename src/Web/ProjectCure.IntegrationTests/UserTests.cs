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
		}
	}
}
