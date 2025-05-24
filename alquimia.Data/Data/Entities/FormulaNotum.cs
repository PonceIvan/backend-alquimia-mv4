using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

public partial class FormulaNotum
{
    [Key]
    public int FormulaNotaId { get; set; }

    public int? PiramideOlfativaId { get; set; }

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
    [InverseProperty("FormulaNotumNotaId1Navigations")]
    public virtual Nota NotaId1Navigation { get; set; } = null!;

    [ForeignKey("NotaId2")]
    [InverseProperty("FormulaNotumNotaId2Navigations")]
    public virtual Nota? NotaId2Navigation { get; set; }

    [ForeignKey("NotaId3")]
    [InverseProperty("FormulaNotumNotaId3Navigations")]
    public virtual Nota? NotaId3Navigation { get; set; }

    [ForeignKey("NotaId4")]
    [InverseProperty("FormulaNotumNotaId4Navigations")]
    public virtual Nota? NotaId4Navigation { get; set; }

    [ForeignKey("PiramideOlfativaId")]
    [InverseProperty("FormulaNota")]
    public virtual PiramideOlfativa? PiramideOlfativa { get; set; }
}
