using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using ProjectCureData;
using ProjectCureData.Models;
using System.Configuration;
using System.Net.Configuration;

namespace ProjectCure.Web.Controllers
{
	public class EmailNotifier
	{
		private IInjectableSmtpClient _smtpClient;
		private string host;
		private int port;
		private bool enableSSL;
		private bool useDefaultCredentials;
		private string username;
		private string password;
		private SmtpSection section;

		/// <summary>
		/// The default constructor is using gmail, donotreply.projectcure@gmail.com (password: AnnieEllement)
		/// </summary>
		public EmailNotifier()
		{
			//These are the default values being used (also for testing purposes)
			//host = "smtp.gmail.com";
			//port = 587;
			//enableSSL = true;
			//useDefaultCredentials = false;
			//username = "donotreply.projectcure@gmail.com";
			//password = "AnnieEllement";


			//Create the smtp client with info given above
			_smtpClient = new InjectableSmptClient();
			//SetSection();
		}

		/// <summary>
		/// This version is used when you want to define the SMTP info from another source outside of gmail
		/// </summary>
		/// <param name="smtpClient"></param>
		public EmailNotifier(IInjectableSmtpClient smtpClient)
		{
			_smtpClient = smtpClient;
			//SetSection();
		}

		public void GiveTemporaryPasswordNotification(IRepository repository, string recipientAddress, string tempPassword)
		{
			string templateName = "Password Reset Email";
			string templateBody;
			string templateSubject;

			GetTemplateByTemplateName(repository, templateName, out templateBody, out templateSubject);

			//Fill in the dynamic variables from template
			templateBody = templateBody.Replace("{name}", GetFullNameFromEmailAddress(repository, recipientAddress));
			templateBody = templateBody.Replace("{temp password}", tempPassword);

			//Send the email
			SendNotification(repository, new List<string>() { recipientAddress }, templateBody, templateSubject);
		}

		//private void SetSection()
		//{
		//	section = ConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection;
		//}

		public void PasswordChangeConfirmationNotification(IRepository repository, string recipientAddress)
		{
			string templateName = "Password Change Confirmation Email";
			string templateBody;
			string templateSubject;

			GetTemplateByTemplateName(repository, templateName, out templateBody, out templateSubject);

			//Fill in the dynamic variables from template
			templateBody = templateBody.Replace("{name}", GetFullNameFromEmailAddress(repository, recipientAddress));

			//Send the email
			SendNotification(repository, new List<string> { recipientAddress }, templateBody, templateSubject);
		}

		public void EventCancellationNotification(IRepository repository, Event cancelledEvent, string recipientAddress)
		{
			string templateName = "Cancellation Email";
			string templateBody;
			string templateSubject;

			GetTemplateByTemplateName(repository, templateName, out templateBody, out templateSubject);

			//Fill in the dynamic variables from template
			templateBody = templateBody.Replace("{name}", GetFullNameFromEmailAddress(repository, recipientAddress));
			templateBody = templateBody.Replace("{title}", cancelledEvent.EventTitle);
			templateBody = templateBody.Replace("{date}", cancelledEvent.EventStartDateTime.Date.ToShortDateString());
			templateBody = templateBody.Replace("{start time}", cancelledEvent.EventStartDateTime.ToShortTimeString());
			templateBody = templateBody.Replace("{end time}", cancelledEvent.EventEndDateTime.ToShortTimeString());

			//Add admins to this email thread.
			var listOfRecipients = new List<string> { recipientAddress };
			listOfRecipients.AddRange(repository.GetAdminList().Select(user => user.UserEmail));

			//Send the email
			SendNotification(repository, listOfRecipients, templateBody, templateSubject);
		}

        public void LeadCancellationNotification(IRepository repository, Event evt)
        {
            //TODO: Implement admin notification when a user cancels and event
        }
        
		public void UnfilledEventsNotification(IRepository repository, List<Event> eventsToBeListed)
		{
            var emails = repository.GetUserList()
                                   .Where(c => c.UserActiveIn)
                                   .Select(c => c.UserEmail)
                                   .ToArray();

			string templateName = "Unfilled Group Lead Email";
			string templateBody;
			string templateSubject;

			GetTemplateByTemplateName(repository, templateName, out templateBody, out templateSubject);

			//Create list of unfilled events
			string eventsText = "";
			foreach (var unfilledEvent in eventsToBeListed)
			{
				eventsText += unfilledEvent.EventTitle + " on " + unfilledEvent.EventStartDateTime.Date.ToShortDateString()
							+ " from " + unfilledEvent.EventStartDateTime.ToShortTimeString() + " to "
							+ unfilledEvent.EventEndDateTime.ToShortTimeString() + "\n\n";
			}

			//Fill in the dynamic variables from template
			templateBody = templateBody.Replace("{events}", eventsText);

			//Send the email
            SendNotification(repository, null, templateBody, templateSubject, emails);
		}

		private string GetFullNameFromEmailAddress(IRepository repository, string userEmailAddress)
		{
			var user = repository.GetUserByUserName(userEmailAddress);
			var userFullName = user.UserFirstName + " " + user.UserLastName;
			return userFullName;
		}

		private void GetTemplateByTemplateName(IRepository repository, string templateName, out string templateBody, out string templateSubject)
		{
			Template template = repository.GetTemplateByName(templateName);
			templateBody = template.TemplateText;
			templateSubject = template.TemplateSubject;
		}

		private void SendNotification(IRepository repository, IEnumerable<string> addresses, string body, string subject, IEnumerable<string> bccRecipients = null )
		{
			var email = new MailMessage();

			email.Subject = subject;
			email.Body = body;

            //Check to make sure any of potential recipients are inactive that they are not sent an email.
            if (addresses != null && addresses.Count() > 0)
            {
                var emails = addresses.Where(recipientAddress => repository.GetUserByUserName(recipientAddress).UserActiveIn).ToArray();
                if (emails != null && emails.Count() > 0)
                    email.To.Add(string.Join<string>(",", addresses));
            }

            if(bccRecipients != null && bccRecipients.Count() > 0)
                email.Bcc.Add(string.Join<string>(",", bccRecipients));

		    _smtpClient.Send(email);
		}
	}

	public interface IInjectableSmtpClient
	{
		void Send(MailMessage message);
	}

	public class InjectableSmptClient : IInjectableSmtpClient
	{
		private readonly SmtpClient smtpClient;

		public InjectableSmptClient()
		{
			smtpClient = new SmtpClient();
		}

		public void Send(MailMessage message)
		{
			smtpClient.Send(message);
		}
	}
}