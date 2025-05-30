//using backendAlquimia.alquimia.Services.Models;
using backendAlquimia.Models;

namespace backendAlquimia.alquimia.Services.Interfaces
{
    public interface IFormulaService
    {
        //Task<GETFormulaDTO> guardar(POSTFormulaDTO dto);
        Task<List<IntensidadDTO>> ObtenerIntensidadAsync();
        Task<List<IntensitiesDTO>> GetIntensitiesAsync();
    }
}
