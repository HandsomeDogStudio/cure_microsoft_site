using System;
using System.Collections.Generic;
using System.Net.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectCure.Web.Controllers;
using Moq;
using ProjectCureData;
using ProjectCureData.Models;

namespace ProjectCure.IntegrationTests
{
    [TestClass]
    public class TestEmailNotifier
    {
	    private readonly Mock<IInjectableSmtpClient> _smtpClientMock;
	    private readonly Mock<IRepository> _repositoryMock;
	    private Template _passwordResetEmail;
	    private Template _passwordChangeNotificationEmail;

	    public TestEmailNotifier()
	    {
		    InitializeTemplates();
			_smtpClientMock = new Mock<IInjectableSmtpClient>();
		    _smtpClientMock.Setup(s => s.Send(It.IsAny<MailMessage>())).Callback<MailMessage>(
			    m =>
			    {
					Assert.AreEqual("test.user@hello.com", m.To[0].Address);
					Assert.AreEqual("Project C.U.R.E - Password reset confirmation", m.Subject);
					Assert.IsTrue(m.Body.Contains("Dear Test User"));
			    });
			_repositoryMock = new Mock<IRepository>();
			_repositoryMock.Setup(r => r.GetTemplateByName("Password Reset Email"))
				.Returns(_passwordResetEmail);
		    _repositoryMock.Setup(r => r.GetTemplateByName("Password Change Confirmation Email"))
				.Returns(_passwordChangeNotificationEmail);
		    _repositoryMock.Setup(r => r.GetUserByUserName("test.user@hello.com"))
			    .Returns(new User {UserFirstName = "Test", UserLastName = "User", UserActiveIn = true});
	    }

        [TestMethod]
		public void TestGiveTemporaryPasswordNotification()
        {
			_smtpClientMock.Setup(s => s.Send(It.IsAny<MailMessage>())).Callback<MailMessage>(
				m =>
				{
					Assert.AreEqual("test.user@hello.com", m.To[0].Address);
					Assert.AreEqual("Project C.U.R.E - Password reset confirmation", m.Subject);
					Assert.IsTrue(m.Body.Contains("Dear Test User"));
					Assert.IsTrue(m.Body.Contains("xyz"));
				});
            var testEmailer = new EmailNotifier(_smtpClientMock.Object);
			testEmailer.GiveTemporaryPasswordNotification(_repositoryMock.Object, "test.user@hello.com", "xyz");
        }

	    [TestMethod]
	    public void TestPasswordChangeConfirmationNotification()
	    {
			_smtpClientMock.Setup(s => s.Send(It.IsAny<MailMessage>())).Callback<MailMessage>(
				m =>
				{
					Assert.AreEqual("test.user@hello.com", m.To[0].Address);
					Assert.AreEqual("Project C.U.R.E - Password change confirmation", m.Subject);
					Assert.IsTrue(m.Body.Contains("Dear Test User"));
				});
			var testEmailer = new EmailNotifier(_smtpClientMock.Object);
			testEmailer.PasswordChangeConfirmationNotification(_repositoryMock.Object, "test.user@hello.com");
	    }

	    private void InitializeTemplates()
	    {
		    _passwordResetEmail = new Template();
		    _passwordResetEmail.TemplateId = 1;
			_passwordResetEmail.TemplateName = "Password Reset Email";
			_passwordResetEmail.TemplateSubject = "Project C.U.R.E - Password reset confirmation";
		    _passwordResetEmail.TemplateText =
			    "Dear {name},    Your account password at Project C.U.R.E has been reset and you have " +
			    "been issued with a new temporary password {temp password}.  Please go to this page and " +
			    "change your password:  http://projectcure.azurewebsites.net/    Thanks,  The Project C.U.R.E. Team";

			_passwordChangeNotificationEmail = new Template();
		    _passwordChangeNotificationEmail.TemplateId = 6;
			_passwordChangeNotificationEmail.TemplateName = "Password Change Confirmation Email";
		    _passwordChangeNotificationEmail.TemplateSubject = "Project C.U.R.E - Password change confirmation";
		    _passwordChangeNotificationEmail.TemplateText =
			    "Dear {name},    Thank you for visiting the Project C.U.R.E website. As per your request, " +
			    "we have successfully changed your password.    Thanks,  The Project C.U.R.E. Team";
	    }
    }
}
