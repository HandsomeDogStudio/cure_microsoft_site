using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectCure.Web.Controllers;

namespace ProjectCure.IntegrationTests
{
    [TestClass]
    public class TestEmailNotifier
    {
        [TestMethod]
        public void TestHappyPathEmailNotifier()
        {
            var testEmailer = new EmailNotifier();
            //testEmailer.SendNotification(new List<string>() {"nickolas.wood@hotmail.com"}, "Stuff here", "Sign Up Template");
        }
    }
}
