using alquimia.Data.Data.Entities;
using alquimia.Services.Services.Interfaces;
using alquimia.Services.Services.Models;
using backendAlquimia.alquimia.Services.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace backendAlquimia.Controllers
{
    [ApiController]
    [Route("quiz")]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpPost("resultado")]
        public ActionResult<List<QuizResultDTO>> ObtenerResultado([FromBody] List<int> respuestas)
        {
            if (respuestas == null || respuestas.Count != 10)
                return BadRequest("Se requieren exactamente 10 respuestas");

            var resultado = _quizService.GetQuizResult(respuestas);
            return Ok(resultado);
        }
    }
}
