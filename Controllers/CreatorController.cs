//using backendAlquimia.alquimia.Data;
using alquimia.Services.Services;
using alquimia.Services.Services.Interfaces;
using alquimia.Services.Services.Models;
using backendAlquimia.alquimia.Services.Interfaces;
using backendAlquimia.Models;
using Microsoft.AspNetCore.Mvc;
using Note = alquimia.Data.Data.Entities.Note;

namespace backendAlquimia.Controllers
{
    //[Authorize]
    [Route("creator")]
    [ApiController]
    public class CreatorController : ControllerBase
    {
        private readonly INoteService _notaService;
        private readonly IFormulaService _formulaService;
        private readonly IOlfactoryFamilyService _olfactoryFamilyService;

        public CreatorController(INoteService notaService, IFormulaService formulaService, IOlfactoryFamilyService olfactoryFamilyService)
        {
            _notaService = notaService;
            _formulaService = formulaService;
            _olfactoryFamilyService = olfactoryFamilyService;
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return Ok("Bienvenido a crear tu perfume");
        }

        [HttpGet("start")]
        public IActionResult Start()
        {
            return Ok("Vas a crear tu perfume ahora. Arrastra las notas al frasco");
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
        public async Task<ActionResult<IEnumerable<IntensityDTO>>> GetIntensities()
        {
            var intensities = await _formulaService.GetIntensitiesAsync();
            return Ok(intensities);
        }

        [HttpPost("save-formula")]
        public async Task<IActionResult> SaveFormula([FromBody] POSTFormulaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var formulaId = await _formulaService.SaveAsync(dto);
                return Ok(new { formulaId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error al guardar la fórmula", details = ex.Message });
            }
        }

        [HttpGet("get-formula/{id}")]
        public async Task<IActionResult> GetFormulaById(int id)
        {
            var formula = await _formulaService.GetFormulaByIdAsync(id);
            return Ok(formula);

        }

        [HttpGet("note-info/{id}")]
        public async Task<IActionResult> GetNoteInfo(int id)
        {
            var note = await _notaService.GetNoteInfoAsync(id);
            return Ok(note);
        }

        [HttpGet("family-info/{id}")]
        public async Task<IActionResult> GetOlfactoryFamilyInfo(int id)
        {
            var family = await _olfactoryFamilyService.GetOlfactoryFamilyInfoAsync(id);
            return Ok(family);
        }
    }
}
