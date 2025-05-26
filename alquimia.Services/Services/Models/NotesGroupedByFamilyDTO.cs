namespace backendAlquimia.Models
{
    public class NotesGroupedByFamilyDTO
    {
        public string Family { get; set; }
        public List<NoteDTO> Notes { get; set; }
    }
}
