
using alquimia.Services.Services.Models;

namespace alquimia.Services.Services.Interfaces
{
    public interface IOlfactoryFamilyService
    {
        Task<OlfactoryFamilyDTO> GetOlfactoryFamilyInfoAsync(int id);
    }
}
