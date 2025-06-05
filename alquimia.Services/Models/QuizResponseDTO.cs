namespace alquimia.Services.Models
{
    public class QuizResponseDTO
    {
        public string ConcentrationType { get; set; } = string.Empty;
        public List<QuizResultDTO> Families { get; set; } = new();
    }
}
