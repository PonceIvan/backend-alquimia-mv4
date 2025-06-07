namespace alquimia.Services.Models
{
    public class NoteDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Sector { get; set; }
        public string Description { get; set; }
        public TimeOnly Duration { get; set; }
    }
}