using alquimia.Data.Entities;
using alquimia.Services.Interfaces;
using alquimia.Services.Models;
using alquimia.Services.QuizLogic;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Services
{
    public class QuizService : IQuizService
    {
        private readonly AlquimiaDbContext _context;
        private readonly INoteService _noteService;

        public QuizService(AlquimiaDbContext context, INoteService noteService)
        {
            _context = context;
            _noteService = noteService;
        }

        public async Task<List<QuestionDTO>> GetQuestionsAsync()
        {
            var preguntas = await _context.Questions.Include(q => q.IdOpcionesNavigation).ToListAsync();

            return preguntas.Select(q => new QuestionDTO
            {
                Id = q.Id,
                Pregunta = q.Pregunta,
                Opciones = new List<OptionDTO>
                {
                    new OptionDTO { Letra = "A", Texto = q.IdOpcionesNavigation?.Option1 ?? "", ImagenUrl = q.IdOpcionesNavigation?.Image1 },
                    new OptionDTO { Letra = "B", Texto = q.IdOpcionesNavigation?.Option2 ?? "", ImagenUrl = q.IdOpcionesNavigation?.Image2 },
                    new OptionDTO { Letra = "C", Texto = q.IdOpcionesNavigation?.Option3 ?? "", ImagenUrl = q.IdOpcionesNavigation?.Image3 },
                    new OptionDTO { Letra = "D", Texto = q.IdOpcionesNavigation?.Option4 ?? "", ImagenUrl = q.IdOpcionesNavigation?.Image4 },
                }
            }).ToList();
        }

        public Task SaveAnswersAsync(List<AnswerDTO> respuestas)
        {
            return Task.CompletedTask;
        }

        public async Task<QuizResponseDTO?> GetResultAsync(List<AnswerDTO> answers)
        {
            var subFamilyScores = new Dictionary<string, int>();

            foreach (var answer in answers.Where(a => a.QuestionId != 10))
            {
                int selectedIndex = answer.SelectedOption switch
                {
                    "A" => 1,
                    "B" => 2,
                    "C" => 3,
                    "D" => 4,
                    _ => int.TryParse(answer.SelectedOption, out int val) ? val : -1
                };

                var mapping = FamilyMappingData.Mappings.FirstOrDefault(m => m.QuestionId == answer.QuestionId && m.OptionIndex == selectedIndex);
                if (mapping != null)
                {
                    foreach (var subFam in mapping.Families)
                    {
                        subFamilyScores[subFam] = subFamilyScores.GetValueOrDefault(subFam, 0) + 1;
                    }
                }
            }

            var superFamilyScores = SuperFamilyMapping.Groups.ToDictionary(
                g => g.Key,
                g => g.Value.Sum(sf => subFamilyScores.GetValueOrDefault(sf, 0)));

            var dominantSuperFamily = superFamilyScores.OrderByDescending(s => s.Value).FirstOrDefault().Key;
            var allSubFamilies = SuperFamilyMapping.Groups[dominantSuperFamily];

            var orderedSubFamilies = allSubFamilies
                .Where(sf => subFamilyScores.ContainsKey(sf))
                .OrderByDescending(sf => subFamilyScores[sf])
                .ToList();

            var topMatchedSubFamilies = orderedSubFamilies.Take(2).ToList();
            var fallbackSubFamilies = orderedSubFamilies.Skip(2).ToList();

            var result = new QuizResponseDTO
            {
                SuperFamily = dominantSuperFamily,
                AllSubFamilies = allSubFamilies,
                TopMatchedSubFamilies = new List<SubFamilyDTO>(),
                Formulas = new List<ExampleFormulaDTO>()
            };

            foreach (var subFamily in topMatchedSubFamilies)
            {
                var family = await _context.OlfactoryFamilies.Include(f => f.Notes)
                    .FirstOrDefaultAsync(f => f.Nombre.ToLower() == subFamily.ToLower());

                if (family != null)
                {
                    result.TopMatchedSubFamilies.Add(new SubFamilyDTO
                    {
                        Name = subFamily,
                        CompatibleNotes = family.Notes.Select(n => n.Name).ToList()
                    });
                }
            }


            var usedNotes = new HashSet<string>();
            string? top = await GetNoteWithFallback(1, topMatchedSubFamilies, fallbackSubFamilies, usedNotes);
            string? heart = await GetNoteWithFallback(2, topMatchedSubFamilies, fallbackSubFamilies, usedNotes);
            string? baseNote = await GetNoteWithFallback(3, topMatchedSubFamilies, fallbackSubFamilies, usedNotes);

            result.Formulas.Add(new ExampleFormulaDTO
            {
                TopNote = top ?? "Nota compatible",
                HeartNote = heart ?? "Nota compatible",
                BaseNote = baseNote ?? "Nota compatible"
            });

            var intensity = answers.FirstOrDefault(a => a.QuestionId == 10)?.SelectedOption;
            result.ConcentrationType = intensity switch
            {
                "1" => "Body Splash",
                "2" => "Eau de Toilette",
                "3" => "Eau de Parfum",
                _ => "Desconocido"
            };

            return result;
        }

        private async Task<string?> GetNoteWithFallback(
            int pyramidLevel,
            List<string> topFamilies,
            List<string> fallbackFamilies,
            HashSet<string> usedNotes)
        {
            var usedNoteEntities = await _context.Notes
                .Where(n => usedNotes.Contains(n.Name))
                .ToListAsync();

            foreach (var fam in topFamilies.Concat(fallbackFamilies))
            {
                var family = await _context.OlfactoryFamilies
                    .Include(f => f.Notes)
                    .FirstOrDefaultAsync(f => f.Nombre.ToLower() == fam.ToLower());

                if (family == null) continue;

                foreach (var candidateNote in family.Notes
                    .Where(n => n.OlfactoryPyramidId == pyramidLevel && !usedNotes.Contains(n.Name)))
                {
                    bool isCompatible = true;

                    foreach (var usedNote in usedNoteEntities)
                    {
                        var isIncompatible = await _context.IncompatibleNotes.AnyAsync(incomp =>
                            (incomp.NotaId == usedNote.Id && incomp.NotaIncompatibleId == candidateNote.Id) ||
                            (incomp.NotaId == candidateNote.Id && incomp.NotaIncompatibleId == usedNote.Id));

                        if (isIncompatible)
                        {
                            isCompatible = false;
                            break;
                        }
                    }

                    if (isCompatible)
                    {
                        usedNotes.Add(candidateNote.Name);
                        return candidateNote.Name;
                    }
                }
            }

            return null;
        }
    }
}


