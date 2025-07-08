using alquimia.Api.Controllers;
using alquimia.Services.Interfaces;
using alquimia.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Xunit;

namespace alquimia.Tests.TestControllers
{
    public class ProviderControllerTests
    {
        private Mock<IProductService> _mockProductService;
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private Mock<IMercadoLibreService> _mockMeliService;
        private ProviderController _controller;

        public ProviderControllerTests()
        {
            _mockProductService = new Mock<IProductService>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockMeliService = new Mock<IMercadoLibreService>();


            var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, "1")
        };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            _mockHttpContextAccessor.Setup(x => x.HttpContext.User).Returns(principal);

            _controller = new ProviderController(
                _mockProductService.Object,
                _mockHttpContextAccessor.Object,

                null
            );
        }



        [Fact]
        public async Task GetProducts_ReturnsOkResult_WhenProviderHasProducts()
        {
            var products = new List<ProductDTO>
        {
            new ProductDTO { Id = 1, Name = "Producto 1", Description = "Descripción 1", ProductType = "Tipo 1" }
        };
            _mockProductService.Setup(service => service.GetProductsByProviderAsync(It.IsAny<int>())).ReturnsAsync(products);

            var result = await _controller.GetProducts();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<ProductDTO>>(okResult.Value);
            Assert.Equal(1, returnValue.Count);
        }

        [Fact]
        public async Task CreateProduct_ReturnsOkResult_WhenProductIsCreatedSuccessfully()
        {
            var dto = new CreateProductoDTO { Name = "Nuevo Producto", Description = "Descripción nueva", TipoProductoDescription = "Tipo 1" };
            var productCreated = new ProductDTO { Id = 1, Name = "Nuevo Producto", Description = "Descripción nueva", ProductType = "Tipo 1" };
            _mockProductService.Setup(service => service.CreateProductAsync(dto, It.IsAny<int>())).ReturnsAsync(productCreated);

            var result = await _controller.CreateProduct(1, dto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ProductDTO>(okResult.Value);
            Assert.Equal(productCreated.Name, returnValue.Name);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNoContent_WhenProductIsDeletedSuccessfully()
        {
            _mockProductService.Setup(service => service.DeleteProductAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);

            var result = await _controller.DeleteProduct(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsOkResult_WhenProductIsUpdatedSuccessfully()
        {
            var dto = new UpdateProductoDTO { Name = "Producto Actualizado", Description = "Descripción actualizada" };
            var updatedProduct = new ProductDTO { Id = 1, Name = "Producto Actualizado", Description = "Descripción actualizada", ProductType = "Tipo 1" };
            _mockProductService.Setup(service => service.UpdateProductAsync(It.IsAny<int>(), dto, It.IsAny<int>())).ReturnsAsync(updatedProduct);

            var result = await _controller.UpdateProduct(1, dto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ProductDTO>(okResult.Value);
            Assert.Equal(updatedProduct.Name, returnValue.Name);
        }

        [Fact]
        public async Task CreateProduct_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("Name", "Required");

            var dto = new CreateProductoDTO();

            var result = await _controller.CreateProduct(1, dto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_ProductNotFound_ReturnsNotFound()
        {
            _mockProductService.Setup(s => s.DeleteProductAsync(99, 1))
                .ReturnsAsync(false);

            var result = await _controller.DeleteProduct(99);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains("no encontrado", notFoundResult.Value.ToString());
        }

        [Fact]
        public async Task DeleteProduct_ProductDeleted_ReturnsNoContent()
        {
            _mockProductService.Setup(s => s.DeleteProductAsync(1, 1))
                .ReturnsAsync(true);

            var result = await _controller.DeleteProduct(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task SyncMercadoLibreProducts_ReturnsNoContent()
        {
            var dto = new MercadoLibreSyncDTO { AccessToken = "abc" };

            var result = await _controller.SyncMercadoLibreProducts(dto);

            Assert.IsType<NoContentResult>(result);
            _mockMeliService.Verify(m => m.SyncProductsAsync(1, "abc"), Times.Once);
        }
    }
}
