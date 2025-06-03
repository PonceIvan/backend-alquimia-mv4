using alquimia.Services.Services.Models;
using backendAlquimia.alquimia.Services.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alquimia.Services.Services.Interfaces
{
    public interface IQuizService
    {
        QuizResponseDTO GetQuizResult(List<int> selectedOptions);
    }
}
