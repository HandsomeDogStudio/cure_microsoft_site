using System;
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
            //testEmailer.SendNotification("nickolas.wood@hotmail.com", "Sign Up Template");
        }
    }
}
