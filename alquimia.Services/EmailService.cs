using alquimia.Services.Interfaces;
using alquimia.Services.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace alquimia.Services
{
    public class EmailService : IEmailService
    {
        //private readonly IConfiguration _config;
        private readonly EmailSettings _settings;

        //public EmailService(IConfiguration config)
        //{
        //    _config = config;
        //}
        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendEmailAsync(string recipient, string _subject, string mensajeHtml)
        {
            using var smtpClient = new SmtpClient(_settings.SmtpHost, _settings.SmtpPort)
            {
                //Port = int.Parse(_config["Email:SmtpPort"]),
                Credentials = new NetworkCredential(_settings.User, _settings.Password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_settings.From, "Alquimia"),
                Subject = _subject,
                Body = mensajeHtml,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(recipient);
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
