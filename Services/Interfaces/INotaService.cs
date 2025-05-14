using backendAlquimia.Models;

namespace backendAlquimia.Services.Interfaces
{
    public interface INotaService
    {
        Task<List<NotaDTO>> ObtenerNotasDeCorazonAsync();
        Task<List<NotaDTO>> ObtenerNotasDeSalidaAsync();
    }
}