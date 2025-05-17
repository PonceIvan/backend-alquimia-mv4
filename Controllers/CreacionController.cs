using backendAlquimia.Data.Entities;
using backendAlquimia.Models;
using backendAlquimia.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backendAlquimia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreacionController : ControllerBase
    {
        private readonly INotaService _notaService;

        public CreacionController(INotaService notaService)
        {
            _notaService = notaService;
        }
        // tiene que ser la primera solicitud.
        [HttpGet("notasDeFondo")]
        public async Task<ActionResult<IEnumerable<Nota>>> GetNotasFondo()
        {
            var notas = await _notaService.ObtenerNotasDeFondoAgrupadasPorFamiliaAsync();
            return Ok(notas);
        }

        [HttpGet("notasDeCorazon")]
        public async Task<ActionResult<IEnumerable<Nota>>> GetNotasCorazon()
        {
            var notas = await _notaService.ObtenerNotasDeCorazonAgrupadasPorFamiliaAsync();
            return Ok(notas);
        }

        [HttpGet("notasDeSalida")]
        public async Task<ActionResult<IEnumerable<Nota>>> GetNotasSalida()
        {
            var notas = await _notaService.ObtenerNotasDeSalidaAgrupadasPorFamiliaAsync();
            return Ok(notas);
        }

        [HttpGet("grado")]
        public async Task<IActionResult> ObtenerGradoCompatibilidad(int notaAId, int notaBId)
        {
            var grado = await _notaService.CalcularCompatibilidadAsync(notaAId, notaBId);
            return Ok(new { grado });
        }

        [HttpPost("sugerencias")]
        public async Task<IActionResult> ObtenerNotasCompatiblesAsync([FromBody] NotasSeleccionadasDTO dto)
        {
            var compatibles = await _notaService.ObtenerNotasCompatiblesAsync(dto.ListaDeIdsSeleccionadas, dto.Sector);
            return Ok(compatibles);
        }

        // user envia nota
        // la recibo
        // proceso esa info para mostrarle las compatibles
        // actualizo y le devuelvo las compatibles
        // asi hasta llegar a 4 o hasta confirmar la seleccion

        // necesito que el front me haga post cada vez que el user arrastra la nota al frasco. no se como, cookie? session? local storage? 
    }
}
