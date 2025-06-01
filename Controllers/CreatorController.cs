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

        [HttpPost("envase-pdf")]
        public IActionResult DescargarPdf([FromBody] DesignDTO dto)
        {
            var pdfBytes = DesignLabelService.CrearPdfDesdeDesign(dto);
            return File(pdfBytes, "application/pdf", "myDesign.pdf");
        }


        [HttpGet("intensities")]
        public async Task<ActionResult<IEnumerable<IntensitiesDTO>>> GetIntensities()
        {
            var intensities = await _formulaService.GetIntensitiesAsync();
            return Ok(intensities);
        }

        [HttpPost("save-formula")]
        public async Task<IActionResult> SaveFormula([FromBody] POSTFormulaDTO formula)
        {
            try
            {
                int formulaId = await _formulaService.SaveAsync(formula);
                return CreatedAtAction(nameof(GetFormulaById), new { id = formulaId }, new { FormulaId = formulaId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al guardar la fórmula", detail = ex.Message });
            }
        }

        //Ejemplo para usar en CreatedAtAction si ya lo tenés:
        [HttpGet("get-formula/{id}")]
        public async Task<IActionResult> GetFormulaById(int id)
        {
            var formula = await _formulaService.GetFormulaByIdAsync(id);
            if (formula == null)
                return NotFound();

            return Ok(formula);
        }
    }
}
