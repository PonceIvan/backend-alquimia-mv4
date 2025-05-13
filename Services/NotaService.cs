using backendAlquimia.Data;
using backendAlquimia.Models;
using backendAlquimia.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backendAlquimia.Services
{
    public class NotaService : INotaService
    {
        private readonly AlquimiaDbContext _context;

        public NotaService(AlquimiaDbContext context)
        {
            _context = context;
        }

        public async Task<List<NotaDTO>> ObtenerNotasDeSalidaAsync()
        {
            return await _context.Notas
                        .Where(n => n.Sector.Nombre == "Salida")
                        .Select(n => new NotaDTO
                        {
                            Id = n.Id,
                            Nombre = n.Nombre,
                            Descripcion = n.Descripcion
                        })
                        .ToListAsync();

        }
    }

}
