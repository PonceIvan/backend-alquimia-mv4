using backendAlquimia.Models;

namespace backendAlquimia.alquimia.Services.Interfaces
{
    public interface INoteService
    {
        Task<List<NotesGroupedByFamilyDTO>> GetTopNotesGroupedByFamilyAsync();
        Task<List<NotesGroupedByFamilyDTO>> GetHeartNotesGroupedByFamilyAsync();
        Task<List<NotesGroupedByFamilyDTO>> GetBaseNotesGroupedByFamilyAsync();
        //Task<int> CalcularCompatibilidadAsync(int notaAId, int notaBId);
        //Task<bool> EsCompatibleConSeleccionAsync(int nuevaNotaId, List<int> seleccionadasIds);
        //Task<List<NotesGroupedByFamily>> ObtenerNotasCompatiblesAsync(List<int> seleccionadasIds, string Sector);

    }
}