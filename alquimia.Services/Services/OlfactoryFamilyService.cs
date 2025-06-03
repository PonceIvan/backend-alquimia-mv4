using alquimia.Data.Data.Entities;
using alquimia.Services.Services.Interfaces;
using alquimia.Services.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Services.Services
{
    public class OlfactoryFamilyService : IOlfactoryFamilyService
    {
        private readonly AlquimiaDbContext _context;

        public OlfactoryFamilyService(AlquimiaDbContext context)
        {
            _context = context;
        }
        public async Task<OlfactoryFamilyDTO> GetOlfactoryFamilyInfoAsync(int id)
        {
            var found = await _context.OlfactoryFamilies.FirstOrDefaultAsync(f => f.Id == id);
            if (found == null)
            {
                throw new KeyNotFoundException();
            }
            return new OlfactoryFamilyDTO
            {
                Id = found.Id,
                Name = found.Nombre,
                Description = found.Description,
                Image1 = found.Image1,
            };
        }
    }
}
