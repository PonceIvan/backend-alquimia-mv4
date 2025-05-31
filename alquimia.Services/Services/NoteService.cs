using alquimia.Data.Data.Entities;
using backendAlquimia.alquimia.Services.Interfaces;
using backendAlquimia.alquimia.Services.Services.Models;
using backendAlquimia.Models;
using Microsoft.EntityFrameworkCore;

namespace backendAlquimia.alquimia.Services.Services
{
    public class NoteService : INoteService
    {
        private readonly AlquimiaDbContext _context;
        private const string Base = "Fondo";
        private const string Heart = "Corazón";
        private const string Top = "Salida";
        private const int UmbralCompatibilidad = 70;

        public NoteService(AlquimiaDbContext context)
        {
            _context = context;
        }

        public async Task<List<NotesGroupedByFamilyDTO>> GetHeartNotesGroupedByFamilyAsync()
        {
            return await _context.Notes
                .Include(n => n.PiramideOlfativa)
                .Include(n => n.FamiliaOlfativa)
                .Where(n => n.PiramideOlfativa.Sector == Heart)
                .GroupBy(n => n.FamiliaOlfativa.Nombre)
                .Select(grupo => new NotesGroupedByFamilyDTO
                {
                    Family = grupo.Key,
                    Notes = grupo.Select(n => new GETNoteDTO
                    {
                        Id = n.Id,
                        Name = n.Nombre,
                        Family = n.FamiliaOlfativa.Nombre,
                        Sector = n.PiramideOlfativa.Sector,
                        Description = n.Descripcion,
                        Duration = n.PiramideOlfativa.Duracion
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<List<NotesGroupedByFamilyDTO>> GetTopNotesGroupedByFamilyAsync()
        {
            return await _context.Notes
                .Include(n => n.PiramideOlfativa)
                .Include(n => n.FamiliaOlfativa)
                .Where(n => n.PiramideOlfativa.Sector == Top)
                .GroupBy(n => n.FamiliaOlfativa.Nombre)
                .Select(grupo => new NotesGroupedByFamilyDTO
                {
                    Family = grupo.Key,
                    Notes = grupo.Select(n => new GETNoteDTO
                    {
                        Id = n.Id,
                        Name = n.Nombre,
                        Family = n.FamiliaOlfativa.Nombre,
                        Sector = n.PiramideOlfativa.Sector,
                        Description = n.Descripcion,
                        Duration = n.PiramideOlfativa.Duracion
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<List<NotesGroupedByFamilyDTO>> GetBaseNotesGroupedByFamilyAsync()
        {
            return await _context.Notes
                .Include(n => n.PiramideOlfativa)
                .Include(n => n.FamiliaOlfativa)
                .Where(n => n.PiramideOlfativa.Sector == Base)
                .GroupBy(n => n.FamiliaOlfativa.Nombre)
                .Select(grupo => new NotesGroupedByFamilyDTO
                {
                    Family = grupo.Key,
                    Notes = grupo.Select(n => new GETNoteDTO
                    {
                        Id = n.Id,
                        Name = n.Nombre,
                        Family = n.FamiliaOlfativa.Nombre,
                        Sector = n.PiramideOlfativa.Sector,
                        Description = n.Descripcion,
                        Duration = n.PiramideOlfativa.Duracion
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<List<NotesGroupedByFamilyDTO>> GetCompatibleNotesAsync(List<int> seleccionadasIds, string sector)
        {
            var seleccionadas = await _context.Notes
                .Where(n => seleccionadasIds.Contains(n.Id) && n.PiramideOlfativa.Sector == sector)
                .Include(n => n.FamiliaOlfativa)
                .Include(n => n.PiramideOlfativa)
                .ToListAsync();

            var todasLasNotasDelSector = await _context.Notes
                .Where(n => n.PiramideOlfativa.Sector == sector)
                .Include(n => n.FamiliaOlfativa)
                .Include(n => n.PiramideOlfativa)
                .ToListAsync();

            var incompatibilidades = await _context.IncompatibleNotes.ToListAsync();
            var compatibilidades = await _context.FamilyCompatibilities.ToListAsync();

            var compatiblesConCompatibilidad = new List<(Note Nota, int MinCompatibilidad)>();

            foreach (var candidata in todasLasNotasDelSector)
            {
                if (seleccionadas.Any(n => n.Id == candidata.Id))
                    continue;

                bool esCompatible = true;
                int minCompatibilidad = int.MaxValue;

                foreach (var seleccionada in seleccionadas)
                {
                    if (incompatibilidades.Any(i =>
                        (i.NotaId == seleccionada.Id && i.NotaIncompatibleId == candidata.Id) ||
                        (i.NotaId == candidata.Id && i.NotaIncompatibleId == seleccionada.Id)))
                    {
                        esCompatible = false;
                        break;
                    }

                    int f1 = Math.Min(seleccionada.FamiliaOlfativaId, candidata.FamiliaOlfativaId);
                    int f2 = Math.Max(seleccionada.FamiliaOlfativaId, candidata.FamiliaOlfativaId);

                    var compat = compatibilidades.FirstOrDefault(c =>
                        c.FamiliaMenor == f1 && c.FamiliaMayor == f2);

                    if (compat == null || compat.GradoDeCompatibilidad < 70)
                    {
                        esCompatible = false;
                        break;
                    }

                    minCompatibilidad = Math.Min(minCompatibilidad, compat.GradoDeCompatibilidad);
                }

                if (esCompatible)
                    compatiblesConCompatibilidad.Add((candidata, minCompatibilidad));
            }

            // Ordenamos antes de agrupar
            var resultado = compatiblesConCompatibilidad
                .OrderByDescending(c => c.MinCompatibilidad)
                .GroupBy(c => c.Nota.FamiliaOlfativa.Nombre)
                .Select(g => new NotesGroupedByFamilyDTO
                {
                    Family = g.Key,
                    Notes = g.Select(c => new GETNoteDTO
                    {
                        Id = c.Nota.Id,
                        Name = c.Nota.Nombre,
                        Description = c.Nota.Descripcion,
                        Family = c.Nota.FamiliaOlfativa.Nombre,
                        Sector = c.Nota.PiramideOlfativa.Sector,
                        Duration = c.Nota.PiramideOlfativa.Duracion
                    }).ToList()
                })
                .ToList();

            return resultado;
        }
    }
}
