using alquimia.Api.Controllers;
using alquimia.Services.Interfaces;
using alquimia.Services.Models;
using alquimia.Tests.TestUtils;
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

        [Fact]
        public void Start_RetursnOkWithExpectedMessage()
        {
            var result = _controller.Start();
            string expectedMessage = "Ruta activa";
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedMessage, okResult.Value);
        }

        [Fact]
        public async Task GetBaseNotes_ReturnsOKWithAListOfBaseNotes()
        {
            _mockNoteService.Setup(s => s.GetBaseNotesGroupedByFamilyAsync()).ReturnsAsync(MockGroupedNotesDataDTO.GetBaseNotesGrouped());

            var result = await _controller.GetBaseNotes();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var data = Assert.IsType<List<NotesGroupedByFamilyDTO>>(okResult.Value);

            Assert.Equal(2, data.Count);
            Assert.Contains(data, g => g.Family == "Terroso");
            Assert.Contains(data, g => g.Family == "Amaderado");
        }

        [Fact]
        public async Task GetTopNotes_ReturnsOKWithAListOfBaseNotes()
        {
            _mockNoteService.Setup(s => s.GetBaseNotesGroupedByFamilyAsync()).ReturnsAsync(MockGroupedNotesDataDTO.GetTopNotesGrouped());

            var result = await _controller.GetBaseNotes();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var data = Assert.IsType<List<NotesGroupedByFamilyDTO>>(okResult.Value);

            Assert.Equal(2, data.Count);
            Assert.Contains(data, g => g.Family == "Cítrico");
            Assert.Contains(data, g => g.Family == "Alcanforado");
        }

    }
}
