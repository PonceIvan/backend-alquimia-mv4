namespace backendAlquimia.alquimia.Services.Services.QuizLogic
{
    public class FamilyMapping
    {
        public int QuestionId { get; set; }
        public int OptionIndex { get; set; }
        public List<string> Families { get; set; } = new();
    }
}
