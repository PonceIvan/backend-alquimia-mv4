using alquimia.Data.Data.Entities;
using backendAlquimia.alquimia.Services.Interfaces;
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
                    Notes = grupo.Select(n => new NoteDTO
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
                    Notes = grupo.Select(n => new NoteDTO
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
                    Notes = grupo.Select(n => new NoteDTO
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


        // //////////////////////////////////////

        // Calcula la compatibilidad entre dos notas a partir de la familia.
        //public async Task<int> CalcularCompatibilidadAsync(int notaAId, int notaBId)
        //{
        //    var notaA = await _context.Notes.Include(n => n.FamiliaOlfativa).FirstOrDefaultAsync(n => n.Id == notaAId);
        //    var notaB = await _context.Notes.Include(n => n.FamiliaOlfativa).FirstOrDefaultAsync(n => n.Id == notaBId);

        //    if (notaA == null || notaB == null)
        //        throw new ArgumentException("Alguna de las notas no existe");

        //    var familia1Id = notaA.FamiliaOlfativaId;
        //    var familia2Id = notaB.FamiliaOlfativaId;

        //    var compatibilidad = await _context.FamilyCompatibilities
        //        .FirstOrDefaultAsync(c =>
        //            (c.Familia1Id == familia1Id && c.Familia2Id == familia2Id) ||
        //            (c.Familia1Id == familia2Id && c.Familia2Id == familia1Id));

        //    return compatibilidad?.GradoDeCompatibilidad ?? 0; // valor neutro si no hay definición
        //}

        //public async Task<bool> EsCompatibleConSeleccionAsync(int nuevaNotaId, List<int> seleccionadasIds)
        //{
        //    var nuevaNota = await _context.Notes.Include(n => n.FamiliaOlfativa).FirstOrDefaultAsync(n => n.Id == nuevaNotaId);
        //    if (nuevaNota == null) return false;

        //    var seleccionadas = await _context.Notes
        //        .Where(n => seleccionadasIds.Contains(n.Id))
        //        .Include(n => n.FamiliaOlfativa)
        //        .ToListAsync();

        //    foreach (var anterior in seleccionadas)
        //    {
        //        int grado = await CalcularCompatibilidadAsync(anterior.Id, nuevaNota.Id);
        //        if (grado < 70)
        //            return false;
        //    }

        //    seleccionadasIds.Add(nuevaNotaId);
        //    return true;
        //}

        //public async Task<List<Note>> ObtenerNotasCompatiblesAsync(List<int> seleccionadasIds)
        //{
        //    var seleccionadas = await _context.Notes
        //    .Where(n => seleccionadasIds.Contains(n.Id))
        //    .Include(n => n.FamiliaOlfativa)
        //    .ToListAsync();

        //    var todasLasNotas = await _context.Notes.Include(n => n.FamiliaOlfativa).ToListAsync();

        //    var compatibles = new List<Note>();

        //    foreach (var candidata in todasLasNotas)
        //    {
        //        if (seleccionadas.Any(n => n.Id == candidata.Id)) continue;

        //        bool esCompatible = true;

        //        foreach (var anterior in seleccionadas)
        //        {
        //            int grado = await CalcularCompatibilidadAsync(anterior.Id, candidata.Id);
        //            if (grado < 70)
        //            {
        //                esCompatible = false;
        //                break;
        //            }
        //        }

        //        if (esCompatible)
        //            compatibles.Add(candidata);
        //    }

        //    return compatibles;
        //}

        //public async Task<List<NotesGroupedByFamily>> ObtenerNotasCompatiblesAsync(List<int> seleccionadasIds, string Sector)
        //{
        //    var seleccionadas = await _context.Notes
        //        .Where(n => seleccionadasIds.Contains(n.Id) && n.PiramideOlfativa.Sector == Sector)
        //        .Include(n => n.FamiliaOlfativa)
        //        .Include(n => n.PiramideOlfativa.Sector)
        //        .ToListAsync();

        //    var todasLasNotas = await _context.Notes
        //        .Include(n => n.FamiliaOlfativa)
        //        .Include(n => n.PiramideOlfativa.Sector)
        //        .ToListAsync();

        //    var compatibles = new List<Note>();

        //    foreach (var candidata in todasLasNotas)
        //    {
        //        if (seleccionadas.Any(n => n.Id == candidata.Id)) continue;

        //        bool esCompatible = true;

        //        foreach (var anterior in seleccionadas)
        //        {
        //            int grado = await CalcularCompatibilidadAsync(anterior.Id, candidata.Id);
        //            if (grado < 70)
        //            {
        //                esCompatible = false;
        //                break;
        //            }
        //        }

        //        if (esCompatible)
        //            compatibles.Add(candidata);
        //    }

        //    // Agrupar las notas compatibles por familia
        //    var resultado = compatibles
        //        .GroupBy(n => n.FamiliaOlfativa.Nombre)
        //        .Select(g => new NotesGroupedByFamily
        //        {
        //            Familia = g.Key,
        //            Notas = g.Select(n => new NotaDTO
        //            {
        //                Id = n.Id,
        //                Nombre = n.Nombre,
        //                Familia = n.FamiliaOlfativa.Nombre,
        //                Sector = n.PiramideOlfativa.Sector,
        //                Descripcion = n.Descripcion,
        //                Duracion = n.PiramideOlfativa.Duracion
        //            }).ToList()
        //        })
        //        .ToList();

        //    return resultado;
        //}
        //public async Task<List<NotasPorFamiliaDTO>> ObtenerNotasCompatiblesAsync(List<int> seleccionadasIds, string sector)
        //{
        //    // Obtener las notas seleccionadas del sector especificado
        //    var seleccionadas = await _context.Notas
        //        .Where(n => seleccionadasIds.Contains(n.Id) && n.PiramideOlfativa.Sector == sector)
        //        .Include(n => n.FamiliaOlfativa)
        //        .Include(n => n.PiramideOlfativa.Sector)
        //        .ToListAsync();

        //    if (!seleccionadas.Any())
        //        return new List<NotasPorFamiliaDTO>(); // O lanzar error si preferís

        //    // Obtener todas las notas del mismo sector, excluyendo las ya seleccionadas
        //    var todasLasNotas = await _context.Notas
        //        .Where(n => n.PiramideOlfativa.Sector == sector && !seleccionadasIds.Contains(n.Id))
        //        .Include(n => n.FamiliaOlfativa)
        //        .Include(n => n.PiramideOlfativa.Sector)
        //        .ToListAsync();

        //    var compatibles = new List<Nota>();

        //    foreach (var candidata in todasLasNotas)
        //    {
        //        bool esCompatible = true;

        //        foreach (var anterior in seleccionadas)
        //        {
        //            int grado = await CalcularCompatibilidadAsync(anterior.Id, candidata.Id);
        //            if (grado < 70)
        //            {
        //                esCompatible = false;
        //                break;
        //            }
        //        }

        //        if (esCompatible)
        //            compatibles.Add(candidata);
        //    }

        //    // Agrupar las notas compatibles por familia
        //    var resultado = compatibles
        //        .GroupBy(n => n.FamiliaOlfativa.Nombre)
        //        .Select(g => new NotasPorFamiliaDTO
        //        {
        //            Familia = g.Key,
        //            Notas = g.Select(n => new NotaDTO
        //            {
        //                Id = n.Id,
        //                Nombre = n.Nombre,
        //                Familia = n.FamiliaOlfativa.Nombre,
        //                Sector = n.PiramideOlfativa.Sector,
        //                Descripcion = n.Descripcion,
        //                Duracion = n.PiramideOlfativa.Duracion
        //            }).ToList()
        //        })
        //        .ToList();

        //    return resultado;
        //}

    }
}
