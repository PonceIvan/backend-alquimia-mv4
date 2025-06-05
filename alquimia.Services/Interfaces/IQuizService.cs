using alquimia.Services.Models;

namespace alquimia.Services.Interfaces
{
    public interface IQuizService
    {
        Task<List<QuestionDTO>> GetQuestionsAsync();
        Task SaveAnswersAsync(List<AnswerDTO> respuestas);
        Task<object?> GetResultAsync(List<AnswerDTO> respuestas);
    }
}