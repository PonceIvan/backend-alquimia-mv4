using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

[Table("FormulaNote")]
public partial class FormulaNote
{
    [Key]
    public int FormulaNotaId { get; set; }

    public int NotaId1 { get; set; }

    public int? NotaId2 { get; set; }

    public int? NotaId3 { get; set; }

    public int? NotaId4 { get; set; }

    [InverseProperty("FormulaCorazonNavigation")]
    public virtual ICollection<Formula> FormulaFormulaCorazonNavigations { get; set; } = new List<Formula>();

    [InverseProperty("FormulaFondoNavigation")]
    public virtual ICollection<Formula> FormulaFormulaFondoNavigations { get; set; } = new List<Formula>();

    [InverseProperty("FormulaSalidaNavigation")]
    public virtual ICollection<Formula> FormulaFormulaSalidaNavigations { get; set; } = new List<Formula>();

    [ForeignKey("NotaId1")]
    [InverseProperty("FormulaNoteNotaId1Navigations")]
    public virtual Note NotaId1Navigation { get; set; } = null!;

    [ForeignKey("NotaId2")]
    [InverseProperty("FormulaNoteNotaId2Navigations")]
    public virtual Note? NotaId2Navigation { get; set; }

    [ForeignKey("NotaId3")]
    [InverseProperty("FormulaNoteNotaId3Navigations")]
    public virtual Note? NotaId3Navigation { get; set; }

    [ForeignKey("NotaId4")]
    [InverseProperty("FormulaNoteNotaId4Navigations")]
    public virtual Note? NotaId4Navigation { get; set; }
}
