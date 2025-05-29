using System;
using System.Collections.Generic;

namespace backendAlquimia.Data.Entities;

public partial class FormulaNote
{
    public int FormulaNotaId { get; set; }

    public int NotaId1 { get; set; }

    public int? NotaId2 { get; set; }

    public int? NotaId3 { get; set; }

    public int? NotaId4 { get; set; }

    public virtual ICollection<Formula> FormulaFormulaCorazonNavigations { get; set; } = new List<Formula>();

    public virtual ICollection<Formula> FormulaFormulaFondoNavigations { get; set; } = new List<Formula>();

    public virtual ICollection<Formula> FormulaFormulaSalidaNavigations { get; set; } = new List<Formula>();

    public virtual Note NotaId1Navigation { get; set; } = null!;

    public virtual Note? NotaId2Navigation { get; set; }

    public virtual Note? NotaId3Navigation { get; set; }

    public virtual Note? NotaId4Navigation { get; set; }
}
