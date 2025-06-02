using alquimia.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Services.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<Formula> IncludeFormulaNotesWithDetails(this IQueryable<Formula> query)
        {
            return query
                .Include(f => f.Intensidad)

                // Salida
                .Include(f => f.FormulaSalidaNavigation).ThenInclude(n => n.NotaId1Navigation).ThenInclude(n => n.FamiliaOlfativa)
                .Include(f => f.FormulaSalidaNavigation).ThenInclude(n => n.NotaId1Navigation).ThenInclude(n => n.PiramideOlfativa)
                .Include(f => f.FormulaSalidaNavigation).ThenInclude(n => n.NotaId2Navigation).ThenInclude(n => n.FamiliaOlfativa)
                .Include(f => f.FormulaSalidaNavigation).ThenInclude(n => n.NotaId2Navigation).ThenInclude(n => n.PiramideOlfativa)
                .Include(f => f.FormulaSalidaNavigation).ThenInclude(n => n.NotaId3Navigation).ThenInclude(n => n.FamiliaOlfativa)
                .Include(f => f.FormulaSalidaNavigation).ThenInclude(n => n.NotaId3Navigation).ThenInclude(n => n.PiramideOlfativa)
                .Include(f => f.FormulaSalidaNavigation).ThenInclude(n => n.NotaId4Navigation).ThenInclude(n => n.FamiliaOlfativa)
                .Include(f => f.FormulaSalidaNavigation).ThenInclude(n => n.NotaId4Navigation).ThenInclude(n => n.PiramideOlfativa)

                // Corazón
                .Include(f => f.FormulaCorazonNavigation).ThenInclude(n => n.NotaId1Navigation).ThenInclude(n => n.FamiliaOlfativa)
                .Include(f => f.FormulaCorazonNavigation).ThenInclude(n => n.NotaId1Navigation).ThenInclude(n => n.PiramideOlfativa)
                .Include(f => f.FormulaCorazonNavigation).ThenInclude(n => n.NotaId2Navigation).ThenInclude(n => n.FamiliaOlfativa)
                .Include(f => f.FormulaCorazonNavigation).ThenInclude(n => n.NotaId2Navigation).ThenInclude(n => n.PiramideOlfativa)
                .Include(f => f.FormulaCorazonNavigation).ThenInclude(n => n.NotaId3Navigation).ThenInclude(n => n.FamiliaOlfativa)
                .Include(f => f.FormulaCorazonNavigation).ThenInclude(n => n.NotaId3Navigation).ThenInclude(n => n.PiramideOlfativa)
                .Include(f => f.FormulaCorazonNavigation).ThenInclude(n => n.NotaId4Navigation).ThenInclude(n => n.FamiliaOlfativa)
                .Include(f => f.FormulaCorazonNavigation).ThenInclude(n => n.NotaId4Navigation).ThenInclude(n => n.PiramideOlfativa)

                // Fondo
                .Include(f => f.FormulaFondoNavigation).ThenInclude(n => n.NotaId1Navigation).ThenInclude(n => n.FamiliaOlfativa)
                .Include(f => f.FormulaFondoNavigation).ThenInclude(n => n.NotaId1Navigation).ThenInclude(n => n.PiramideOlfativa)
                .Include(f => f.FormulaFondoNavigation).ThenInclude(n => n.NotaId2Navigation).ThenInclude(n => n.FamiliaOlfativa)
                .Include(f => f.FormulaFondoNavigation).ThenInclude(n => n.NotaId2Navigation).ThenInclude(n => n.PiramideOlfativa)
                .Include(f => f.FormulaFondoNavigation).ThenInclude(n => n.NotaId3Navigation).ThenInclude(n => n.FamiliaOlfativa)
                .Include(f => f.FormulaFondoNavigation).ThenInclude(n => n.NotaId3Navigation).ThenInclude(n => n.PiramideOlfativa)
                .Include(f => f.FormulaFondoNavigation).ThenInclude(n => n.NotaId4Navigation).ThenInclude(n => n.FamiliaOlfativa)
                .Include(f => f.FormulaFondoNavigation).ThenInclude(n => n.NotaId4Navigation).ThenInclude(n => n.PiramideOlfativa);
        }
    }
}
