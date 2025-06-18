using alquimia.Data.Entities;
using alquimia.Services.Models;

namespace alquimia.Services.Helpers
{
    public static class FormulaNoteHelper
    {
        internal static FormulaNote CreateFormulaNote(POSTFormulaNoteDTO notes)
        {
            return new FormulaNote
            {
                NotaId1 = notes.Note1.Id,
                NotaId2 = notes.Note2?.Id,
                NotaId3 = notes.Note3?.Id,
                NotaId4 = notes.Note4?.Id
            };
        }
    }
}
