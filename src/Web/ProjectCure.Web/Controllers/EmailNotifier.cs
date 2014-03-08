using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using ProjectCureData;
using ProjectCureData.Models;

namespace ProjectCure.Web.Controllers
{
    public class EmailNotifier
    {
        private SmtpClient smtpClient;
        private string host;
        private int port;
        private bool enableSSL;
        private bool useDefaultCredentials;
        private string username;
        private string password;

        /// <summary>
        /// The default constructor is using gmail, donotreply.projectcure@gmail.com (password: AnnieEllement)
        /// </summary>
        public EmailNotifier()
        {
            //These are the default values being used (also for testing purposes)
            host = "smtp.gmail.com";
            port = 587;
            enableSSL = true;
            useDefaultCredentials = false;
            username = "donotreply.projectcure@gmail.com";
            password = "AnnieEllement";


            //Create the smtp client with info given above
            CreateSmtpClient();
        }

        /// <summary>
        /// This version is used when you want to define the SMTP info from another source outside of gmail
        /// </summary>
        /// <param name="smtpClient"></param>
        public EmailNotifier(SmtpClient smtpClient)
        {
            //Create the smtp client with info given above
            this.smtpClient = smtpClient;
        }

        private void CreateSmtpClient()
        {
            smtpClient = new SmtpClient
            {
                Host = host,
                Port = port,
                EnableSsl = enableSSL,
                UseDefaultCredentials = useDefaultCredentials,
                Credentials = new System.Net.NetworkCredential(username, password)
            };
        }

        private void GetTemplateByTemplateName(IRepository repository, string templateName, out string templateBody, out string templateSubject)
        {
            Template template = repository.GetTemplateByName(templateName);
            //modify this to fill in the subject from the given template+
            templateBody = template.TemplateText;
            templateSubject = template.TemplateSubject;
        }

        public void GiveTemporaryPassword(IRepository repository, string recipientAddress, string tempPassword, string templateName = "Password Reset Email")
        {
            string templateBody;
            string templateSubject;

            GetTemplateByTemplateName(repository, templateName, out templateBody, out templateSubject);

            //Fill in the dynamic variables from template


        }


        private void SendNotification(List<string> recipientAddresses, string body, string subject)
        {

            var email = new MailMessage("donotreply@projectcure.org", recipientAddresses.First());

            //Remove the already used email address from the array
            recipientAddresses.Remove(recipientAddresses.First());
            //cycle through any other addresses to put them in the to field.
            if (recipientAddresses.Any())
            {
                foreach (var address in recipientAddresses)
                {
                    email.To.Add(address);
                }
            }

            email.Subject = subject;
            email.Body = body;
                
            smtpClient.Send(email);
        }
    }
}