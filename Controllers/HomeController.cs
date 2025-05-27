using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backendAlquimia.Controllers
{
    [ApiController]
    [Route("home")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetRoot()
        {
            return Ok("Bienvenido a la API de Alquimia");
        }

        //[Authorize]
        [HttpGet("usuario")]
        public IActionResult ObtenerUsuarioInfo()
        {
            var identity = HttpContext.User.Identity;

            if (identity != null && identity.IsAuthenticated)
            {
                var nombre = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

                return Ok(new
                {
                    autenticado = true,
                    nombre,
                    email
                });
            }

            return Ok(new
            {
                autenticado = false,
                mensaje = "Usuario no autenticado"
            });
        }

    }
}
