//using backendAlquimia.alquimia.Services.Models;
using backendAlquimia.Models;

namespace backendAlquimia.alquimia.Services.Interfaces
{
    public interface IFormulaService
    {
        Task<GETFormulaDTO> GetFormulaByIdAsync(int id);
        Task<int> SaveAsync(POSTFormulaDTO formula);
        Task<List<IntensityDTO>> GetIntensitiesAsync();
    }
}
