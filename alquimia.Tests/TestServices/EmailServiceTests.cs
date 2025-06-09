using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using alquimia.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace alquimia.Tests.TestServices
{
    public class EmailServiceTests
    {
        private readonly Mock<IConfiguration> _configMock;
        private readonly Mock<SmtpClient> _smtpClientMock;
        private readonly EmailService _emailService;

        public EmailServiceTests()
        {
            _configMock = new Mock<IConfiguration>();
            _smtpClientMock = new Mock<SmtpClient>();
            _emailService = new EmailService(_configMock.Object);
        }

        //[Fact]
        //public async Task SendEmailAsync_ShouldCallSendMailAsync()
        //{
        //    // Arrange
        //    var destinatario = "test@example.com";
        //    var asunto = "Test Subject";
        //    var mensajeHtml = "<p>This is a test email</p>";

        //    _configMock.SetupGet(c => c["Email:SmtpHost"]).Returns("smtp.example.com");
        //    _configMock.SetupGet(c => c["Email:SmtpPort"]).Returns("587");
        //    _configMock.SetupGet(c => c["Email:User"]).Returns("user@example.com");
        //    _configMock.SetupGet(c => c["Email:Password"]).Returns("password");
        //    _configMock.SetupGet(c => c["Email:From"]).Returns("noreply@example.com");

        //    // Act
        //    await _emailService.SendEmailAsync(destinatario, asunto, mensajeHtml);

        //    // Assert
        //    _smtpClientMock.Verify(s => s.SendMailAsync(It.IsAny<MailMessage>()), Times.Once);
        //}

        //[Fact]
        //public async Task SendEmailAsync_ShouldThrowException_WhenSmtpClientThrowsException()
        //{
        //    // Arrange
        //    var destinatario = "test@example.com";
        //    var asunto = "Test Subject";
        //    var mensajeHtml = "<p>This is a test email</p>";

        //    _configMock.SetupGet(c => c["Email:SmtpHost"]).Returns("smtp.example.com");
        //    _configMock.SetupGet(c => c["Email:SmtpPort"]).Returns("587");
        //    _configMock.SetupGet(c => c["Email:User"]).Returns("user@example.com");
        //    _configMock.SetupGet(c => c["Email:Password"]).Returns("password");
        //    _configMock.SetupGet(c => c["Email:From"]).Returns("noreply@example.com");

        //    _smtpClientMock.Setup(s => s.SendMailAsync(It.IsAny<MailMessage>())).ThrowsAsync(new SmtpException("SMTP error"));

        //    // Act & Assert
        //    await Assert.ThrowsAsync<SmtpException>(() => _emailService.SendEmailAsync(destinatario, asunto, mensajeHtml));
        //}

        //[Fact]
        //public async Task SendEmailAsync_ShouldSendEmail_WithCorrectParameters()
        //{
        //    // Arrange
        //    var destinatario = "test@example.com";
        //    var asunto = "Test Subject";
        //    var mensajeHtml = "<p>This is a test email</p>";

        //    var mailMessage = new MailMessage
        //    {
        //        From = new MailAddress("noreply@example.com", "Alquimia"),
        //        Subject = asunto,
        //        Body = mensajeHtml,
        //        IsBodyHtml = true
        //    };
        //    mailMessage.To.Add(destinatario);

        //    _configMock.SetupGet(c => c["Email:SmtpHost"]).Returns("smtp.example.com");
        //    _configMock.SetupGet(c => c["Email:SmtpPort"]).Returns("587");
        //    _configMock.SetupGet(c => c["Email:User"]).Returns("user@example.com");
        //    _configMock.SetupGet(c => c["Email:Password"]).Returns("password");
        //    _configMock.SetupGet(c => c["Email:From"]).Returns("noreply@example.com");

        //    // Act
        //    await _emailService.SendEmailAsync(destinatario, asunto, mensajeHtml);

        //    // Assert
        //    _smtpClientMock.Verify(s => s.SendMailAsync(It.Is<MailMessage>(m =>
        //        m.Subject == asunto &&
        //        m.Body == mensajeHtml &&
        //        m.To.ToString() == destinatario)), Times.Once);
        //}

        //[Fact]
        //public async Task SendEmailAsync_ShouldLogError_WhenExceptionOccurs()
        //{
        //    // Arrange
        //    var destinatario = "test@example.com";
        //    var asunto = "Test Subject";
        //    var mensajeHtml = "<p>This is a test email</p>";

        //    _configMock.SetupGet(c => c["Email:SmtpHost"]).Returns("smtp.example.com");
        //    _configMock.SetupGet(c => c["Email:SmtpPort"]).Returns("587");
        //    _configMock.SetupGet(c => c["Email:User"]).Returns("user@example.com");
        //    _configMock.SetupGet(c => c["Email:Password"]).Returns("password");
        //    _configMock.SetupGet(c => c["Email:From"]).Returns("noreply@example.com");

        //    _smtpClientMock.Setup(s => s.SendMailAsync(It.IsAny<MailMessage>())).ThrowsAsync(new SmtpException("SMTP error"));

        //    var loggerMock = new Mock<ILogger<EmailService>>();

        //    var emailService = new EmailService(_configMock.Object);
        //    var exception = await Assert.ThrowsAsync<SmtpException>(() => emailService.SendEmailAsync(destinatario, asunto, mensajeHtml));

        //    // Assert
        //    loggerMock.Verify(l => l.LogError(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once);
        //}

        //[Fact]
        //public async Task SendEmailAsync_ShouldNotSendEmail_WithInvalidEmailFormat()
        //{
        //    // Arrange
        //    var destinatario = "invalid-email"; // Invalid email format
        //    var asunto = "Test Subject";
        //    var mensajeHtml = "<p>This is a test email</p>";

        //    var mailMessage = new MailMessage
        //    {
        //        From = new MailAddress("noreply@example.com", "Alquimia"),
        //        Subject = asunto,
        //        Body = mensajeHtml,
        //        IsBodyHtml = true
        //    };
        //    mailMessage.To.Add(destinatario);

        //    _configMock.SetupGet(c => c["Email:SmtpHost"]).Returns("smtp.example.com");
        //    _configMock.SetupGet(c => c["Email:SmtpPort"]).Returns("587");
        //    _configMock.SetupGet(c => c["Email:User"]).Returns("user@example.com");
        //    _configMock.SetupGet(c => c["Email:Password"]).Returns("password");
        //    _configMock.SetupGet(c => c["Email:From"]).Returns("noreply@example.com");

        //    // Act
        //    await Assert.ThrowsAsync<FormatException>(() => _emailService.SendEmailAsync(destinatario, asunto, mensajeHtml));
        //}

        //[Fact]
        //public async Task SendEmailAsync_ShouldThrowException_WhenConfigurationIsMissing()
        //{
            
        //   var destinatario = "test@example.com";
        //    var asunto = "Test Subject";
        //    var mensajeHtml = "<p>This is a test email</p>";

        //    _configMock.SetupGet(c => c["Email:SmtpHost"]).Returns<string>(null); // Missing configuration

            
        //    var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
        //        _emailService.SendEmailAsync(destinatario, asunto, mensajeHtml));

        //    Assert.Equal("Value cannot be null. (Parameter 'Email:SmtpHost')", exception.Message);
        //}


    }
}
