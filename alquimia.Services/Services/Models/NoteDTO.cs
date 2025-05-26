
namespace backendAlquimia.Models
{
    public class NoteDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; internal set; }
        public string Sector { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; internal set; }
    }
}
