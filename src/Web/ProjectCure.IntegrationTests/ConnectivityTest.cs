﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectCureData;
using ProjectCureData.Models;

namespace ProjectCure.IntegrationTests
{
	[TestClass]
	public class ConnectivityTest
	{
		[TestMethod]
		public void ConnectAndRetrieve()
		{
			using (var context = new ProjectCureContext("Data Source=.;Initial Catalog=ProjectCure;Integrated Security=True;MultipleActiveResultSets=True"))
			{
				Assert.AreNotEqual(0, context.Roles.Count());
			}
		}

		[TestMethod]
		public void CanRetrieveUser()
		{
			var repo = new Repository();
			Assert.IsTrue(repo.IsValidUser("jpresa@gmail.com", "test"));


		}
	}
}
