using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alquimia.Api.Controllers;
using alquimia.Services.Interfaces;
using alquimia.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace alquimia.Tests.TestControllers
{
    public class AdminControllerTests
    {
        private readonly Mock<IAdminService> _serviceMock;
        private readonly AdminController _controller;

        public AdminControllerTests()
        {
            _serviceMock = new Mock<IAdminService>();
            _controller = new AdminController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetOnlyProviders_ReturnsOkWithList()
        {
            _serviceMock.Setup(s => s.GetPendingAndApprovedProvidersAsync())
                .ReturnsAsync(new List<ProviderDTO> { new ProviderDTO { Id = 1, Nombre = "A", Email = "a@mail.com", EsAprobado = true } });

            var result = await _controller.GetOnlyProviders();

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<ProviderDTO>>(ok.Value);
        }

        [Fact]
        public async Task GetOnlyProviders_ReturnsOkWithEmptyList()
        {
            _serviceMock.Setup(s => s.GetPendingAndApprovedProvidersAsync()).ReturnsAsync(new List<ProviderDTO>());

            var result = await _controller.GetOnlyProviders();

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Empty((List<ProviderDTO>)ok.Value!);
        }

        [Fact]
        public async Task GetProviderById_ReturnsOkIfFound()
        {
            _serviceMock.Setup(s => s.GetPendingOrApprovedProviderByIdAsync(1))
                .ReturnsAsync(new ProviderDTO { Id = 1, Nombre = "B", Email = "b@mail.com", EsAprobado = true });

            var result = await _controller.GetProviderById(1);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<ProviderDTO>(ok.Value);
        }

        [Fact]
        public async Task GetProviderById_ReturnsNotFoundIfNull()
        {
            _serviceMock.Setup(s => s.GetPendingOrApprovedProviderByIdAsync(99))
                .ReturnsAsync((ProviderDTO)null!);

            var result = await _controller.GetProviderById(99);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task ApproveProvider_ReturnsOkIfApproved()
        {
            _serviceMock.Setup(s => s.ApprovePendingProviderAsync(1)).ReturnsAsync(true);

            var result = await _controller.ApproveProvider(1);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Proveedor aprobado correctamente.", ok.Value);
        }

        [Fact]
        public async Task ApproveProvider_ReturnsBadRequestIfFails()
        {
            _serviceMock.Setup(s => s.ApprovePendingProviderAsync(1)).ReturnsAsync(false);

            var result = await _controller.ApproveProvider(1);

            var bad = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("No se pudo aprobar al proveedor.", bad.Value);
        }

        [Fact]
        public async Task DeactivateProvider_ReturnsOkIfSuccess()
        {
            _serviceMock.Setup(s => s.DeactivateProviderAsync(1)).ReturnsAsync(true);

            var result = await _controller.DeactivateProvider(1);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Proveedor dado de baja correctamente.", ok.Value);
        }

        [Fact]
        public async Task DeactivateProvider_ReturnsBadRequestIfFails()
        {
            _serviceMock.Setup(s => s.DeactivateProviderAsync(1)).ReturnsAsync(false);

            var result = await _controller.DeactivateProvider(1);

            var bad = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("No se pudo desactivar al proveedor.", bad.Value);
        }
    }
}
