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
        public EmailNotifier()
        {
            Console.WriteLine(Environment.CurrentDirectory);
            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            host = System.Configuration.ConfigurationManager.AppSettings["Host"];
            host = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(Environment.CurrentDirectory).AppSettings.Settings["Host"].Value;
            Console.WriteLine();
            //Get the values for SMTP from the Web Config
            //var webConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null).AppSettings;
            //var host = webConfig.Settings["Host"].Value;
            //Console.WriteLine(webConfig.Settings["Host"].Value);
            //var port = int.Parse(webConfig.Settings["Port"].Value);
            //var enableSSL = bool.Parse(webConfig.Settings["EnableSSL"].Value);
            //var useDefaultCredentials = bool.Parse(webConfig.Settings["UseDefaultCredentials"].Value);
            //var username = webConfig.Settings["Username"].Value;
            //var password = webConfig.Settings["Password"].Value;


            //Create the smtp client with info given above
            smtpClient = new SmtpClient
            {
                Host = host,
                Port = port,
                EnableSsl = enableSSL,
                UseDefaultCredentials = useDefaultCredentials,
                Credentials = new System.Net.NetworkCredential(username, password)
            };
        }

        public string FillInTemplate(string template)
        {
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