using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    new OptionDTO { Letra = "A", Texto = q.IdOpcionesNavigation?.Option1 ?? "", ImagenBase64 = ConvertToBase64(q.IdOpcionesNavigation?.Image1) },
                    new OptionDTO { Letra = "B", Texto = q.IdOpcionesNavigation?.Option2 ?? "", ImagenBase64 = ConvertToBase64(q.IdOpcionesNavigation?.Image2) },
                    new OptionDTO { Letra = "C", Texto = q.IdOpcionesNavigation?.Option3 ?? "", ImagenBase64 = ConvertToBase64(q.IdOpcionesNavigation?.Image3) },
                    new OptionDTO { Letra = "D", Texto = q.IdOpcionesNavigation?.Option4 ?? "", ImagenBase64 = ConvertToBase64(q.IdOpcionesNavigation?.Image4) },
                }
            }).ToList();
        }

        public Task SaveAnswersAsync(List<AnswerDTO> respuestas)
        {
            // Aquí se podría guardar en base de datos si quisieras
            // Por ahora asumimos que se guarda en memoria o no se persiste
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
            var letraAFamiliaId = new Dictionary<string, int> { { "A", 1 }, { "B", 2 }, { "C", 3 }, { "D", 4 } };

            if (!letraAFamiliaId.ContainsKey(letraDominante))
                return null;

            var familiaId = letraAFamiliaId[letraDominante];

            var familia = await _context.OlfactoryFamilies
                .FirstOrDefaultAsync(f => f.Id == familiaId);

            if (familia == null)
                return null;

            return new
            {
                letraDominante,
                familia.Id,
                familia.Nombre,
                familia.Description
            };
        }

        private string? ConvertToBase64(byte[]? imagen)
        {
            return imagen != null ? Convert.ToBase64String(imagen) : null;
        }

    }
}
