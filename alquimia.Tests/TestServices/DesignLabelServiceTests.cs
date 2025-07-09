using alquimia.Data.Entities;
using alquimia.Services;
using alquimia.Services.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace alquimia.Tests.TestServices
{
    public class DesignLabelServiceTests
    {
        [Fact]
        public void CreatePdfDesign_ShouldReturnPdfByteArray_WhenValidDataIsProvided()
        {
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

            var result = DesignLabelService.CreatePdfDesign(dto);

            Assert.NotNull(result);
            Assert.True(result.Length > 0);
        }

        [Fact]
        public void CreatePdfDesign_ShouldReturnPdfByteArray_WhenInvalidImageIsProvided()
        {
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

            var result = DesignLabelService.CreatePdfDesign(dto);

            Assert.NotNull(result);
            Assert.True(result.Length > 0);
        }

        [Fact]
        public async Task SaveDesignAsync_ShouldSaveDesign_WhenValidDataIsProvided()
        {
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

            var result = await service.SaveDesignAsync(dto);

            var design = await context.Set<Design>().FindAsync(result);
            Assert.NotNull(design);
            Assert.Equal(dto.Text, design.Text);
            Assert.Equal(dto.Volume, design.Volume);
            Assert.Equal(dto.LabelColor, design.LabelColor);
        }

        [Fact]
        public async Task SaveDesignAsync_ShouldReturnCorrectId_WhenDesignIsSaved()
        {
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

            var result = await service.SaveDesignAsync(dto);

            var design = await context.Set<Design>().FindAsync(result);
            Assert.NotNull(design);
            Assert.Equal(result, design.Id);
        }



        [Fact]
        public void CreatePdfDesign_ShouldReturnValidPdfByteArray_WhenDataIsProvided()
        {
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

            var result = DesignLabelService.CreatePdfDesign(dto);

            Assert.NotNull(result);
            Assert.True(result.Length > 0);

        }

        [Fact]
        public void CreatePdfDesign_ShouldNotThrowException_WhenImageIsEmpty()
        {
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

            var result = DesignLabelService.CreatePdfDesign(dto);
            Assert.NotNull(result);
            Assert.True(result.Length > 0);
        }

        [Fact]
        public async Task SaveDesignAsync_ShouldHandleInvalidDataGracefully()
        {
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

            var result = await service.SaveDesignAsync(dto);
            Assert.NotEqual(0, result);
        }




        [Fact]
        public void CreatePdfDesign_ShouldHandleSpecialCharactersGracefully()
        {
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

            var result = DesignLabelService.CreatePdfDesign(dto);

            Assert.NotNull(result);
            Assert.True(result.Length > 0);
        }

    }
}
