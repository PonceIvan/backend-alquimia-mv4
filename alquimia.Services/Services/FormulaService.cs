using alquimia.Data.Data.Entities;
using alquimia.Services.Services;
using alquimia.Services.Services.Models;
using backendAlquimia.alquimia.Services.Interfaces;
using backendAlquimia.alquimia.Services.Services;
using backendAlquimia.Models;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace backendAlquimia.alquimia.Services.Services
{
    public class FormulaService : IFormulaService
    {
        private readonly AlquimiaDbContext _context;
        private FormulaConcentration FormulaConcentration { get; set; }
        public FormulaService(AlquimiaDbContext context)
        {
            _context = context;
            
        }

        public async Task<List<IntensitiesDTO>> GetIntensitiesAsync()
        {
            return await _context.Intensities
                .Select
                (x => new IntensitiesDTO
                {
                    Id = x.Id,
                    Name = x.Nombre,
                    Description = x.Description
                }).ToListAsync();
        }

        public async Task<int> SaveAsync(POSTFormulaDTO dto)
        {
            // 1. Validar que Note1 esté presente en cada bloque
            if (dto.TopNotes.Note1 == null || dto.HeartNotes.Note1 == null || dto.BaseNotes.Note1 == null)
                throw new Exception("Cada sección debe tener al menos una nota principal (Note1).");

            // 2. Crear instancias de FormulaNote
            var notaSalida = new FormulaNote
            {
                NotaId1 = dto.TopNotes.Note1.Id,
                NotaId2 = dto.TopNotes.Note2?.Id,
                NotaId3 = dto.TopNotes.Note3?.Id,
                NotaId4 = dto.TopNotes.Note4?.Id
            };

            var notaCorazon = new FormulaNote
            {
                NotaId1 = dto.HeartNotes.Note1.Id,
                NotaId2 = dto.HeartNotes.Note2?.Id,
                NotaId3 = dto.HeartNotes.Note3?.Id,
                NotaId4 = dto.HeartNotes.Note4?.Id
            };

            var notaFondo = new FormulaNote
            {
                NotaId1 = dto.BaseNotes.Note1.Id,
                NotaId2 = dto.BaseNotes.Note2?.Id,
                NotaId3 = dto.BaseNotes.Note3?.Id,
                NotaId4 = dto.BaseNotes.Note4?.Id
            };

            Console.WriteLine(notaSalida.FormulaNotaId); // debe ser 0
            Console.WriteLine(notaCorazon.FormulaNotaId); // debe ser 0
            Console.WriteLine(notaFondo.FormulaNotaId); // debe ser 0

            // 3. Guardar las notas primero
            _context.FormulaNotes.AddRange(notaSalida, notaCorazon, notaFondo);
            await _context.SaveChangesAsync();

            // 4. Calcular concentración según intensidad
            var concentracion = FormulaConcentration.CalculateConcentrationBasedOnIntensity(dto.IdIntensidad);

            // 5. Crear la fórmula
            var formula = new Formula
            {
                FormulaSalida = notaSalida.FormulaNotaId,
                FormulaCorazon = notaCorazon.FormulaNotaId,
                FormulaFondo = notaFondo.FormulaNotaId,
                IntensidadId = dto.IdIntensidad,
                ConcentracionAlcohol = concentracion.Alcohol,
                ConcentracionAgua = concentracion.Water,
                ConcentracionEsencia = concentracion.Essence,
                CreadorId = dto.IdCreador
            };

            _context.Formulas.Add(formula);
            await _context.SaveChangesAsync();

            return formula.Id;
        }

        public async Task<GETFormulaDTO?> GetFormulaByIdAsync(int id)
        {
            var found = await _context.Formulas
                .Include(f => f.Intensidad)
                .Include(f => f.FormulaSalidaNavigation)
                    .ThenInclude(n => n.NotaId1Navigation)
                .Include(f => f.FormulaSalidaNavigation).ThenInclude(n => n.NotaId2Navigation)
                .Include(f => f.FormulaSalidaNavigation).ThenInclude(n => n.NotaId3Navigation)
                .Include(f => f.FormulaSalidaNavigation).ThenInclude(n => n.NotaId4Navigation)
                .Include(f => f.FormulaCorazonNavigation).ThenInclude(n => n.NotaId1Navigation)
                .Include(f => f.FormulaCorazonNavigation).ThenInclude(n => n.NotaId2Navigation)
                .Include(f => f.FormulaCorazonNavigation).ThenInclude(n => n.NotaId3Navigation)
                .Include(f => f.FormulaCorazonNavigation).ThenInclude(n => n.NotaId4Navigation)
                .Include(f => f.FormulaFondoNavigation).ThenInclude(n => n.NotaId1Navigation)
                .Include(f => f.FormulaFondoNavigation).ThenInclude(n => n.NotaId2Navigation)
                .Include(f => f.FormulaFondoNavigation).ThenInclude(n => n.NotaId3Navigation)
                .Include(f => f.FormulaFondoNavigation).ThenInclude(n => n.NotaId4Navigation)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (found == null) return null;

            return new GETFormulaDTO
            {
                Intensity = new IntensitiesDTO
                {
                    Id = found.Intensidad.Id,
                    Name = found.Intensidad.Nombre,
                    Description = found.Intensidad.Description
                },
                ConcentracionAlcohol = found.ConcentracionAlcohol,
                ConcentracionAgua = found.ConcentracionAgua,
                ConcentracionEsencia = found.ConcentracionEsencia,
                NotasSalidaIds = MapFormulaNoteToDTO(found.FormulaSalidaNavigation),
                NotasCorazonIds = MapFormulaNoteToDTO(found.FormulaCorazonNavigation),
                NotasFondoIds = MapFormulaNoteToDTO(found.FormulaFondoNavigation)
            };
        }

        private GETNoteDTO MapNoteToDTO(Note note)
        {
            return new GETNoteDTO
            {
                Id = note.Id,
                Name = note.Nombre,
                Description = note.Descripcion,
                Family = note.FamiliaOlfativa.Nombre,
                Sector = note.PiramideOlfativa.Sector,
                Duration = note.PiramideOlfativa.Duracion
            };
        }
        private GETFormulaNoteDTO MapFormulaNoteToDTO(FormulaNote note)
        {
            return new GETFormulaNoteDTO
            {
                Note1 = MapNoteToDTO(note.NotaId1Navigation),
                Note2 = note.NotaId2.HasValue ? MapNoteToDTO(note.NotaId2Navigation!) : null,
                Note3 = note.NotaId3.HasValue ? MapNoteToDTO(note.NotaId3Navigation!) : null,
                Note4 = note.NotaId4.HasValue ? MapNoteToDTO(note.NotaId4Navigation!) : null
            };
        }

        public static byte[] CrearPdf(GETFormulaDTO dto)
        {
            var doc = new PdfDocument();
            var page = doc.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Verdana", 12);
            int y = 40;
            gfx.DrawString("Diseño personalizado", new XFont("Verdana", 18, XFontStyle.Bold), XBrushes.Black, new XPoint(40, y));
            y += 40;

            gfx.DrawString($"Notas de fondo: {dto.NotasFondoIds}", font, XBrushes.Black, new XPoint(40, y));
            y += 20;
            gfx.DrawString($"Notas de corazón: {dto.NotasCorazonIds}", font, XBrushes.Black, new XPoint(40, y));
            y += 20;
            gfx.DrawString($"Notas de salida: {dto.NotasSalidaIds}", font, XBrushes.Black, new XPoint(40, y));
            y += 20;
            gfx.DrawString($"Intensidad: {dto.Intensity}", font, XBrushes.Black, new XPoint(40, y));
            y += 20;
            gfx.DrawString($"Concentración de alcohol: {dto.ConcentracionAlcohol}", font, XBrushes.Black, new XPoint(40, y));
            y += 20;
            gfx.DrawString($"Concentración de agua: {dto.ConcentracionAgua}", font, XBrushes.Black, new XPoint(40, y));
            y += 20;
            gfx.DrawString($"Concentración de esencia: {dto.ConcentracionEsencia}", font, XBrushes.Black, new XPoint(40, y));
            y += 20;
            using var stream = new MemoryStream();
            doc.Save(stream, false);
            return stream.ToArray();
            throw new NotImplementedException();
        }
    }
}
