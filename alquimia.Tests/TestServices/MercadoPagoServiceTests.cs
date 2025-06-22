using alquimia.Data.Entities;
using alquimia.Services;
using alquimia.Services.Interfaces;
using alquimia.Services.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Xunit;

public class MercadoPagoServiceTests
{
    [Fact]
    public async Task GeneratePaymentLinkAsync_ReturnsInitPointUrl()
    {
        // Arrange
        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c["MercadoPago:AccessToken"]).Returns("TEST-TOKEN");
        configMock.Setup(c => c["MercadoPago:Currency"]).Returns("ARS");
        configMock.Setup(c => c["MercadoPago:SuccessUrl"]).Returns("https://example.com/success");
        configMock.Setup(c => c["MercadoPago:FailureUrl"]).Returns("https://example.com/failure");
        configMock.Setup(c => c["MercadoPago:PendingUrl"]).Returns("https://example.com/pending");

        var variant = new ProductVariant
        {
            Id = 123,
            Price = 999,
            Product = new Product
            {
                Name = "Bergamota",
                Description = "Perfume cítrico de calidad"
            }
        };

        var productServiceMock = new Mock<IProductService>();
        productServiceMock.Setup(p => p.GetVariantEntityAsync(123))
                          .ReturnsAsync(variant);

        var responseJson = JsonSerializer.Serialize(new { init_point = "https://mp.com/init123" });
        var message = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(responseJson, Encoding.UTF8, "application/json")
        };

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(message);

        var httpClient = new HttpClient(handlerMock.Object);
        var httpFactoryMock = new Mock<IHttpClientFactory>();
        httpFactoryMock.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var service = new MercadoPagoService(
            configMock.Object,
            productServiceMock.Object,
            httpFactoryMock.Object
        );

        var dto = new CreatePaymentPreferenceDTO
        {
            ProductVariantId = 123,
            Quantity = 2
        };

        // Act
        var result = await service.GeneratePaymentLinkAsync(dto);

        // Assert
        Assert.Equal("https://mp.com/init123", result);
    }
}
