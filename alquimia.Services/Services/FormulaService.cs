using backendAlquimia.alquimia.Services.Interfaces;
using alquimia.Data.Data.Entities;
using backendAlquimia.Models;
using Microsoft.EntityFrameworkCore;

namespace backendAlquimia.alquimia.Services.Services
{
    public class FormulaService : IFormulaService
    {
        private readonly AlquimiaDbContext _context;
        public FormulaService(AlquimiaDbContext context)
        {
            _context = context;
        }

        //public async Task<GETFormulaDTO> guardar(POSTFormulaDTO dto)
        //{
        //    var notasSalida = await _context.Notas
        //        .Where(n => dto.NotasSalidaIds.Contains(n.Id) && n.PiramideOlfativa.Id == 1)
        //        .ToListAsync();

        //    var notasCorazon = await _context.Notas
        //        .Where(n => dto.NotasCorazonIds.Contains(n.Id) && n.PiramideOlfativa.Id == 2)
        //        .ToListAsync();

        //    var notasFondo = await _context.Notas
        //        .Where(n => dto.NotasFondoIds.Contains(n.Id) && n.PiramideOlfativa.Id == 3)
        //        .ToListAsync();

        //    if (notasSalida.Count != dto.NotasSalidaIds.Count ||
        //        notasCorazon.Count != dto.NotasCorazonIds.Count ||
        //        notasFondo.Count != dto.NotasFondoIds.Count)
        //    {
        //        throw new Exception("Una o más notas no existen o no pertenecen al sector correspondiente.");
        //    }

        //    //var formula = new FormulaNotum
        //    //{
        //    //    FormulaSalida = notasSalida,
        //    //    NotaCorazon = notasCorazon,
        //    //    NotaFondo = notasFondo
        //    //};

        //    _context.FormulaNota.Add(formula);
        //    await _context.SaveChangesAsync();
        //    var formulaN = new Formula
        //    {
        //        Id = dto.Id,
        //        IntensidadId = dto.IdIntensidad,
        //        FormulaFondo = dto.IdCreador,
        //        FormulaCorazon = dto.IdCreador,
        //        FormulaSalida = dto.IdCreador,
        //        ConcentracionAlcohol = 0,
        //        ConcentracionAgua = 0,
        //        ConcentracionEsencia = 0
        //    };

        //    _context.Formulas.Add(formula);
        //    await _context.SaveChangesAsync();

        //    return new GETFormulaDTO
        //    {
        //        Id = formula.Id,
        //        NotasSalidaIds = dto.NotasSalidaIds,
        //        NotasCorazonIds = dto.NotasCorazonIds,
        //        NotasFondoIds = dto.NotasFondoIds,
        //        IdIntensidad = dto.IdIntensidad,
        //        IdCreador = dto.IdCreador,
        //        ConcentracionAlcohol = 0,
        //        ConcentracionAgua = 0,
        //        ConcentracionEsencia = 0
        //    };
        //}

        public async Task<List<IntensidadDTO>> ObtenerIntensidadAsync()
        {
            return await _context.Intensities
                .Select
                (x => new IntensidadDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                }).ToListAsync();
        }

        //public async Task<GETFormulaDTO> ObtenerPorId(int id)
        //{
        //    var formula = await _context.Formulas
        //        .Include(f => f.Combinacion)
        //        .Include(f => f.Combinacion.NotaSalida)
        //        .Include(f => f.Combinacion.NotaCorazon)
        //        .Include(f => f.Combinacion.NotaFondo)
        //        .FirstOrDefaultAsync(f => f.Id == id);

        //    if (formula == null)
        //        return null;

        //    return new GETFormulaDTO
        //    {
        //        Id = formula.Id,
        //        NotasSalidaIds = formula.Combinacion.NotaSalida.Select(n => n.Id).ToList(),
        //        NotasCorazonIds = formula.Combinacion.NotaCorazon.Select(n => n.Id).ToList(),
        //        NotasFondoIds = formula.Combinacion.NotaFondo.Select(n => n.Id).ToList(),
        //        IdIntensidad = formula.IntensidadId,
        //        IdCreador = formula.CreadorId,
        //        ConcentracionAlcohol = formula.ConcentracionAlcohol,
        //        ConcentracionAgua = formula.ConcentracionAgua,
        //        ConcentracionEsencia = formula.ConcentracionEsencia
        //    };
        //}


        private double calcularConcentracionAgua()
        {
            return 0.0;
        }
        private double calcularConcentracionAlcohol()
        {
            return 0.0;
        }

        public async Task<List<IntensitiesDTO>> GetIntensitiesAsync()
        {
            return await _context.Intensities
                .Select
                (x => new IntensitiesDTO
        {
            return 0.0;
        }
    }
}
