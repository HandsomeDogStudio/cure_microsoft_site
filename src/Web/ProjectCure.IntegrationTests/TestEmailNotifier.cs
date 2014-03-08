using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectCure.Web.Controllers;
using ProjectCureData;

namespace ProjectCure.IntegrationTests
{
    [TestClass]
    public class TestEmailNotifier
    {
        [TestMethod]
        public void TestHappyPathEmailNotifier()
        {
            var testEmailer = new EmailNotifier();
            //testEmailer.GiveTemporaryPasswordNotification(, );
        }
    }
}
