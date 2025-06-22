using alquimia.Api.Controllers;
using alquimia.Services.Interfaces;
using alquimia.Services.Models;
using Alquimia.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

public class MpControllerTests
{
    [Fact]
    public async Task GeneratePaymentLink_ReturnsOkResultWithUrl()
    {
        // Arrange
        var dto = new CreatePaymentPreferenceDTO
        {
            ProductVariantId = 123,
            Quantity = 1,
            ExternalReference = "pedido-001"
        };

        var mockService = new Mock<IMercadoPagoService>();
        mockService
            .Setup(s => s.GeneratePaymentLinkAsync(dto))
            .ReturnsAsync("https://mp.com/init");

        var controller = new MpController(mockService.Object);

        // Act
        var result = await controller.GeneratePaymentLink(dto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);

        // ✅ Deserializamos el JSON dinámicamente
        var json = JsonSerializer.Serialize(okResult.Value);
        using var doc = JsonDocument.Parse(json);

        var url = doc.RootElement.GetProperty("url").GetString();
        Assert.Equal("https://mp.com/init", url);
    }


    [Fact]
    public async Task GeneratePaymentLink_ReturnsNotFound_WhenVariantNotFound()
    {
        // Arrange
        var dto = new CreatePaymentPreferenceDTO { ProductVariantId = 999 };

        var mockService = new Mock<IMercadoPagoService>();
        mockService
            .Setup(s => s.GeneratePaymentLinkAsync(dto))
            .ThrowsAsync(new KeyNotFoundException("No encontrado"));

        var controller = new MpController(mockService.Object);

        // Act
        var result = await controller.GeneratePaymentLink(dto);

        // Assert
        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("No encontrado", notFound.Value);
    }

    [Fact]
    public async Task GeneratePaymentLink_Returns500_OnGeneralException()
    {
        // Arrange
        var dto = new CreatePaymentPreferenceDTO { ProductVariantId = 1 };

        var mockService = new Mock<IMercadoPagoService>();
        mockService
            .Setup(s => s.GeneratePaymentLinkAsync(dto))
            .ThrowsAsync(new Exception("Error interno"));

        var controller = new MpController(mockService.Object);

        // Act
        var result = await controller.GeneratePaymentLink(dto);

        // Assert
        var serverError = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, serverError.StatusCode);
        Assert.Equal("Error interno", serverError.Value);
    }

    [Fact]
    public void PaymentSuccess_ReturnsRedirectWithParams()
    {
        var mockService = new Mock<IMercadoPagoService>();
        var controller = new MpController(mockService.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        // 🔧 Important: set scheme for URL generation
        controller.ControllerContext.HttpContext.Request.Scheme = "https";

        var result = controller.PaymentSuccess("123", "approved", "pedido-123");

        var redirect = Assert.IsType<RedirectResult>(result);
        Assert.StartsWith("https", redirect.Url);
        Assert.Contains("payment_id=123", redirect.Url);
        Assert.Contains("status=approved", redirect.Url);
        Assert.Contains("external_reference=pedido-123", redirect.Url);
    }


    [Fact]
    public void PaymentFailure_ReturnsRedirect()
    {
        var mockService = new Mock<IMercadoPagoService>();
        var controller = new MpController(mockService.Object);

        var result = controller.PaymentFailure();
        var redirect = Assert.IsType<RedirectResult>(result);
        Assert.Equal("https://pago-exitoso.onrender.com/failure.html", redirect.Url);
    }

    [Fact]
    public void PaymentPending_ReturnsRedirect()
    {
        var mockService = new Mock<IMercadoPagoService>();
        var controller = new MpController(mockService.Object);

        var result = controller.PaymentPending();
        var redirect = Assert.IsType<RedirectResult>(result);
        Assert.Equal("https://pago-exitoso.onrender.com/pending.html", redirect.Url);
    }
}
