
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Mango.Utility.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration configuration;

        public EmailSender(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task SendAsync(string to, string htmlMessage, string subject = "")
        {
            var fromEmail = configuration.GetValue<string>("MailService:fromMail");
            var password = configuration.GetValue<string>("MailService:fromPassword");
          
            var message = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            message.To.Add(to);
            using var client = new SmtpClient("smtp.mail.yahoo.com", 587);

            using (var smptClient = new SmtpClient("smtp.mail.yahoo.com", 587)
            {
                Credentials = new NetworkCredential(fromEmail, password),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
            });

            try
            {
                await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
    }
}
