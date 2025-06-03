using alquimia.Data.Data.Entities;
using alquimia.Services.Services.Interfaces;
using alquimia.Services.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Services.Services
{
    public class QuizService : IQuizService
    {
        private readonly AlquimiaDbContext _context;

        public QuizService(AlquimiaDbContext context)
        {
            _context = context;
        }

        public async Task<List<QuestionDTO>> GetQuestionsAsync()
        {
            var preguntas = await _context.Questions
                .Include(q => q.IdOpcionesNavigation)
                .ToListAsync();

            return preguntas.Select(q => new QuestionDTO
            {
                Id = q.Id,
                Pregunta = q.Pregunta,
                Opciones = new List<OptionDTO>
                {
                    new OptionDTO { Letra = "A", Texto = q.IdOpcionesNavigation?.Option1 ?? "", ImagenUrl = q.IdOpcionesNavigation?.Image1 },
                    new OptionDTO { Letra = "B", Texto = q.IdOpcionesNavigation?.Option2 ?? "",ImagenUrl  = q.IdOpcionesNavigation?.Image2 },
                    new OptionDTO { Letra = "C", Texto = q.IdOpcionesNavigation?.Option3 ?? "", ImagenUrl = q.IdOpcionesNavigation?.Image3 },
                    new OptionDTO { Letra = "D", Texto = q.IdOpcionesNavigation?.Option4 ?? "", ImagenUrl  = q.IdOpcionesNavigation?.Image4 },
                }
            }).ToList();
        }

        public Task SaveAnswersAsync(List<AnswerDTO> respuestas)
        {
            // No persiste por ahora
            return Task.CompletedTask;
        }

        public async Task<object?> GetResultAsync(List<AnswerDTO> respuestas)
        {
            var conteo = new Dictionary<string, int> { { "A", 0 }, { "B", 0 }, { "C", 0 }, { "D", 0 } };

            foreach (var r in respuestas)
            {
                var letra = r.SelectedOption.ToUpper();
                if (conteo.ContainsKey(letra))
                    conteo[letra]++;
            }

            var letraDominante = conteo.OrderByDescending(k => k.Value).First().Key;

            var letraAFamiliaNombre = new Dictionary<string, string>
            {
                { "A", "Fresca" },
                { "B", "Amaderada" },
                { "C", "Oriental" },
                { "D", "Floral" }
            };

            if (!letraAFamiliaNombre.TryGetValue(letraDominante, out var nombreFamilia))
                return null;

            var familia = await _context.OlfactoryFamilies
                .FirstOrDefaultAsync(f => f.Nombre == nombreFamilia);

            if (familia == null)
                return null;

            return new
            {
                letraDominante,
                Nombre = familia.Nombre,
                Descripcion = familia.Description,
                Imagen = familia.Image1
            };
        }
    }
}