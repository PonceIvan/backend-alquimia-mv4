using alquimia.Services.Models;

namespace alquimia.Services.Interfaces
{
    public interface IFormulaService
    {
        Task<GETFormulaDTO> GetFormulaByIdAsync(int id);
        Task<int> SaveAsync(POSTFormulaDTO formula);
        Task<List<IntensityDTO>> GetIntensitiesAsync();
    }
}
