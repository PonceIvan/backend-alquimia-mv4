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
                .Include(f => f.FormulaSalidaNavigation).ThenInclude(n => n.NotaId1Navigation).ThenInclude(n => n.OlfactoryFamily)
                .Include(f => f.FormulaSalidaNavigation).ThenInclude(n => n.NotaId1Navigation).ThenInclude(n => n.OlfactoryPyramid)
                .Include(f => f.FormulaSalidaNavigation).ThenInclude(n => n.NotaId2Navigation).ThenInclude(n => n.OlfactoryFamily)
                .Include(f => f.FormulaSalidaNavigation).ThenInclude(n => n.NotaId2Navigation).ThenInclude(n => n.OlfactoryPyramid)
                .Include(f => f.FormulaSalidaNavigation).ThenInclude(n => n.NotaId3Navigation).ThenInclude(n => n.OlfactoryFamily)
                .Include(f => f.FormulaSalidaNavigation).ThenInclude(n => n.NotaId3Navigation).ThenInclude(n => n.OlfactoryPyramid)
                .Include(f => f.FormulaSalidaNavigation).ThenInclude(n => n.NotaId4Navigation).ThenInclude(n => n.OlfactoryFamily)
                .Include(f => f.FormulaSalidaNavigation).ThenInclude(n => n.NotaId4Navigation).ThenInclude(n => n.OlfactoryPyramid)

                // Corazón
                .Include(f => f.FormulaCorazonNavigation).ThenInclude(n => n.NotaId1Navigation).ThenInclude(n => n.OlfactoryFamily)
                .Include(f => f.FormulaCorazonNavigation).ThenInclude(n => n.NotaId1Navigation).ThenInclude(n => n.OlfactoryPyramid)
                .Include(f => f.FormulaCorazonNavigation).ThenInclude(n => n.NotaId2Navigation).ThenInclude(n => n.OlfactoryFamily)
                .Include(f => f.FormulaCorazonNavigation).ThenInclude(n => n.NotaId2Navigation).ThenInclude(n => n.OlfactoryPyramid)
                .Include(f => f.FormulaCorazonNavigation).ThenInclude(n => n.NotaId3Navigation).ThenInclude(n => n.OlfactoryFamily)
                .Include(f => f.FormulaCorazonNavigation).ThenInclude(n => n.NotaId3Navigation).ThenInclude(n => n.OlfactoryPyramid)
                .Include(f => f.FormulaCorazonNavigation).ThenInclude(n => n.NotaId4Navigation).ThenInclude(n => n.OlfactoryFamily)
                .Include(f => f.FormulaCorazonNavigation).ThenInclude(n => n.NotaId4Navigation).ThenInclude(n => n.OlfactoryPyramid)

                // Fondo
                .Include(f => f.FormulaFondoNavigation).ThenInclude(n => n.NotaId1Navigation).ThenInclude(n => n.OlfactoryFamily)
                .Include(f => f.FormulaFondoNavigation).ThenInclude(n => n.NotaId1Navigation).ThenInclude(n => n.OlfactoryPyramid)
                .Include(f => f.FormulaFondoNavigation).ThenInclude(n => n.NotaId2Navigation).ThenInclude(n => n.OlfactoryFamily)
                .Include(f => f.FormulaFondoNavigation).ThenInclude(n => n.NotaId2Navigation).ThenInclude(n => n.OlfactoryPyramid)
                .Include(f => f.FormulaFondoNavigation).ThenInclude(n => n.NotaId3Navigation).ThenInclude(n => n.OlfactoryFamily)
                .Include(f => f.FormulaFondoNavigation).ThenInclude(n => n.NotaId3Navigation).ThenInclude(n => n.OlfactoryPyramid)
                .Include(f => f.FormulaFondoNavigation).ThenInclude(n => n.NotaId4Navigation).ThenInclude(n => n.OlfactoryFamily)
                .Include(f => f.FormulaFondoNavigation).ThenInclude(n => n.NotaId4Navigation).ThenInclude(n => n.OlfactoryPyramid);
        }
    }
}
