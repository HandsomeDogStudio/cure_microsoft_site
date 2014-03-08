using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

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
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="enableSSL"></param>
        /// <param name="useDefaultCredentials"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
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

        public string GetTemplateSubject(string templateName)
        {
            //modify this to fill in the subject from the given template
            return "something";
        }

        public string FillInTemplate(string templateName)
        {
            //modify this to fill in the template with the correct name and such
            return "something";
        }

        public void SendNotification(string recipientAddress, string templateName)
        {
            var email = new MailMessage("donotreply@projectcure.org", recipientAddress);

            //Get template subject and body message
            
            email.Subject = "sub";
            email.Body = "Test";
                
            smtpClient.Send(email);
        }
    }
}