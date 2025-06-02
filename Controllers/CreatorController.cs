//using backendAlquimia.alquimia.Data;
using alquimia.Data.Data.Entities;
using alquimia.Services.Services;
using alquimia.Services.Services.Models;
using backendAlquimia.alquimia.Services.Interfaces;
using backendAlquimia.Models;
using Microsoft.AspNetCore.Mvc;
using Note = alquimia.Data.Data.Entities.Note;

namespace backendAlquimia.Controllers
{
    [Route("creator")]
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


        [HttpGet("intensities")]
        public async Task<ActionResult<IEnumerable<IntensitiesDTO>>> GetIntensities()
        {
            var intensities = await _formulaService.GetIntensitiesAsync();
            return Ok(intensities);
        }
    }
}
