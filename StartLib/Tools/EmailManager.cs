using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace StartLib.Tools
{
    public class EmailManager
    {
        private MailMessage _MailMessage;
        private SmtpClient _SmptClient;

        public EmailManager(string host, int port, string id, string password)
        {
            _SmptClient = new SmtpClient(host, port);
            _SmptClient.Credentials = new NetworkCredential(id, password);

            _MailMessage = new MailMessage();
            _MailMessage.IsBodyHtml = true;
            _MailMessage.Priority = MailPriority.Normal;
        }

        #region Function
        public string From    
        {
            get { return _MailMessage.From == null ? String.Empty : _MailMessage.From.Address; }
            set { _MailMessage.From = new MailAddress(value); }
        }

        public MailAddressCollection To
        {
            get { return _MailMessage.To; }
        }

        public string subject
        {
            get { return _MailMessage.Subject; }
            set { _MailMessage.Subject = value; }
        }

        public string Body
        {
            get { return _MailMessage.Body; }
            set { _MailMessage.Body = value; }
        }

        public void Send()
        {
            _SmptClient.Send(_MailMessage);
        }
        #endregion


        #region Static Methods
        public static void Send(string from, string to, string subject, string contents, string cc, string bcc)
        {
            if(String.IsNullOrEmpty(from))
                throw new ArgumentNullException("Sender is empty.");
            if (String.IsNullOrEmpty(to))
                throw new ArgumentNullException("To is empty.");

            string smtpHost = ConfigurationManager.AppSettings["SMTPHost"];
            int smtpPort = 0;
            if (ConfigurationManager.AppSettings["SMTPHost"] == null ||
                    int.TryParse(ConfigurationManager.AppSettings["SMTPPort"], out smtpPort) == false)
                smtpPort = 25;

            Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);

            string smtpId = ConfigurationManager.AppSettings["SMTPId"]; ;
            string smtpPwd = ConfigurationManager.AppSettings["SMTPPassword"];



            MailMessage mailMsg = new MailMessage();
            mailMsg.From = new MailAddress(from);
            mailMsg.To.Add(to);    

            if (!String.IsNullOrEmpty(cc))  
                mailMsg.CC.Add(cc);
            if (!String.IsNullOrEmpty(bcc)) 
                mailMsg.Bcc.Add(bcc);

            mailMsg.Subject = subject;
            mailMsg.IsBodyHtml = true; 
            mailMsg.Body = contents;
            mailMsg.Priority = MailPriority.Normal; 

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential();  
            smtpClient.Host = smtpHost;   
            smtpClient.Port = smtpPort;  
            smtpClient.Send(mailMsg);
        }

        public static void Send(string from, string to, string subject, string contents)
        {
            Send(from, to, subject, contents, null, null);
        }

        public static void Send(string to, string subject, string contents)
        {
            string sender = ConfigurationManager.AppSettings["SMTPSender"];
            Send(sender, to, subject, contents);

        }
        #endregion
    }
}
