//using backendAlquimia.alquimia.Data;
using alquimia.Data.Data.Entities;
using backendAlquimia.alquimia.Services.Interfaces;
using backendAlquimia.Models;
using Microsoft.AspNetCore.Mvc;

namespace backendAlquimia.Controllers
{
    [Route("creador")]
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


    }
}
