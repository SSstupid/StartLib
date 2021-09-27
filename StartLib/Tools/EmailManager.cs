using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Configuration; // 어셈블리에서 참조 추가해야함

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
            //_MailMessage.Attachments.Add(new Attachment(파일경로_path));
        }

        #region Function
        public string From     //From만 노출시키고 string으로 받기 
        {
            get { return _MailMessage.From == null ? String.Empty : _MailMessage.From.Address; }        //null이면 empty값 return 아니면 Address
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
            // 변경되지 않을 값들을 App.config에 저장했음
            // string sender = ConfigurationManager.AppSettings["SMTPSender"]; from으로 받기 때문에 대체함.

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
            mailMsg.To.Add(to);     // string으로 받기

            if (!String.IsNullOrEmpty(cc))  //참조
                mailMsg.CC.Add(cc);
            if (!String.IsNullOrEmpty(bcc)) //숨은참조
                mailMsg.Bcc.Add(bcc);

            mailMsg.Subject = subject;
            mailMsg.IsBodyHtml = true;   // text, html로 보낼것인가
            mailMsg.Body = contents;
            mailMsg.Priority = MailPriority.Normal;     // Low ~ High 메일 중요도 설정

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential();   //인증서
            smtpClient.Host = smtpHost;     //받는자?
            smtpClient.Port = smtpPort;     //받는자?
            smtpClient.Send(mailMsg);

            //mailMsg.To.Add(new MailAddress(to));
        }

        public static void Send(string from, string to, string subject, string contents)// cc, bcc 없는 경우
        {
            Send(from, to, subject, contents, null, null);
        }

        public static void Send(string to, string subject, string contents) //from이 없는 경우
        {
            string sender = ConfigurationManager.AppSettings["SMTPSender"];
            Send(sender, to, subject, contents);

        }
        #endregion
    }
}
