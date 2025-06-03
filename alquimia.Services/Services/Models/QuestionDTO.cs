namespace alquimia.Services.Services.Models
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Pregunta { get; set; }
        public List<OptionDTO> Opciones { get; set; }
    }
}