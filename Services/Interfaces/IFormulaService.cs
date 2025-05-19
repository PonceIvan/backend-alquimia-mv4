using backendAlquimia.Models;

namespace backendAlquimia.Services.Interfaces
{
    public interface IFormulaService
    {
        Task<List<IntensidadDTO>> ObtenerIntensidadAsync();
    }
}
