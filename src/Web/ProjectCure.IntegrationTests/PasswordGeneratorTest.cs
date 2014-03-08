using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectCure.Web.Models;

namespace ProjectCure.IntegrationTests
{
	[TestClass]
	public class PasswordGeneratorTest
	{
		[TestMethod]
		public void GeneratePassword()
		{
			var pg = new PasswordGenerator();
			var pwd = pg.GeneratePassword(8);
			Assert.AreNotEqual(0, pwd.Length);
		}
	}
}
