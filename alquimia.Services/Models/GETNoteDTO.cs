namespace alquimia.Services.Models
{
    public class GETNoteDTO
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string Sector { get; set; }
        public string Description { get; set; }
        public TimeOnly Duration { get; set; }

        public string? Image { get; set; }

    }
}
