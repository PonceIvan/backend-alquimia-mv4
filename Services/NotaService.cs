using backendAlquimia.Data;
using backendAlquimia.Models;
using backendAlquimia.Services.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace backendAlquimia.Services
{
    public class NotaService : INotaService
    {
        private readonly AlquimiaDbContext _context;
        public const string Sector_Corazon = "Corazón";
        public const string Sector_Salida = "Salida";

        public NotaService(AlquimiaDbContext context)
        {
            _context = context;
        }

        public async Task<List<NotasPorFamiliaDTO>> ObtenerNotasDeCorazonAgrupadasPorFamiliaAsync()
        {
            return await _context.Notas
         .Where(n => n.Sector.Nombre == Sector_Corazon)
         .GroupBy(n => n.FamiliaOlfativa.Nombre)
         .Select(grupo => new NotasPorFamiliaDTO
         {
             Familia = grupo.Key,
             Notas = grupo.Select(n => new NotaDTO
             {
                 Id = n.Id,
                 Nombre = n.Nombre,
                 Familia = n.FamiliaOlfativa.Nombre,
                 Sector = n.Sector.Nombre,
                 Descripcion = n.Descripcion,
                 Duracion = n.Sector.Duracion
             }).ToList()
         }).ToListAsync();
        }

        public async Task<List<NotasPorFamiliaDTO>> ObtenerNotasDeSalidaAgrupadasPorFamiliaAsync()
        {
            return await _context.Notas
         .Where(n => n.Sector.Nombre == Sector_Salida)
         .GroupBy(n => n.FamiliaOlfativa.Nombre)
         .Select(grupo => new NotasPorFamiliaDTO
         {
             Familia = grupo.Key,
             Notas = grupo.Select(n => new NotaDTO
             {
                 Id = n.Id,
                 Nombre = n.Nombre,
                 Familia = n.FamiliaOlfativa.Nombre,
                 Sector = n.Sector.Nombre,
                 Descripcion = n.Descripcion,
                 Duracion = n.Sector.Duracion
             }).ToList()
         }).ToListAsync();
        }
    }

}
