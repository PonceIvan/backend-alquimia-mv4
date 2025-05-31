//using backendAlquimia.alquimia.Data;
using alquimia.Data.Data.Entities;
using alquimia.Services.Services;
using alquimia.Services.Services.Models;
using backendAlquimia.alquimia.Services.Interfaces;
using backendAlquimia.Models;
using Microsoft.AspNetCore.Mvc;

namespace backendAlquimia.Controllers
{
    [Route("creator")]
    [ApiController]
    public class CreatorController : ControllerBase
    {
        private readonly INoteService _notaService;

        public CreatorController(INoteService notaService)
        {
            _notaService = notaService;
        }

        [HttpGet("base-notes")]
        public async Task<ActionResult<IEnumerable<Note>>> GetBaseNotes()
        {
            List<NotesGroupedByFamilyDTO> notes = await _notaService.GetBaseNotesGroupedByFamilyAsync();
            return Ok(notes);
        }

        [HttpGet("heart-notes")]
        public async Task<ActionResult<IEnumerable<Note>>> GetHeartNotes()
        {
            List<NotesGroupedByFamilyDTO> notas = await _notaService.GetHeartNotesGroupedByFamilyAsync();
            return Ok(notas);
        }

        [HttpGet("top-notes")]
        public async Task<ActionResult<IEnumerable<Note>>> GetTopNotes()
        {
            List<NotesGroupedByFamilyDTO> notas = await _notaService.GetTopNotesGroupedByFamilyAsync();
            return Ok(notas);
        }

        [HttpPost("compatibilities")]
        public async Task<IActionResult> PostCompatibleNotes([FromBody] SelectedNotesDTO dto)
        {
            var compatibles = await _notaService.GetCompatibleNotesAsync(dto.ListaDeIdsSeleccionadas, dto.Sector);
            return Ok(compatibles);
        }

        //[HttpPost("envase-pdf")]
        //public IActionResult DescargarPdf([FromBody] DesignDTO dto)
        //{
        //    var pdfBytes = DesignLabelService.CrearPdfDesdeDesign(dto);
        //    return File(pdfBytes, "application/pdf", "mi-diseño.pdf");
        //}


    }
}
