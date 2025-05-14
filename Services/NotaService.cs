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

        public async Task<List<NotaDTO>> ObtenerNotasDeCorazonAsync()
        {
            return await _context.Notas
                        .Where(n => n.Sector.Nombre == Sector_Corazon)
                        .Select(n => new NotaDTO
                        {
                            Id = n.Id,
                            Nombre = n.Nombre,
                            Familia = n.FamiliaOlfativa.Nombre,
                            Sector = n.Sector.Nombre,
                            Descripcion = n.Descripcion,
                            Duracion = n.Sector.Duracion
                        })
                        .ToListAsync();
        }

        public async Task<List<NotaDTO>> ObtenerNotasDeSalidaAsync()
        {
            return await _context.Notas
                        .Where(n => n.Sector.Nombre == Sector_Salida)
                        .Select(n => new NotaDTO
                        {
                            Id = n.Id,
                            Nombre = n.Nombre,
                            Familia = n.FamiliaOlfativa.Nombre,
                            Sector = n.Sector.Nombre,
                            Descripcion = n.Descripcion,
                            Duracion = n.Sector.Duracion
                        })
                        .ToListAsync();
        }
    }

}
