using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class EmailManager
    {
        private readonly string smtpHost = "smtp.gmail.com";  // Use your SMTP server host (Gmail in this case)
        private readonly int smtpPort = 587;                  // For Gmail, use port 587 (TLS)
        private readonly string smtpUser;                     // Your email address
        private readonly string smtpPassword;                 // Your email password (be cautious with this)

        private readonly NetworkCredential networkCredential;
        private readonly SmtpClient smtpClient;

        public EmailManager()
        {
            this.smtpUser = Environment.GetEnvironmentVariable("EMAIL_USERNAME");
            this.smtpPassword = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");

            this.networkCredential = new NetworkCredential(smtpUser, smtpPassword);
            this.smtpClient = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = networkCredential,
                EnableSsl = true // Enable SSL for secure connection
            };
        }

        public bool IsEmailValid(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SendEmail(IEnumerable<string> toEmails, string subject, string body)
        {
            MailMessage mailMessage = new MailMessage()
            {
                From = new MailAddress(smtpUser),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            foreach (string toEmail in toEmails)
            {
                if (IsEmailValid(toEmail)) mailMessage.To.Add(toEmail);
            }

            smtpClient.Send(mailMessage);
        }
    }
}
