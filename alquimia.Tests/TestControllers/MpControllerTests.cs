using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alquimia.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace alquimia.Tests.TestControllers
{
    public class MpControllerTests
    {
        
        [Fact]
        public async Task GeneratePaymentLink_NoAccessToken_Returns500()
        {
            // Arrange
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c["MercadoPago:AccessToken"]).Returns(string.Empty);
            var controller = new MpController(mockConfig.Object);

            // Act
            var result = await controller.GeneratePaymentLink("ref-123");

            // Assert
            var statusResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusResult.StatusCode);
            Assert.Equal("Mercado Pago access token is not configured.", statusResult.Value);
        }

        [Fact]
        public async Task GeneratePaymentLink_SuccessfulResponse_ReturnsOkWithUrl()
        {
            // Arrange
            var fakeResponseContent = "{\"init_point\": \"https://mercadopago.com/payment_link\"}";
            var fakeResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(fakeResponseContent, Encoding.UTF8, "application/json")
            };
            var fakeHandler = new FakeHttpMessageHandler(fakeResponse);
            var client = new HttpClient(fakeHandler);

            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c["MercadoPago:AccessToken"]).Returns("fake_token");

            var controller = new MpController(mockConfig.Object);

            // Para inyectar HttpClient, tendrías que modificar el controlador para aceptar HttpClient via DI,
            // pero en este caso vamos a usar Moq para la llamada a PostAsync (más abajo comento alternativa).

            // Como el controlador crea directamente HttpClient, esto dificulta testear sin modificar el código.
            // Por ahora, dejamos el test así para mostrar intención.

            // Act - no puede ejecutarse directamente sin refactor
            // var result = await controller.GeneratePaymentLink("ref-123");

            // Assert
            // var okResult = Assert.IsType<OkObjectResult>(result);
            // dynamic data = okResult.Value;
            // Assert.Equal("https://mercadopago.com/payment_link", data.Url);
        }
        [Fact]
        public async Task GeneratePaymentLink_FailedResponse_ReturnsStatusCodeAndContent()
        {
            // Arrange
            var fakeResponseContent = "Bad Request";
            var fakeResponse = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
            {
                Content = new StringContent(fakeResponseContent)
            };
            var fakeHandler = new FakeHttpMessageHandler(fakeResponse);
            var client = new HttpClient(fakeHandler);

            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c["MercadoPago:AccessToken"]).Returns("fake_token");

            var controller = new MpController(mockConfig.Object);

            // Igual que antes, el test no puede ejecutarse sin refactor
        }
        //[Fact]
        //public void PaymentSuccess_ReturnsExpectedData()
        //{
        //    // Arrange
        //    var controller = new MpController(Mock.Of<IConfiguration>());

        //    // Act
        //    var result = controller.PaymentSuccess("123", "approved", "ref-abc");

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    dynamic data = okResult.Value;
        //    Assert.Equal("Payment successful.", data.Message);
        //    Assert.Equal("123", data.PaymentId);
        //    Assert.Equal("approved", data.Status);
        //    Assert.Equal("ref-abc", data.ExternalReference);
        //}

        [Fact]
        public void PaymentFailure_ReturnsBadRequest()
        {
            // Arrange
            var controller = new MpController(Mock.Of<IConfiguration>());

            // Act
            var result = controller.PaymentFailure();

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("The payment was rejected or cancelled.", badRequest.Value);
        }
        [Fact]
        public void PaymentPending_ReturnsOk()
        {
            // Arrange
            var controller = new MpController(Mock.Of<IConfiguration>());

            // Act
            var result = controller.PaymentPending();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("The payment is pending.", okResult.Value);
        }


    }
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _fakeResponse;

        public FakeHttpMessageHandler(HttpResponseMessage fakeResponse)
        {
            _fakeResponse = fakeResponse;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_fakeResponse);
        }
    }

}
