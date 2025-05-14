using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backendAlquimia.Data;
using backendAlquimia.Data.Entities;
using backendAlquimia.Services.Interfaces;

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
            var notas = await _notaService.ObtenerNotasDeSalidaAsync();
            return Ok(notas);
        }

        [HttpGet("notasDeCorazon")]
        public async Task<ActionResult<IEnumerable<Nota>>> GetNotasCorazon()
        {
            var notas = await _notaService.ObtenerNotasDeCorazonAsync();
            return Ok(notas);
        }
    }
}
