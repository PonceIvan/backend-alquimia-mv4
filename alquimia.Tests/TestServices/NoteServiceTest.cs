using alquimia.Data.Entities;
using alquimia.Services;
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
            await CleanDatabaseAsync();

            // Arrange
            var pyramid = new OlfactoryPyramid { Sector = "Corazón", Duracion = new TimeOnly(1, 0) };
            var family = new OlfactoryFamily { Nombre = "Floral", Description = "Una familia de flores" };
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
            var result = await _noteService.GetNotesGroupedByFamilyAsync("Corazón");

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
            await CleanDatabaseAsync();

            // Arrange
            var pyramid = new OlfactoryPyramid { Sector = "Salida", Duracion = new TimeOnly(1, 0) };
            var family = new OlfactoryFamily { Nombre = "Cítrica", Description = "Naranjas y esas" };
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
            var result = await _noteService.GetNotesGroupedByFamilyAsync("Salida");

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
            await CleanDatabaseAsync();

            // Arrange
            var pyramid = new OlfactoryPyramid { Sector = "Fondo", Duracion = new TimeOnly(2, 0) };
            var family = new OlfactoryFamily { Nombre = "Amaderado", Description = "Tronquitos si" };
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
            var result = await _noteService.GetNotesGroupedByFamilyAsync("Fondo");

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
            await CleanDatabaseAsync();

            // Arrange
            var pyramid = new OlfactoryPyramid { Sector = "Fondo", Duracion = new TimeOnly(2, 0) };
            var compatiblePyramid = new OlfactoryPyramid { Sector = "Corazón", Duracion = new TimeOnly(1, 0) };

            var family1 = new OlfactoryFamily { Nombre = "Amaderado", Description = "Mas madera" };
            var family2 = new OlfactoryFamily { Nombre = "Floral", Description = "Flores bonitas" };

            _context.OlfactoryFamilies.AddRange(family1, family2);
            _context.OlfactoryPyramids.AddRange(pyramid, compatiblePyramid);
            await _context.SaveChangesAsync();

            var note1 = new Note { Name = "Sándalo", OlfactoryPyramid = pyramid, OlfactoryFamily = family1, Description = "Madera" };
            var note2 = new Note { Name = "Jazmín", OlfactoryPyramid = compatiblePyramid, OlfactoryFamily = family2, Description = "Flor" };

            _context.Notes.Add(note1);
            _context.Notes.Add(note2);
            await _context.SaveChangesAsync();

            var compatibility = new FamilyCompatibility
            {
                FamiliaMenor = Math.Min(family1.Id, family2.Id),
                FamiliaMayor = Math.Max(family1.Id, family2.Id),
                GradoDeCompatibilidad = 85
            };
            _context.FamilyCompatibilities.Add(compatibility);
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
            await CleanDatabaseAsync();

            // Arrange
            var pyramid = new OlfactoryPyramid { Sector = "Salida", Duracion = new TimeOnly(1, 0) };
            var family = new OlfactoryFamily { Nombre = "Frutal", Description = "Que fruta noble la papa" };
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
                _noteService.GetNoteInfoAsync(999));

            Assert.Equal("The given key was not present in the dictionary.", exception.Message);
        }
        private async Task CleanDatabaseAsync()
        {
            _context.Notes.RemoveRange(_context.Notes);
            _context.OlfactoryPyramids.RemoveRange(_context.OlfactoryPyramids);
            _context.OlfactoryFamilies.RemoveRange(_context.OlfactoryFamilies);
            await _context.SaveChangesAsync();
        }

    }
}
