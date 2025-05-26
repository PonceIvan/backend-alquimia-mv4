using System.Security.Claims;
using alquimia.Data.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendAlquimia.Controllers
{
    [ApiController]
    [Route("quiz")]
    public class QuizController : ControllerBase
    {
        private readonly AlquimiaDbContext _context;
        private readonly ILogger<QuizController> _logger;

        public QuizController(AlquimiaDbContext context, ILogger<QuizController> logger)
        {
            _context = context;
            _logger = logger;
        }


        //public async Task<IActionResult> ObtenerPreguntas()
        //{
        //    var preguntas = await _context.Questions
        //        .Include(p => p.Options)
        //        .ToListAsync();

        //    var resultado = preguntas.Select(p => new
        //    {
        //        id = p.Id,
        //        texto = p.Texto,
        //        opciones = p.Opciones.Select(o => new
        //        {
        //            id = o.Id,
        //            texto = o.Texto,
        //            imagenBase64 = o.Imagen != null ? Convert.ToBase64String(o.Imagen) : null
        //        }).ToList()
        //    });

        //    return Ok(resultado);
        //}


        //[HttpPost("responder")]
        //public async Task<IActionResult> GuardarRespuestas([FromBody] List<RespuestaUsuario> respuestas)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // o lo que uses para identificar
        //    if (userId == null)
        //        return Unauthorized();

        //    foreach (var respuesta in respuestas)
        //    {
        //        // Aquí podrías hacer validaciones si es necesario
        //        respuesta.UsuarioId = int.Parse(userId);
        //        _context.RespuestasUsuarios.Add(respuesta);
        //    }

        //    await _context.SaveChangesAsync();
        //    return Ok(new { mensaje = "Respuestas guardadas correctamente." });
        //}

        //[HttpGet("resultado")]
        //public async Task<IActionResult> ObtenerResultado()
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    if (userId == null)
        //        return Unauthorized();

        //    int uid = int.Parse(userId);

        //    // Por ejemplo, suponiendo que ya calculaste y guardaste el resultado antes
        //    var resultado = await _context.ResultadosFinales
        //        .FirstOrDefaultAsync(r => r.UsuarioId == uid);

        //    if (resultado == null)
        //        return NotFound(new { mensaje = "Resultado no encontrado para este usuario." });

        //    return Ok(new { resultado = resultado.TextoResultado });
        //}
    }
}
