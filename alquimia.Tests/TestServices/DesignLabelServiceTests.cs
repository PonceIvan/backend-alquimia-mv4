using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alquimia.Services.Models;
using alquimia.Services;
using Xunit;
using alquimia.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Tests.TestServices
{
    public class DesignLabelServiceTests
    {
        [Fact]
        public void CreatePdfDesign_ShouldReturnPdfByteArray_WhenValidDataIsProvided()
        {
            // Arrange
            var dto = new DesignDTO
            {
                Text = "Texto de prueba",
                Typography = "Arial",
                TextColor = "Negro",
                Shape = "Rectangular",
                LabelColor = "Rojo",
                Volume = 100,
                Image = "" 
            };

            // Act
            var result = DesignLabelService.CreatePdfDesign(dto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Length > 0); 
        }

        [Fact]
        public void CreatePdfDesign_ShouldReturnPdfByteArray_WhenInvalidImageIsProvided()
        {
            // Arrange
            var dto = new DesignDTO
            {
                Text = "Texto de prueba",
                Typography = "Arial",
                TextColor = "Negro",
                Shape = "Rectangular",
                LabelColor = "Rojo",
                Volume = 100,
                Image = "invalidBase64String" 
            };

            // Act
            var result = DesignLabelService.CreatePdfDesign(dto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Length > 0); 
        }

        [Fact]
        public async Task SaveDesignAsync_ShouldSaveDesign_WhenValidDataIsProvided()
        {
            // Arrange
            var dto = new DesignDTO
            {
                Text = "Texto de prueba",
                Typography = "Arial",
                TextColor = "Negro",
                Shape = "Rectangular",
                LabelColor = "Rojo",
                Volume = 100,
                Image = "" 
            };

            var options = new DbContextOptionsBuilder<AlquimiaDbContext>()
                .UseInMemoryDatabase(databaseName: "AlquimiaTestDb")
                .Options;

            var context = new AlquimiaDbContext(options);
            var service = new DesignLabelService(context);

            // Act
            var result = await service.SaveDesignAsync(dto);

            // Assert
            var design = await context.Set<Design>().FindAsync(result);
            Assert.NotNull(design);
            Assert.Equal(dto.Text, design.Text);
            Assert.Equal(dto.Volume, design.Volume);
            Assert.Equal(dto.LabelColor, design.LabelColor);
        }

        [Fact]
        public async Task SaveDesignAsync_ShouldReturnCorrectId_WhenDesignIsSaved()
        {
            // Arrange
            var dto = new DesignDTO
            {
                Text = "Texto de prueba",
                Typography = "Arial",
                TextColor = "Negro",
                Shape = "Rectangular",
                LabelColor = "Rojo",
                Volume = 100,
                Image = "" 
            };

            var options = new DbContextOptionsBuilder<AlquimiaDbContext>()
                .UseInMemoryDatabase(databaseName: "AlquimiaTestDb")
                .Options;

            var context = new AlquimiaDbContext(options);
            var service = new DesignLabelService(context);

            // Act
            var result = await service.SaveDesignAsync(dto);

            // Assert
            var design = await context.Set<Design>().FindAsync(result);
            Assert.NotNull(design);
            Assert.Equal(result, design.Id);
        }

        

        [Fact]
        public void CreatePdfDesign_ShouldReturnValidPdfByteArray_WhenDataIsProvided()
        {
            // Arrange
            var dto = new DesignDTO
            {
                Text = "Texto de prueba",
                Typography = "Arial",
                TextColor = "Negro",
                Shape = "Rectangular",
                LabelColor = "Rojo",
                Volume = 100,
                Image = "" 
            };

            // Act
            var result = DesignLabelService.CreatePdfDesign(dto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Length > 0); 
                                            
        }

        [Fact]
        public void CreatePdfDesign_ShouldNotThrowException_WhenImageIsEmpty()
        {
            // Arrange
            var dto = new DesignDTO
            {
                Text = "Texto de prueba",
                Typography = "Arial",
                TextColor = "Negro",
                Shape = "Rectangular",
                LabelColor = "Rojo",
                Volume = 100,
                Image = "" 
            };

            // Act & Assert
            var result = DesignLabelService.CreatePdfDesign(dto);
            Assert.NotNull(result);
            Assert.True(result.Length > 0); 
        }

        [Fact]
        public async Task SaveDesignAsync_ShouldHandleInvalidDataGracefully()
        {
            // Arrange
            var dto = new DesignDTO
            {
                Text = "null", 
                Typography = "Arial",
                TextColor = "Negro",
                Shape = "Rectangular",
                LabelColor = "Rojo",
                Volume = 100,
                Image = "" 
            };

            var options = new DbContextOptionsBuilder<AlquimiaDbContext>()
                .UseInMemoryDatabase(databaseName: "AlquimiaTestDb")
                .Options;

            var context = new AlquimiaDbContext(options);
            var service = new DesignLabelService(context);

            // Act & Assert
            var result = await service.SaveDesignAsync(dto);
            Assert.NotEqual(0, result);
        }

        


        [Fact]
        public void CreatePdfDesign_ShouldHandleSpecialCharactersGracefully()
        {
            // Arrange
            var dto = new DesignDTO
            {
                Text = "Texto con caracteres especiales: ñ, á, ü, &",
                Typography = "Arial",
                TextColor = "Negro",
                Shape = "Rectangular",
                LabelColor = "Rojo",
                Volume = 100,
                Image = "" 
            };

            // Act
            var result = DesignLabelService.CreatePdfDesign(dto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Length > 0); 
        }

    }
}
