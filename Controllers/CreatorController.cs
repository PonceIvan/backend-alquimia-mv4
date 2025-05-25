//using backendAlquimia.alquimia.Data;
using backendAlquimia.Models;
using backendAlquimia.alquimia.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using alquimia.Data.Data.Entities;

namespace backendAlquimia.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CreatorController : ControllerBase
    {
        private readonly INoteService _notaService;
        private readonly IFormulaService _formulaService;

        public CreatorController(INoteService notaService, IFormulaService formulaService)
        {
            _notaService = notaService;
            _formulaService = formulaService;
        }
        // tiene que ser la primera solicitud.
        [HttpGet("notasDeFondo")]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotasFondo()
        {
            List<NotasPorFamiliaDTO> notas = await _notaService.ObtenerNotasDeFondoAgrupadasPorFamiliaAsync();
            return Ok(notas);
        }

        [HttpGet("notasDeCorazon")]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotasCorazon()
        {
            List<NotasPorFamiliaDTO> notas = await _notaService.ObtenerNotasDeCorazonAgrupadasPorFamiliaAsync();
            return Ok(notas);
        }

        [HttpGet("notasDeSalida")]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotasSalida()
        {
            List<NotasPorFamiliaDTO> notas = await _notaService.ObtenerNotasDeSalidaAgrupadasPorFamiliaAsync();
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

        [HttpGet("intensidad")]
        public async Task<IActionResult> GetIntensidad()
        {
            List<IntensidadDTO> intensidad = await _formulaService.ObtenerIntensidadAsync();
            return Ok(intensidad);
        }

        //[HttpPost("formular")]
        //public async Task<IActionResult> finalizarcreacion([FromBody] POSTFormulaDTO dto)
        //{
        //    GETFormulaDTO formulaguardada = await _formulaService.guardar(dto);
        //    return CreatedAtAction(
        //        nameof(obtenerformulaporid),
        //        new { id = formulaguardada.Id },
        //        formulaguardada
        //        );
        //}

        //[HttpGet("formulas/{id}")]
        //public async Task<IActionResult> obtenerformulaporid(int id)
        //{
        //    var formula = await _formulaService.ObtenerPorId(id);
        //    if (formula == null) return NotFound();
        //    return Ok(formula);
        //}



        // user envia nota
        // la recibo
        // proceso esa info para mostrarle las compatibles
        // actualizo y le devuelvo las compatibles
        // asi hasta llegar a 4 o hasta confirmar la seleccion

        // necesito que el front me haga post cada vez que el user arrastra la nota al frasco. no se como, cookie? session? local storage? 
        // necesito que el front me haga post cada vez que el user arrastra la nota al frasco. no se como, cookie? session? local storage?. UNa vez que el usuario ya selecciono todas las notas, hace click en CONFIRMAR Y AHI SE HACE esa solicitud /creacion/confirmar.

    }
}
