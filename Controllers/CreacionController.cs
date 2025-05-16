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

        [HttpGet("notasDeSalida")]
        public async Task<ActionResult<IEnumerable<Nota>>> GetNotasSalida()
        {
            var notas = await _notaService.ObtenerNotasDeSalidaAgrupadasPorFamiliaAsync();
            return Ok(notas);
        }

        [HttpGet("notasDeCorazon")]
        public async Task<ActionResult<IEnumerable<Nota>>> GetNotasCorazon()
        {
            var notas = await _notaService.ObtenerNotasDeCorazonAgrupadasPorFamiliaAsync();
            return Ok(notas);
        }

        // tiene que ser la primera solicitud.
        [HttpGet("notasDeFondo")]
        public async Task<ActionResult<IEnumerable<Nota>>> GetNotasFondo()
        {
            var notas = await _notaService.ObtenerNotasDeFondoAgrupadasPorFamiliaAsync();
            return Ok(notas);
        }

        [HttpGet("grado")]
        public async Task<IActionResult> ObtenerGradoCompatibilidad(int notaAId, int notaBId)
        {
            var grado = await _notaService.CalcularCompatibilidadAsync(notaAId, notaBId);
            return Ok(new { grado });
        }

        //[HttpPost("validarSeleccion")]
        //// despues de que agrega 4 notas al frasco (o menos, no importa) pone confirmar y se envia un formulario que tiene una lista de esas 4 notas.
        //public async Task<IActionResult> ValidarNotaSeleccionada([FromBody] NotasSeleccionadasDTO NotasSeleccionadas)
        //{
        //    var EsCompatible = _notaService.EsCompatibleConSeleccionAsync(NotasSeleccionadas.NuevaNotaId, NotasSeleccionadas.ListaDeIdsSeleccionadas);
        //    return Ok(new { esCompatible = EsCompatible });
        //}

        [HttpPost("sugerencias")]
        public async Task<IActionResult> ObtenerNotasCompatiblesAsync([FromBody] NotasSeleccionadasDTO NotasSeleccionadas)
        {
            var compatibles = await _notaService.ObtenerNotasCompatiblesAsync(NotasSeleccionadas.ListaDeIdsSeleccionadas);
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
