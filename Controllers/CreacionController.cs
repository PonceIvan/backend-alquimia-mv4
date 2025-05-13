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

        //    // GET: api/Creacion/5
        //    [HttpGet("{id}")]
        //    public async Task<ActionResult<Nota>> GetNota(int id)
        //    {
        //        var nota = await _context.Notas.FindAsync(id);

        //        if (nota == null)
        //        {
        //            return NotFound();
        //        }

        //        return nota;
        //    }

        //    // PUT: api/Creacion/5
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPut("{id}")]
        //    public async Task<IActionResult> PutNota(int id, Nota nota)
        //    {
        //        if (id != nota.Id)
        //        {
        //            return BadRequest();
        //        }

        //        _context.Entry(nota).State = EntityState.Modified;

        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!NotaExists(id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return NoContent();
        //    }

        //    // POST: api/Creacion
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPost]
        //    public async Task<ActionResult<Nota>> PostNota(Nota nota)
        //    {
        //        _context.Notas.Add(nota);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction("GetNota", new { id = nota.Id }, nota);
        //    }

        //    // DELETE: api/Creacion/5
        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> DeleteNota(int id)
        //    {
        //        var nota = await _context.Notas.FindAsync(id);
        //        if (nota == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.Notas.Remove(nota);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }

        //    private bool NotaExists(int id)
        //    {
        //        return _context.Notas.Any(e => e.Id == id);
        //    }
    }
}
