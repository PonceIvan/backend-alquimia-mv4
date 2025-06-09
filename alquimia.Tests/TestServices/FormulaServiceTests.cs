using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alquimia.Data.Entities;
using alquimia.Services;
using alquimia.Services.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace alquimia.Tests.TestServices
{
    public class FormulaServiceTests
    {
        private readonly AlquimiaDbContext _context;
        private readonly FormulaService _formulaService;

        public FormulaServiceTests()
        {
            var options = new DbContextOptionsBuilder<AlquimiaDbContext>()
                .UseInMemoryDatabase("AlquimiaTestDb")
                .Options;

            _context = new AlquimiaDbContext(options);
            _formulaService = new FormulaService(_context);
        }

        [Fact]
        public async Task GetIntensitiesAsync_ShouldReturnIntensities()
        {
            // Arrange
            var intensities = new[]
            {
                new Intensity { Id = 1, Nombre = "Baja", Description = "Baja intensidad", Category = "Baja" },
                new Intensity { Id = 2, Nombre = "Media", Description = "Media intensidad", Category = "Media" },
                new Intensity { Id = 3, Nombre = "Alta", Description = "Alta intensidad", Category = "Alta" }
            };

            _context.Intensities.AddRange(intensities);
            await _context.SaveChangesAsync();

            // Act
            var result = await _formulaService.GetIntensitiesAsync();

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Contains(result, i => i.Name == "Baja");
            Assert.Contains(result, i => i.Name == "Media");
            Assert.Contains(result, i => i.Name == "Alta");
        }

    //    [Fact]
    //    public async Task SaveAsync_ShouldSaveFormulaAndNotes()
    //    {
    //        // Arrange
    //        var dto = new POSTFormulaDTO
    //        {
    //            IntensityId = 1,
    //            CreatorId = 1,
    //            TopNotes = new POSTFormulaNoteDTO { Note1 = new NoteDTO { Id = 1 }, Note2 = new NoteDTO { Id = 2 } },
    //            HeartNotes = new POSTFormulaNoteDTO { Note1 = new NoteDTO { Id = 3 }, Note2 = new NoteDTO { Id = 4 } },
    //            BaseNotes = new POSTFormulaNoteDTO { Note1 = new NoteDTO { Id = 5 }, Note2 = new NoteDTO { Id = 6 } }
    //        };

    //        var user = new User { Id = 1, Name = "Test User" };
    //        _context.Users.Add(user);
    //        await _context.SaveChangesAsync();

    //        // Mock formula and notes
    //        var notes = new[]
    //        {
    //    new Note { Id = 1, Name = "Note 1" },
    //    new Note { Id = 2, Name = "Note 2" },
    //    new Note { Id = 3, Name = "Note 3" },
    //    new Note { Id = 4, Name = "Note 4" },
    //    new Note { Id = 5, Name = "Note 5" },
    //    new Note { Id = 6, Name = "Note 6" }
    //};

    //        _context.Notes.AddRange(notes);
    //        await _context.SaveChangesAsync();

    //        // Act
    //        var result = await _formulaService.SaveAsync(dto);

    //        // Assert
    //        Assert.True(result > 0);  // Assuming that result is the formula ID.
    //        var savedFormula = await _context.Formulas.FindAsync(result);
    //        Assert.NotNull(savedFormula);
    //        Assert.Equal(dto.IntensityId, savedFormula.IntensidadId);
    //    }

        //[Fact]
        //public async Task GetFormulaByIdToDTOAsync_ShouldReturnCorrectFormulaDTO()
        //{
        //    // Arrange
        //    var formula = new Formula
        //    {
        //        Id = 1,
        //        IntensidadId = 2,
        //        CreadorId = 1,
        //        ConcentracionAlcohol = 70.0,
        //        ConcentracionAgua = 27.0,
        //        ConcentracionEsencia = 3.0
        //    };
        //    _context.Formulas.Add(formula);
        //    await _context.SaveChangesAsync();

        //    var formulaNote = new FormulaNote { FormulaNotaId = 1, NotaId1 = 1, NotaId2 = 2, NotaId3 = 3, NotaId4 = 4 };
        //    _context.FormulaNotes.Add(formulaNote);
        //    await _context.SaveChangesAsync();

        //    // Act
        //    var result = await _formulaService.GetFormulaByIdToDTOAsync(1);

        //    // Assert
        //    Assert.Equal(formula.Id, result.Id);
        //    Assert.Equal(70.0, result.ConcentracionAlcohol);
        //}

        [Fact]
        public void CreatePdf_ShouldGeneratePdfFile()
        {
            // Arrange
            var dto = new GETFormulaDTO
            {
                Id = 1,
                NotasSalidaIds = new GETFormulaNoteDTO(),
                NotasCorazonIds = new GETFormulaNoteDTO(),
                NotasFondoIds = new GETFormulaNoteDTO(),
                Intensity = new IntensityDTO { Name = "Baja" },
                Title = "Test Formula",
                ConcentracionAlcohol = 70.0,
                ConcentracionAgua = 27.0,
                ConcentracionEsencia = 3.0
            };

            // Act
            var result = FormulaService.CreatePdf(dto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Length > 0);
        }

        [Fact]
        public async Task UpdateTitleAsync_ShouldUpdateTitle()
        {
            // Arrange
            var formula = new Formula { Id = 1, Title = "Old Title" };
            _context.Formulas.Add(formula);
            await _context.SaveChangesAsync();

            var newTitle = "New Title";

            // Act
            await _formulaService.UpdateTitleAsync(formula, newTitle);

            // Assert
            var updatedFormula = await _context.Formulas.FindAsync(formula.Id);
            Assert.Equal(newTitle, updatedFormula?.Title);
        }

        [Fact]
        public async Task GetFormulaByIdToDTOAsync_ShouldThrowKeyNotFoundException_WhenFormulaNotFound()
        {
            // Act & Assert
            var result = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _formulaService.GetFormulaByIdToDTOAsync(999));

            Assert.Equal("The given key was not present in the dictionary.", result.Message);
        }

    //    [Fact]
    //    public async Task SaveAsync_ShouldCalculateCorrectConcentration_WhenIntensityIsHigh()
    //    {
    //        // Arrange
    //        var dto = new POSTFormulaDTO
    //        {
    //            IntensityId = 3, // Alta intensidad
    //            CreatorId = 1,
    //            TopNotes = new POSTFormulaNoteDTO { Note1 = new NoteDTO { Id = 1 }, Note2 = new NoteDTO { Id = 2 } },
    //            HeartNotes = new POSTFormulaNoteDTO { Note1 = new NoteDTO { Id = 3 }, Note2 = new NoteDTO { Id = 4 } },
    //            BaseNotes = new POSTFormulaNoteDTO { Note1 = new NoteDTO { Id = 5 }, Note2 = new NoteDTO { Id = 6 } }
    //        };

    //        var user = new User { Id = 1, Name = "Test User" };
    //        _context.Users.Add(user);
    //        await _context.SaveChangesAsync();

    //        var notes = new[]
    //        {
    //    new Note { Id = 1, Name = "Note 1" },
    //    new Note { Id = 2, Name = "Note 2" },
    //    new Note { Id = 3, Name = "Note 3" },
    //    new Note { Id = 4, Name = "Note 4" },
    //    new Note { Id = 5, Name = "Note 5" },
    //    new Note { Id = 6, Name = "Note 6" }
    //};

    //        _context.Notes.AddRange(notes);
    //        await _context.SaveChangesAsync();

    //        // Act
    //        var formulaId = await _formulaService.SaveAsync(dto);

    //        // Assert
    //        var savedFormula = await _context.Formulas.FindAsync(formulaId);
    //        Assert.NotNull(savedFormula);
    //        Assert.Equal(80.0, savedFormula.ConcentracionAlcohol);  // Verifica la concentración de alcohol
    //        Assert.Equal(2.0, savedFormula.ConcentracionAgua);     // Verifica la concentración de agua
    //        Assert.Equal(18.0, savedFormula.ConcentracionEsencia); // Verifica la concentración de esencia
    //    }

        //[Fact]
        //public async Task GetFormulaByIdToDTOAsync_ShouldReturnFormulaWithTitle()
        //{
        //    // Arrange
        //    var formula = new Formula
        //    {
        //        Id = 8,
        //        Title = "Test Formula",
        //        IntensidadId = 2,
        //        CreadorId = 1,
        //        ConcentracionAlcohol = 70.0,
        //        ConcentracionAgua = 27.0,
        //        ConcentracionEsencia = 3.0
        //    };
        //    _context.Formulas.Add(formula);
        //    await _context.SaveChangesAsync();

        //    // Act
        //    var result = await _formulaService.GetFormulaByIdToDTOAsync(8);

        //    // Assert
        //    Assert.Equal("Test Formula", result.Title);
        //    Assert.Equal(70.0, result.ConcentracionAlcohol);
        //}

        [Fact]
        public async Task GetFormulaByIdToDTOAsync_ShouldMapNotesCorrectly()
        {
            // Arrange
            var formula = new Formula
            {
                Id = 9,
                IntensidadId = 1,
                CreadorId = 1,
                ConcentracionAlcohol = 70.0,
                ConcentracionAgua = 27.0,
                ConcentracionEsencia = 3.0,
                FormulaSalida = 1,
                FormulaCorazon = 2,
                FormulaFondo = 3
            };

            _context.Formulas.Add(formula);
            _context.FormulaNotes.Add(new FormulaNote
            {
                FormulaNotaId = 1,
                NotaId1 = 1,
                NotaId2 = 2
            });
            _context.FormulaNotes.Add(new FormulaNote
            {
                FormulaNotaId = 2,
                NotaId1 = 3,
                NotaId2 = 4
            });
            _context.FormulaNotes.Add(new FormulaNote
            {
                FormulaNotaId = 3,
                NotaId1 = 5,
                NotaId2 = 6
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _formulaService.GetFormulaByIdToDTOAsync(9);

            // Assert
            Assert.Equal(1, result.Intensity.Id);
            Assert.Equal("Baja", result.Intensity.Name);
            Assert.NotNull(result.NotasSalidaIds);
            Assert.NotNull(result.NotasCorazonIds);
            Assert.NotNull(result.NotasFondoIds);
        }

        [Fact]
        public async Task GetFormulaByIdToDTOAsync_ShouldThrowKeyNotFoundException_WhenFormulaDoesNotExist()
        {
            // Act & Assert
            var result = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _formulaService.GetFormulaByIdToDTOAsync(999));

            Assert.Equal("The given key was not present in the dictionary.", result.Message);
        }

        [Fact]
        public void CreatePdf_ShouldGeneratePdfFileCorrectly()
        {
            // Arrange
            var dto = new GETFormulaDTO
            {
                Id = 1,
                NotasSalidaIds = new GETFormulaNoteDTO(),
                NotasCorazonIds = new GETFormulaNoteDTO(),
                NotasFondoIds = new GETFormulaNoteDTO(),
                Intensity = new IntensityDTO { Name = "Baja" },
                Title = "Test Formula",
                ConcentracionAlcohol = 70.0,
                ConcentracionAgua = 27.0,
                ConcentracionEsencia = 3.0
            };

            // Act
            var result = FormulaService.CreatePdf(dto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Length > 0);
        }

    }
}
