using alquimia.Services.Services.Models;

namespace alquimia.Services.Services.Interfaces
{
    public interface IQuizService
    {
        Task<List<QuestionDTO>> GetQuestionsAsync();
        Task SaveAnswersAsync(List<AnswerDTO> respuestas);
        Task<object?> GetResultAsync(List<AnswerDTO> respuestas);
    }
}