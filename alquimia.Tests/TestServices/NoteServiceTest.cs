using alquimia.Data.Entities;
using alquimia.Services;
using alquimia.Tests.TestUtils;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace alquimia.Tests.TestServices
{
    public class NoteServiceTests
    {
        private readonly AlquimiaDbContext _context;
        private readonly NoteService _noteService;

        public NoteServiceTests()
        {
            var options = new DbContextOptionsBuilder<AlquimiaDbContext>()
                .UseInMemoryDatabase("AlquimiaTestDb")
                .Options;

            _context = new AlquimiaDbContext(options);
            _noteService = new NoteService(_context);
        }

        [Fact]
        public async Task GetHeartNotesGroupedByFamilyAsync_ShouldReturnCorrectData()
        {
            // Arrange
            var pyramid = new OlfactoryPyramid { Sector = "Corazón", Duracion = new TimeOnly(1, 0) };
            var family = new OlfactoryFamily { Nombre = "Floral" , Description = "Una familia de flores"};
            var note = new Note
            {
                Name = "Jazmín",
                Description = "Fragancia floral",
                OlfactoryPyramid = pyramid,
                OlfactoryFamily = family
            };

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            // Act
            var result = await _noteService.GetHeartNotesGroupedByFamilyAsync();

            // Assert
            Assert.Single(result);
            var floralFamily = result.FirstOrDefault(g => g.Family == "Floral");
            Assert.NotNull(floralFamily);
            Assert.Single(floralFamily.Notes);
            Assert.Equal("Jazmín", floralFamily.Notes.First().Name);
        }

        [Fact]
        public async Task GetTopNotesGroupedByFamilyAsync_ShouldReturnCorrectData()
        {
            // Arrange
            var pyramid = new OlfactoryPyramid { Sector = "Salida", Duracion = new TimeOnly(1, 0) };
            var family = new OlfactoryFamily { Nombre = "Cítrica" , Description = "Naranjas y esas"};
            var note = new Note
            {
                Name = "Limón",
                Description = "Fragancia fresca",
                OlfactoryPyramid = pyramid,
                OlfactoryFamily = family
            };

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            // Act
            var result = await _noteService.GetTopNotesGroupedByFamilyAsync();

            // Assert
            Assert.Single(result);
            var citricFamily = result.FirstOrDefault(g => g.Family == "Cítrica");
            Assert.NotNull(citricFamily);
            Assert.Single(citricFamily.Notes);
            Assert.Equal("Limón", citricFamily.Notes.First().Name);
        }

        [Fact]
        public async Task GetBaseNotesGroupedByFamilyAsync_ShouldReturnCorrectData()
        {
            // Arrange
            var pyramid = new OlfactoryPyramid { Sector = "Fondo", Duracion = new TimeOnly(2, 0) };
            var family = new OlfactoryFamily { Nombre = "Amaderado" , Description ="Tronquitos si"};
            var note = new Note
            {
                Name = "Sándalo",
                Description = "Fragancia amaderada",
                OlfactoryPyramid = pyramid,
                OlfactoryFamily = family
            };

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            // Act
            var result = await _noteService.GetBaseNotesGroupedByFamilyAsync();

            // Assert
            Assert.Single(result);
            var woodyFamily = result.FirstOrDefault(g => g.Family == "Amaderado");
            Assert.NotNull(woodyFamily);
            //Assert.Single(woodyFamily.Notes);
            Assert.Equal("Sándalo", woodyFamily.Notes.First().Name);
        }
        [Fact]
        public async Task GetCompatibleNotesAsync_ShouldReturnCompatibleNotes()
        {
            // Arrange
            var pyramid = new OlfactoryPyramid { Sector = "Fondo", Duracion = new TimeOnly(2, 0) };
            var family = new OlfactoryFamily { Nombre = "Amaderado" ,Description="Mas madera"};
            var note1 = new Note
            {
                Name = "Sándalo",
                Description = "Fragancia amaderada",
                OlfactoryPyramid = pyramid,
                OlfactoryFamily = family
            };
            var note2 = new Note
            {
                Name = "Cedro",
                Description = "Fragancia amaderada",
                OlfactoryPyramid = pyramid,
                OlfactoryFamily = family
            };

            _context.Notes.AddRange(note1, note2);
            await _context.SaveChangesAsync();

            var selectedNotes = new List<int> { note1.Id };

            // Act
            var result = await _noteService.GetCompatibleNotesAsync(selectedNotes, "Corazón");

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal("Floral", result.FirstOrDefault()?.Family);
        }

        [Fact]
        public async Task GetNoteInfoAsync_ShouldReturnNoteInfo()
        {
            // Arrange
            var pyramid = new OlfactoryPyramid { Sector = "Salida", Duracion = new TimeOnly(1, 0) };
            var family = new OlfactoryFamily { Nombre = "Frutal" , Description="Que fruta noble la papa"};
            var note = new Note
            {
                Name = "Manzana",
                Description = "Fragancia frutal",
                OlfactoryPyramid = pyramid,
                OlfactoryFamily = family
            };

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            // Act
            var result = await _noteService.GetNoteInfoAsync(note.Id);

            // Assert
            Assert.Equal("Manzana", result.Name);
            Assert.Equal("Frutal", result.Family);
        }


        [Fact]
        public async Task GetNoteInfoAsync_ShouldThrowException_WhenNoteNotFound()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _noteService.GetNoteInfoAsync(999)); // Nota no existente

            Assert.Equal("The given key was not present in the dictionary.", exception.Message);
        }











    }
}
