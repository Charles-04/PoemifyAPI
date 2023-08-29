using Microsoft.Extensions.Configuration;
using Poemify.BLL.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Poemify.BLL.Services
{
    public class EmailService : IEmailService
    {
        IConfiguration _config;
        public EmailService(IConfiguration config)
        {
                _config = config;
        }
        public Task SendEmail()
        {
            string smtpHost =_config["SmtpSettings:Host"];
        int smtpPort = int.Parse(_config["SmtpSettings:Port"]);
        string smtpUsername =_config["SmtpSettings:Username"];
        string smtpPassword =_config["SmtpSettings:Password"];
        bool enableSsl = bool.Parse(_config["SmtpSettings:EnableSsl"]);

        using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
        {
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            smtpClient.EnableSsl = enableSsl;

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(smtpUsername);
            mailMessage.To.Add(toAddress);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            try
            {
                smtpClient.Send(mailMessage);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email. Error: " + ex.Message);
            }
        }
    }
}
