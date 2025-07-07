using alquimia.Data.Entities;
using alquimia.Services;
using alquimia.Services.Interfaces;
using alquimia.Services.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace alquimia.Tests.TestServices
{
    public class QuizServiceTests
    {
        private AlquimiaDbContext GetTestDbContext()
        {
            var options = new DbContextOptionsBuilder<AlquimiaDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AlquimiaDbContext(options);

            context.OlfactoryFamilies.AddRange(new List<OlfactoryFamily>
    {
        new OlfactoryFamily
        {
            Id = 1,
            Nombre = "Marino",
            Description = "Notas frescas y acuáticas",
            Notes = new List<Note>
            {
                new Note
                {
                    Id = 1,
                    Name = "Algas",
                    OlfactoryPyramidId = 3,
                    OlfactoryFamilyId = 1,
                    Description = "Nota marina intensa"
                },
                new Note
                {
                    Id = 2,
                    Name = "Ozono",
                    OlfactoryPyramidId = 1,
                    OlfactoryFamilyId = 1,
                    Description = "Nota ozónica fresca"
                }
            }
        },
        new OlfactoryFamily
        {
            Id = 2,
            Nombre = "Cítrico",
            Description = "Notas vibrantes y frutales",
            Notes = new List<Note>
            {
                new Note
                {
                    Id = 3,
                    Name = "Limón",
                    OlfactoryPyramidId = 1,
                    OlfactoryFamilyId = 2,
                    Description = "Nota cítrica brillante"
                }
            }
        }
    });

            context.SaveChanges();
            return context;
        }

        private static List<AnswerDTO> GetSampleAnswersForFreshProfile()
        {
            return new List<AnswerDTO>
        {
            new AnswerDTO { QuestionId = 1, SelectedOption = "1" }, // Fresco, Floral
            new AnswerDTO { QuestionId = 4, SelectedOption = "1" }, // Fresca, Cítrico, Marino, Frutal
            new AnswerDTO { QuestionId = 6, SelectedOption = "4" }, // Fresca, Marino, Mentolado
            new AnswerDTO { QuestionId = 10, SelectedOption = "1" } // Body Splash
        };
        }

        [Fact]
        public async Task GetResultAsync_Returns_Valid_Response_For_Fresh_Profile()
        {
            var context = GetTestDbContext();
            var mockNoteService = new Mock<INoteService>();
            var quizService = new QuizService(context, mockNoteService.Object);
            var answers = GetSampleAnswersForFreshProfile();

            var result = await quizService.GetResultAsync(answers);

            Assert.NotNull(result);
            Assert.Equal("Fresca", result.SuperFamily);
            Assert.Contains("Marino", result.AllSubFamilies);
            Assert.Equal("Body Splash", result.ConcentrationType);
            Assert.True(result.Formulas.Any());
            Assert.True(result.TopMatchedSubFamilies.Any());
        }

        [Fact]
        public async Task GetResultAsync_Returns_Null_When_Empty_Answers()
        {

            var context = GetTestDbContext();
            var mockNoteService = new Mock<INoteService>();
            var quizService = new QuizService(context, mockNoteService.Object);
            var emptyAnswers = new List<AnswerDTO>();


            var result = await quizService.GetResultAsync(emptyAnswers);


            Assert.NotNull(result);
            Assert.Equal("Desconocido", result.ConcentrationType);
        }

        [Fact]
        public async Task GetResultAsync_Uses_Fallback_When_Not_Enough_Notes()
        {

            var context = GetTestDbContext();
            var mockNoteService = new Mock<INoteService>();
            var quizService = new QuizService(context, mockNoteService.Object);

            var answers = new List<AnswerDTO>
        {
            new AnswerDTO { QuestionId = 1, SelectedOption = "3" }, // Ámbar, Gourmand
            new AnswerDTO { QuestionId = 9, SelectedOption = "1" }, // Ámbar, Especiado, Gourmand
            new AnswerDTO { QuestionId = 10, SelectedOption = "3" } // Eau de Parfum
        };


            var result = await quizService.GetResultAsync(answers);


            Assert.NotNull(result);
            Assert.Equal("Oriental", result.SuperFamily);
            Assert.Equal("Eau de Parfum", result.ConcentrationType);
        }

        [Fact]
        public async Task GetQuestionsAsync_Returns_Question1_With_Options()
        {

            var options = new DbContextOptionsBuilder<AlquimiaDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AlquimiaDbContext(options);

            var opciones = new Option
            {
                Id = 1,
                Option1 = "Muy clara",
                Image1 = "\\quiz\\01-piel\\fair.png",
                Option2 = "Clara a media",
                Image2 = "\\quiz\\01-piel\\olive.png",
                Option3 = "Morena",
                Image3 = "\\quiz\\01-piel\\medium.png",
                Option4 = "Muy oscura",
                Image4 = "\\quiz\\01-piel\\dark.png"
            };

            var pregunta = new Question
            {
                Id = 1,
                Pregunta = "¿Cuál es tu tono de piel natural?",
                IdOpciones = 1,
                IdOpcionesNavigation = opciones
            };

            context.Options.Add(opciones);
            context.Questions.Add(pregunta);
            context.SaveChanges();

            var mockNoteService = new Mock<INoteService>();
            var service = new QuizService(context, mockNoteService.Object);


            var result = await service.GetQuestionsAsync();

            var question1 = result.FirstOrDefault(q => q.Id == 1);
            Assert.NotNull(question1);
            Assert.Equal("¿Cuál es tu tono de piel natural?", question1.Pregunta);
            Assert.Equal(4, question1.Opciones.Count);
            Assert.Contains(question1.Opciones, o => o.Texto == "Muy clara" && o.ImagenUrl == "\\quiz\\01-piel\\fair.png");
        }
    }
}
