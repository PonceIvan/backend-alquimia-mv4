using alquimia.Data.Data.Entities;
using backendAlquimia.alquimia.Services.Interfaces;
using backendAlquimia.Models;
using Microsoft.EntityFrameworkCore;
using Note = alquimia.Data.Data.Entities.Note;

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
                .Include(n => n.OlfactoryPyramid)
                .Include(n => n.OlfactoryFamily)
                .Where(n => n.OlfactoryPyramid.Sector == Heart)
                .GroupBy(n => n.OlfactoryFamily.Nombre)
                .Select(grupo => new NotesGroupedByFamilyDTO
                {
                    Family = grupo.Key,
                    Notes = grupo.Select(n => new NoteDTO
                    {
                        Id = n.Id,
                        Name = n.Name,
                        Family = n.OlfactoryFamily.Nombre,
                        Sector = n.OlfactoryPyramid.Sector,
                        Description = n.Description,
                        Duration = n.OlfactoryPyramid.Duracion
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<List<NotesGroupedByFamilyDTO>> GetTopNotesGroupedByFamilyAsync()
        {
            return await _context.Notes
                .Include(n => n.OlfactoryPyramid)
                .Include(n => n.OlfactoryFamily)
                .Where(n => n.OlfactoryPyramid.Sector == Top)
                .GroupBy(n => n.OlfactoryFamily.Nombre)
                .Select(grupo => new NotesGroupedByFamilyDTO
                {
                    Family = grupo.Key,
                    Notes = grupo.Select(n => new NoteDTO
                    {
                        Id = n.Id,
                        Name = n.Name,
                        Family = n.OlfactoryFamily.Nombre,
                        Sector = n.OlfactoryPyramid.Sector,
                        Description = n.Description,
                        Duration = n.OlfactoryPyramid.Duracion
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<List<NotesGroupedByFamilyDTO>> GetBaseNotesGroupedByFamilyAsync()
        {
            return await _context.Notes
                .Include(n => n.OlfactoryPyramid)
                .Include(n => n.OlfactoryFamily)
                .Where(n => n.OlfactoryPyramid.Sector == Base)
                .GroupBy(n => n.OlfactoryFamily.Nombre)
                .Select(grupo => new NotesGroupedByFamilyDTO
                {
                    Family = grupo.Key,
                    Notes = grupo.Select(n => new NoteDTO
                    {
                        Id = n.Id,
                        Name = n.Name,
                        Family = n.OlfactoryFamily.Nombre,
                        Sector = n.OlfactoryPyramid.Sector,
                        Description = n.Description,
                        Duration = n.OlfactoryPyramid.Duracion
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<List<NotesGroupedByFamilyDTO>> GetCompatibleNotesAsync(List<int> seleccionadasIds, string sector)
        {
            var seleccionadas = await _context.Notes
                .Where(n => seleccionadasIds.Contains(n.Id) && n.OlfactoryPyramid.Sector == sector)
                .Include(n => n.OlfactoryFamily)
                .Include(n => n.OlfactoryPyramid)
                .ToListAsync();

            var todasLasNotasDelSector = await _context.Notes
                .Where(n => n.OlfactoryPyramid.Sector == sector)
                .Include(n => n.OlfactoryFamily)
                .Include(n => n.OlfactoryPyramid)
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

                    int f1 = Math.Min(seleccionada.OlfactoryFamilyId, candidata.OlfactoryFamilyId);
                    int f2 = Math.Max(seleccionada.OlfactoryFamilyId, candidata.OlfactoryFamilyId);

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
                .GroupBy(c => c.Nota.OlfactoryFamily.Nombre)
                .Select(g => new NotesGroupedByFamilyDTO
                {
                    Family = g.Key,
                    Notes = g.Select(c => new NoteDTO
                    {
                        Id = c.Nota.Id,
                        Name = c.Nota.Name,
                        Description = c.Nota.Description,
                        Family = c.Nota.OlfactoryFamily.Nombre,
                        Sector = c.Nota.OlfactoryPyramid.Sector,
                        Duration = c.Nota.OlfactoryPyramid.Duracion
                    }).ToList()
                })
                .ToList();

            return resultado;
        }

        public async Task<NoteDTO> GetNoteInfoAsync(int id)
        {
            var found = await _context.Notes
                .Include(n => n.OlfactoryPyramid)
                .Include(n => n.OlfactoryFamily)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (found == null)
            {
                throw new KeyNotFoundException();
            }

            return NoteToDTO(found);
        }

        private NoteDTO NoteToDTO(Note found)
        {
            return new NoteDTO
            {
                Id = found.Id,
                Name = found.Name,
                Family = found.OlfactoryFamily.Nombre,
                Sector = found.OlfactoryPyramid.Sector,
                Description = found.Description,
                Duration = found.OlfactoryPyramid.Duracion
            };
        }
    }
}
