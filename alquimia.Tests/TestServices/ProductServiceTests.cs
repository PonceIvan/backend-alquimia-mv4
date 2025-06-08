using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alquimia.Data.Entities;
using alquimia.Services.Interfaces;
using alquimia.Services.Models;
using alquimia.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Identity;

namespace alquimia.Tests.TestServices
{
    public class ProductServiceTests
    {
        private readonly AlquimiaDbContext _context;
        private readonly ProductService _productService;
        private readonly Mock<IFormulaService> _formulaServiceMock;
        private readonly Mock<UserManager<User>> _userManagerMock;
        public ProductServiceTests()
        {
            var options = new DbContextOptionsBuilder<AlquimiaDbContext>()
                .UseInMemoryDatabase(databaseName: "AlquimiaTestDb")
                .Options;

            _context = new AlquimiaDbContext(options);
            _userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            var mockFormulaService = new Mock<IFormulaService>();
            _productService = new ProductService(_context, mockFormulaService.Object);
        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
        //[Fact]
        //public async Task GetProductsByProviderAsync_ShouldReturnProducts_WhenProviderHasProducts()
        //{
        //    var providerId = 5;
        //    var product = new Product
        //    {
        //        Id = 21,
        //        Name = "Perfume A",
        //        Description = "Perfume de prueba",
        //        IdProveedor = providerId,
        //        ProductVariants = new List<ProductVariant>
        //{
        //    new ProductVariant { Id = 1, Volume = 100, Unit = "ml", Price = 50.0M, Stock = 10, IsHypoallergenic = true }
        //}
        //    };

        //    _context.Products.Add(product);
        //    await _context.SaveChangesAsync();

        //    var result = await _productService.GetProductsByProviderAsync(providerId);

        //    Assert.Single(result);
        //    Assert.Equal("Perfume A", result[0].Name);
        //}

        [Fact]
        public async Task CreateProductAsync_ShouldReturnProduct_WhenProductIsCreated()
        {
            // Arrange
            var createProductDTO = new CreateProductoDTO
            {
                Name = "Producto Nuevo",
                Description = "Descripción Producto Nuevo",
                TipoProductoDescription = "Tipo 1"
            };

            var productType = new ProductType { Description = "Tipo 1" };
            _context.ProductTypes.Add(productType);
            await _context.SaveChangesAsync();

            // Act
            var result = await _productService.CreateProductAsync(createProductDTO, 1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createProductDTO.Name, result.Name);
            Assert.Equal(createProductDTO.Description, result.Description);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldReturnTrue_WhenProductIsDeleted()
        {
            // Arrange
            var product = new Product { Name = "Producto A", Description = "Descripción A", IdProveedor = 1 };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Act
            var result = await _productService.DeleteProductAsync(product.Id, 1);

            // Assert
            Assert.True(result);
            Assert.Null(_context.Products.Find(product.Id));
        }

        //[Fact]
        //public async Task GetProductByIdAsync_ShouldReturnProduct_WhenProductExists()
        //{
        //    var providerId = 5;
        //    var product = new Product
        //    {
        //        Id = 10,
        //        Name = "Perfume A",
        //        Description = "Perfume de prueba",
        //        IdProveedor = providerId
        //    };

        //    _context.Products.Add(product);
        //    await _context.SaveChangesAsync();

        //    var result = await _productService.GetProductByIdAsync(10, providerId);

        //    Assert.NotNull(result);
        //    Assert.Equal("Perfume A", result.Name);
        //}

        [Fact]
        public async Task GetProductByIdAsync_ShouldThrowException_WhenProductNotFound()
        {
            var result = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _productService.GetProductByIdAsync(999, 1));

            Assert.Equal("Producto no encontrado", result.Message);
        }

        [Fact]
        public async Task CreateProductAsync_ShouldCreateProduct_WhenValidDataIsProvided()
        {
            var dto = new CreateProductoDTO
            {
                Name = "Perfume Nuevo",
                Description = "Nuevo perfume",
                TipoProductoDescription = "Perfumes",
                Variants = new List<CreateProductVariantDTO>
        {
            new CreateProductVariantDTO { Volume = 100, Unit = "ml", Price = 50.0M, Stock = 10, IsHypoallergenic = true }
        }
            };

            _context.ProductTypes.Add(new ProductType { Description = "Perfumes" });
            await _context.SaveChangesAsync();

            var result = await _productService.CreateProductAsync(dto, 1);

            Assert.NotNull(result);
            Assert.Equal("Perfume Nuevo", result.Name);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldReturnTrue_WhenProductIsDeletedIsTrue()
        {
            var providerId = 4;
            var product = new Product
            {
                Id = 1,
                Name = "Perfume A",
                Description = "Perfume de prueba",
                IdProveedor = providerId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var result = await _productService.DeleteProductAsync(1, providerId);

            Assert.True(result);
        }
        [Fact]
        public async Task DeleteProductAsync_ShouldReturnFalse_WhenProductNotFound()
        {
            var result = await _productService.DeleteProductAsync(999, 1);

            Assert.False(result);
        }

        //[Fact]
        //public async Task UpdateProductAsync_ShouldUpdateProduct_WhenValidDataIsProvided()
        //{
        //    var providerId = 5;
        //    var product = new Product
        //    {
        //        Id = 10,
        //        Name = "Perfume A",
        //        Description = "Perfume de prueba",
        //        IdProveedor = providerId
        //    };

        //    _context.Products.Add(product);
        //    await _context.SaveChangesAsync();

        //    var dto = new UpdateProductoDTO { Name = "Perfume B" };
        //    var result = await _productService.UpdateProductAsync(1, dto, providerId);

        //    Assert.Equal("Perfume B", result.Name);
        //}

    }
}
