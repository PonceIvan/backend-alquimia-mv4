using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using alquimia.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace alquimia.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string destinatario, string asunto, string mensajeHtml)
        {
            var smtpClient = new SmtpClient(_config["Email:SmtpHost"])
            {
                Port = int.Parse(_config["Email:SmtpPort"]),
                Credentials = new NetworkCredential(_config["Email:User"], _config["Email:Password"]),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_config["Email:From"], "Alquimia"),
                Subject = asunto,
                Body = mensajeHtml,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(destinatario);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
