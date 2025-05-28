using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
