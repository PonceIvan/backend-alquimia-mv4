using backendAlquimia.Models;

namespace backendAlquimia.alquimia.Services.Interfaces
{
    public interface INoteService
    {
        Task<List<NotesGroupedByFamilyDTO>> GetTopNotesGroupedByFamilyAsync();
        Task<List<NotesGroupedByFamilyDTO>> GetHeartNotesGroupedByFamilyAsync();
        Task<List<NotesGroupedByFamilyDTO>> GetBaseNotesGroupedByFamilyAsync();
        Task<List<NotesGroupedByFamilyDTO>> GetCompatibleNotesAsync(List<int> seleccionadasIds, string Sector);
        Task<NoteDTO> GetNoteInfoAsync(int id);
    }
}