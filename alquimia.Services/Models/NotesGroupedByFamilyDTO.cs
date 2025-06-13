namespace alquimia.Services.Models
{
    public class NotesGroupedByFamilyDTO
    {
        public string Family { get; set; }

        public int FamilyId { get; set; }

        public List<NoteDTO> Notes { get; set; }
    }
}