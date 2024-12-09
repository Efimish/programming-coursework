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

        private NetworkCredential networkCredential;
        private SmtpClient smtpClient;

        public EmailManager()
        {
            smtpUser = Environment.GetEnvironmentVariable("EMAIL_USERNAME");
            smtpPassword = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");

            if (string.IsNullOrWhiteSpace(smtpUser) || string.IsNullOrWhiteSpace(smtpPassword))
            {
                throw new Exception(
                    "Не удалось прочитать Email адрес и пароль из окружения." +
                    "Убедитесь, что создали и заполниили файл `.env` в корне решения." +
                    "(Рядом с файлом `.env.example`)"
                );
            }
        }

        private void EnsureConnected()
        {
            if (smtpClient == null)
            {
                try
                {
                    networkCredential = new NetworkCredential(smtpUser, smtpPassword);
                    smtpClient = new SmtpClient(smtpHost, smtpPort)
                    {
                        Credentials = networkCredential,
                        EnableSsl = true // Enable SSL for secure connection
                    };
                } catch (Exception)
                {
                    throw new Exception("Не удалось подключиться к SMTP");
                }
            }
        }

        public static bool IsEmailValid(string email)
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
            EnsureConnected();
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

        public async Task SendRent(string email, Rent rent, Skis skis)
        {
            EnsureConnected();
            if (!IsEmailValid(email)) throw new Exception("Указан неверный адрес Email");

            string date = rent.StartTime.ToString("D");
            string time = rent.StartTime.ToString("t");
            string body =
                "Здравствуйте!\n" +
                $"Сегодня в {time} вы арендовали лыжи №{skis.ID} - {skis.Model}.\n" +
                $"Каждый час аренды обойдется вам в {skis.PricePerHour} рублей/час.";

            MailMessage message = new MailMessage()
            {
                From = new MailAddress(smtpUser),
                Subject = $"Аренда лыж от {date}",
                Body = body,
                IsBodyHtml = true
            };

            message.To.Add(email);

            try
            {
                await smtpClient.SendMailAsync(message);
            } catch (Exception)
            {
                throw new Exception("Не удалось отправить Email");
            }
        }
    }
}
