using backendAlquimia.Data;
using backendAlquimia.Models;
using backendAlquimia.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backendAlquimia.Services
{
    public class FormulaService : IFormulaService
    {
        private readonly AlquimiaDbContext _context;
        public FormulaService(AlquimiaDbContext context)
        {
            _context = context;
        }

        public async Task<List<IntensidadDTO>> ObtenerIntensidadAsync()
        {
            return await _context.Intensidades
                .Select
                (x => new IntensidadDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                }).ToListAsync();
        }
    }
}
