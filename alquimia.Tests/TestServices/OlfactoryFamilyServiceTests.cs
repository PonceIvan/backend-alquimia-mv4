using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alquimia.Data.Entities;
using alquimia.Services;
using alquimia.Services.Models;
using alquimia.Tests.TestUtils;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace alquimia.Tests.TestServices
{
    public class OlfactoryFamilyServiceTests
    {
        private readonly Mock<AlquimiaDbContext> _mockContext;
        private readonly OlfactoryFamilyService _olfactoryFamilyService;

        public OlfactoryFamilyServiceTests()
        {
            _mockContext = new Mock<AlquimiaDbContext>();
            _olfactoryFamilyService = new OlfactoryFamilyService(_mockContext.Object);
        }
        
        
        [Fact]
        public async Task GetOlfactoryFamilyInfoAsync_ShouldReturnOlfactoryFamily_WhenUsingInMemoryDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AlquimiaDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDb")
                .Options;

            using var context = new AlquimiaDbContext(options);
            var service = new OlfactoryFamilyService(context);

            var family = new OlfactoryFamily
            {
                Nombre = "Amaderado",
                Description = "Familia con notas amaderadas.",
                Image1 = "amaderado_image.jpg"
            };

            context.OlfactoryFamilies.Add(family);
            await context.SaveChangesAsync();

            // Act
            var result = await service.GetOlfactoryFamilyInfoAsync(family.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(family.Id, result.Id);
            Assert.Equal("Amaderado", result.Name);
            Assert.Equal("Familia con notas amaderadas.", result.Description);
            Assert.Equal("amaderado_image.jpg", result.Image1);
        }

        

        [Fact]
        public async Task CreateOlfactoryFamily_ShouldAddFamily_WhenValidFamilyIsProvided()
        {
            // Arrange
            var family = new OlfactoryFamilyDTO
            {
                Name = "Floral",
                Description = "Familia con notas florales.",
                Image1 = "floral_image.jpg"
            };

            var options = new DbContextOptionsBuilder<AlquimiaDbContext>()
                .UseInMemoryDatabase("InMemoryCreateDb")
                .Options;

            using var context = new AlquimiaDbContext(options);
            var service = new OlfactoryFamilyService(context);

            // Act
            var familyId = await service.CreateOlfactoryFamilyAsync(family);

            // Assert
            var createdFamily = await context.OlfactoryFamilies.FindAsync(familyId);
            Assert.NotNull(createdFamily);
            Assert.Equal(family.Name, createdFamily.Nombre);
            Assert.Equal(family.Description, createdFamily.Description);
            Assert.Equal(family.Image1, createdFamily.Image1);
        }

        [Fact]
        public async Task UpdateOlfactoryFamily_ShouldUpdateFamily_WhenFamilyExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AlquimiaDbContext>()
                .UseInMemoryDatabase("InMemoryUpdateDb")
                .Options;

            using var context = new AlquimiaDbContext(options);
            var service = new OlfactoryFamilyService(context);

            var family = new OlfactoryFamily
            {
                Nombre = "Frutal",
                Description = "Familia con notas frutales.",
                Image1 = "frutal_image.jpg"
            };

            context.OlfactoryFamilies.Add(family);
            await context.SaveChangesAsync();

            // Act
            family.Nombre = "Frutal Modificada";
            family.Description = "Familia con notas frutales modificadas.";
            family.Image1 = "frutal_modified_image.jpg";
            await service.UpdateOlfactoryFamilyAsync(family);

            // Assert
            var updatedFamily = await context.OlfactoryFamilies.FindAsync(family.Id);
            Assert.Equal("Frutal Modificada", updatedFamily.Nombre);
            Assert.Equal("Familia con notas frutales modificadas.", updatedFamily.Description);
            Assert.Equal("frutal_modified_image.jpg", updatedFamily.Image1);
        }
        //[Fact]
        //public async Task CreateOlfactoryFamily_ShouldThrowBadRequest_WhenDataIsInvalid()
        //{
        //    // Arrange
        //    var familyDto = new OlfactoryFamilyDTO
        //    {
        //        Name = "", 
        //        Description = "Familia con notas florales.",
        //        Image1 = "floral_image.jpg"
        //    };

        //    var options = new DbContextOptionsBuilder<AlquimiaDbContext>()
        //        .UseInMemoryDatabase("InMemoryCreateDb")
        //        .Options;

        //    using var context = new AlquimiaDbContext(options);
        //    var service = new OlfactoryFamilyService(context);

        //    // Act & Assert
        //    var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
        //        service.CreateOlfactoryFamilyAsync(familyDto));

        //    Assert.Equal("Value cannot be null. (Parameter 'familyDto.Name')", exception.Message);
        //}

        //[Fact]
        //public async Task UpdateOlfactoryFamily_ShouldThrowNotFound_WhenFamilyDoesNotExist()
        //{
        //    // Arrange
        //    var family = new OlfactoryFamily
        //    {
        //        Nombre = "Frutal",
        //        Description = "Familia con notas frutales.",
        //        Image1 = "frutal_image.jpg"
        //    };

        //    var options = new DbContextOptionsBuilder<AlquimiaDbContext>()
        //        .UseInMemoryDatabase("InMemoryUpdateDb")
        //        .Options;

        //    using var context = new AlquimiaDbContext(options);
        //    var service = new OlfactoryFamilyService(context);

        //    // Act & Assert
        //    await Assert.ThrowsAsync<KeyNotFoundException>(() => service.UpdateOlfactoryFamilyAsync(family));
        //}

        [Fact]
        public async Task CreateOlfactoryFamily_ShouldHandleNullImage_WhenImageIsNotProvided()
        {
            // Arrange
            var familyDto = new OlfactoryFamilyDTO
            {
                Name = "Cítrica",
                Description = "Familia con notas cítricas.",
                Image1 = null  // Sin imagen
            };

            var options = new DbContextOptionsBuilder<AlquimiaDbContext>()
                .UseInMemoryDatabase("InMemoryCreateWithNullImageDb")
                .Options;

            using var context = new AlquimiaDbContext(options);
            var service = new OlfactoryFamilyService(context);

            // Act
            var familyId = await service.CreateOlfactoryFamilyAsync(familyDto);

            // Assert
            var createdFamily = await context.OlfactoryFamilies.FindAsync(familyId);
            Assert.NotNull(createdFamily);
            Assert.Equal(familyDto.Name, createdFamily.Nombre);
            Assert.Equal(familyDto.Description, createdFamily.Description);
            Assert.Null(createdFamily.Image1);  // Aseguramos que la imagen es nula
        }

        //[Fact]
        //public async Task CreateOlfactoryFamily_ShouldReturnBadRequest_WhenArgumentExceptionOccurs()
        //{
        //    // Arrange
        //    var familyDto = new OlfactoryFamilyDTO
        //    {
        //        Name = "", // Nombre vacío para generar una excepción de argumento
        //        Description = "Familia con notas cítricas.",
        //        Image1 = "citrus_image.jpg"
        //    };

        //    var options = new DbContextOptionsBuilder<AlquimiaDbContext>()
        //        .UseInMemoryDatabase("InMemoryCreateWithBadRequest")
        //        .Options;

        //    using var context = new AlquimiaDbContext(options);
        //    var service = new OlfactoryFamilyService(context);

        //    // Act & Assert
        //    var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
        //        service.CreateOlfactoryFamilyAsync(familyDto));

        //    Assert.Equal("Parámetros inválidos en la solicitud.", exception.Message);
        //}

        //[Fact]
        //public async Task CreateOlfactoryFamily_ShouldReturnUnauthorized_WhenUnauthorizedAccessExceptionOccurs()
        //{
        //    // Arrange
        //    var familyDto = new OlfactoryFamilyDTO
        //    {
        //        Name = "Especiada",
        //        Description = "Familia con notas especiadas.",
        //        Image1 = "spicy_image.jpg"
        //    };

        //    var options = new DbContextOptionsBuilder<AlquimiaDbContext>()
        //        .UseInMemoryDatabase("InMemoryCreateUnauthorized")
        //        .Options;

        //    using var context = new AlquimiaDbContext(options);
        //    var service = new OlfactoryFamilyService(context);

        //    // Act & Assert
        //    var exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
        //        service.CreateOlfactoryFamilyAsync(familyDto));

        //    Assert.Equal("Acceso no autorizado.", exception.Message);
        //}

        





    }
}
