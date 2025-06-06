using alquimia.Api.Controllers;
using alquimia.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace alquimia.Tests.TestControllers
{
    public class CreatorControllerTests
    {
        private readonly Mock<INoteService> _mockNoteService;
        private readonly Mock<IFormulaService> _mockFormulaService;
        private readonly Mock<IOlfactoryFamilyService> _mockOlfactoryFamilyService;
        private readonly Mock<IDesignLabelService> _mockDesignLabelService;

        private readonly CreatorController _controller;

        public CreatorControllerTests()
        {
            _mockNoteService = new Mock<INoteService>();
            _mockFormulaService = new Mock<IFormulaService>();
            _mockOlfactoryFamilyService = new Mock<IOlfactoryFamilyService>();
            _mockDesignLabelService = new Mock<IDesignLabelService>();

            _controller = new CreatorController(
                _mockNoteService.Object,
                _mockFormulaService.Object,
                _mockOlfactoryFamilyService.Object,
                _mockDesignLabelService.Object
                );
        }

        [Fact]
        public void Create_ReturnsOkWithExpectedMessage()
        {
            var result = _controller.Create();
            string expectedMessage = "Ruta activa";
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedMessage, okResult.Value);
        }
    }
}
