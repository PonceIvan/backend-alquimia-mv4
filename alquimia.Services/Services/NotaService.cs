//using backendAlquimia.Data;
//using backendAlquimia.Data.Entities;
//using backendAlquimia.Models;
using backendAlquimia.alquimia.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backendAlquimia.alquimia.Services
{
    public class NotaService : INotaService
    {
        //private readonly AlquimiaDbContext _context;
        private const string Sector_Fondo = "Fondo";
        private const string Sector_Corazon = "Corazón";
        private const string Sector_Salida = "Salida";
        private const int UmbralCompatibilidad = 70;

        //public NotaService(AlquimiaDbContext context)
        //{
        //    _context = context;
        //}

        //public async Task<List<NotasPorFamiliaDTO>> ObtenerNotasDeCorazonAgrupadasPorFamiliaAsync()
        //{
        //    return await _context.Notas
        // .Where(n => n.Sector.Nombre == Sector_Corazon)
        // .GroupBy(n => n.FamiliaOlfativa.Nombre)
        // .Select(grupo => new NotasPorFamiliaDTO
        // {
        //     Familia = grupo.Key,
        //     Notas = grupo.Select(n => new NotaDTO
        //     {
        //         Id = n.Id,
        //         Nombre = n.Nombre,
        //         Familia = n.FamiliaOlfativa.Nombre,
        //         Sector = n.Sector.Nombre,
        //         Descripcion = n.Descripcion,
        //         Duracion = n.Sector.Duracion
        //     }).ToList()
        // }).ToListAsync();
        //}

        //public async Task<List<NotasPorFamiliaDTO>> ObtenerNotasDeSalidaAgrupadasPorFamiliaAsync()
        //{
        //    return await _context.Notas
        //    .Where(n => n.Sector.Nombre == Sector_Salida)
        //    .GroupBy(n => n.FamiliaOlfativa.Nombre)
        //    .Select(grupo => new NotasPorFamiliaDTO
        //    {
        //        Familia = grupo.Key,
        //        Notas = grupo.Select(n => new NotaDTO
        //        {
        //            Id = n.Id,
        //            Nombre = n.Nombre,
        //            Familia = n.FamiliaOlfativa.Nombre,
        //            Sector = n.Sector.Nombre,
        //            Descripcion = n.Descripcion,
        //            Duracion = n.Sector.Duracion
        //        }).ToList()
        //    }).ToListAsync();
        //}

        //public async Task<List<NotasPorFamiliaDTO>> ObtenerNotasDeFondoAgrupadasPorFamiliaAsync()
        //{
        //    return await _context.Notas
        // .Where(n => n.Sector.Nombre == Sector_Fondo)
        // .GroupBy(n => n.FamiliaOlfativa.Nombre)
        // .Select(grupo => new NotasPorFamiliaDTO
        // {
        //     Familia = grupo.Key,
        //     Notas = grupo.Select(n => new NotaDTO
        //     {
        //         Id = n.Id,
        //         Nombre = n.Nombre,
        //         Familia = n.FamiliaOlfativa.Nombre,
        //         Sector = n.Sector.Nombre,
        //         Descripcion = n.Descripcion,
        //         Duracion = n.Sector.Duracion
        //     }).ToList()
        // }).ToListAsync();
        //}


        // //////////////////////////////////////

        // Calcula la compatibilidad entre dos notas a partir de la familia.
        //public async Task<int> CalcularCompatibilidadAsync(int notaAId, int notaBId)
        //{
        //    var notaA = await _context.Notas.Include(n => n.FamiliaOlfativa).FirstOrDefaultAsync(n => n.Id == notaAId);
        //    var notaB = await _context.Notas.Include(n => n.FamiliaOlfativa).FirstOrDefaultAsync(n => n.Id == notaBId);

        //    if (notaA == null || notaB == null)
        //        throw new ArgumentException("Alguna de las notas no existe");

        //    var familia1Id = notaA.FamiliaOlfativaId;
        //    var familia2Id = notaB.FamiliaOlfativaId;

        //    var compatibilidad = await _context.CompatibilidadesFamilias
        //        .FirstOrDefaultAsync(c =>
        //            (c.Familia1Id == familia1Id && c.Familia2Id == familia2Id) ||
        //            (c.Familia1Id == familia2Id && c.Familia2Id == familia1Id));

        //    return compatibilidad?.GradoDeCompatibilidad ?? 0; // valor neutro si no hay definición
        //}

        //public async Task<bool> EsCompatibleConSeleccionAsync(int nuevaNotaId, List<int> seleccionadasIds)
        //{
        //    var nuevaNota = await _context.Notas.Include(n => n.FamiliaOlfativa).FirstOrDefaultAsync(n => n.Id == nuevaNotaId);
        //    if (nuevaNota == null) return false;

        //    var seleccionadas = await _context.Notas
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

        //public async Task<List<Nota>> ObtenerNotasCompatiblesAsync(List<int> seleccionadasIds)
        //{
        //    var seleccionadas = await _context.Notas
        //    .Where(n => seleccionadasIds.Contains(n.Id))
        //    .Include(n => n.FamiliaOlfativa)
        //    .ToListAsync();

        //    var todasLasNotas = await _context.Notas.Include(n => n.FamiliaOlfativa).ToListAsync();

        //    var compatibles = new List<Nota>();

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

        //public async Task<List<NotasPorFamiliaDTO>> ObtenerNotasCompatiblesAsync(List<int> seleccionadasIds, string Sector)
        //{
        //    var seleccionadas = await _context.Notas
        //        .Where(n => seleccionadasIds.Contains(n.Id) && n.Sector.Nombre == Sector)
        //        .Include(n => n.FamiliaOlfativa)
        //        .Include(n => n.Sector)
        //        .ToListAsync();

        //    var todasLasNotas = await _context.Notas
        //        .Include(n => n.FamiliaOlfativa)
        //        .Include(n => n.Sector)
        //        .ToListAsync();

        //    var compatibles = new List<Nota>();

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
        //        .Select(g => new NotasPorFamiliaDTO
        //        {
        //            Familia = g.Key,
        //            Notas = g.Select(n => new NotaDTO
        //            {
        //                Id = n.Id,
        //                Nombre = n.Nombre,
        //                Familia = n.FamiliaOlfativa.Nombre,
        //                Sector = n.Sector.Nombre,
        //                Descripcion = n.Descripcion,
        //                Duracion = n.Sector.Duracion
        //            }).ToList()
        //        })
        //        .ToList();

        //    return resultado;
        //}
        //public async Task<List<NotasPorFamiliaDTO>> ObtenerNotasCompatiblesAsync(List<int> seleccionadasIds, string sector)
        //{
        //    // Obtener las notas seleccionadas del sector especificado
        //    var seleccionadas = await _context.Notas
        //        .Where(n => seleccionadasIds.Contains(n.Id) && n.Sector.Nombre == sector)
        //        .Include(n => n.FamiliaOlfativa)
        //        .Include(n => n.Sector)
        //        .ToListAsync();

        //    if (!seleccionadas.Any())
        //        return new List<NotasPorFamiliaDTO>(); // O lanzar error si preferís

        //    // Obtener todas las notas del mismo sector, excluyendo las ya seleccionadas
        //    var todasLasNotas = await _context.Notas
        //        .Where(n => n.Sector.Nombre == sector && !seleccionadasIds.Contains(n.Id))
        //        .Include(n => n.FamiliaOlfativa)
        //        .Include(n => n.Sector)
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
        //                Sector = n.Sector.Nombre,
        //                Descripcion = n.Descripcion,
        //                Duracion = n.Sector.Duracion
        //            }).ToList()
        //        })
        //        .ToList();

        //    return resultado;
        //}

    }
}
