using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

[Index("FamiliaOlfativaId", Name = "IX_Notas_FamiliaOlfativaId")]
[Index("PiramideOlfativaId", Name = "IX_Notas_PiramideOlfativaId")]
public partial class Nota
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    public int FamiliaOlfativaId { get; set; }

    [StringLength(50)]
    public string Descripcion { get; set; } = null!;

    public int PiramideOlfativaId { get; set; }

    [ForeignKey("FamiliaOlfativaId")]
    [InverseProperty("Nota")]
    public virtual FamiliasOlfativa FamiliaOlfativa { get; set; } = null!;

    [InverseProperty("NotaId1Navigation")]
    public virtual ICollection<FormulaNotum> FormulaNotumNotaId1Navigations { get; set; } = new List<FormulaNotum>();

    [InverseProperty("NotaId2Navigation")]
    public virtual ICollection<FormulaNotum> FormulaNotumNotaId2Navigations { get; set; } = new List<FormulaNotum>();

    [InverseProperty("NotaId3Navigation")]
    public virtual ICollection<FormulaNotum> FormulaNotumNotaId3Navigations { get; set; } = new List<FormulaNotum>();

    [InverseProperty("NotaId4Navigation")]
    public virtual ICollection<FormulaNotum> FormulaNotumNotaId4Navigations { get; set; } = new List<FormulaNotum>();

    [ForeignKey("PiramideOlfativaId")]
    [InverseProperty("Nota")]
    public virtual PiramideOlfativa PiramideOlfativa { get; set; } = null!;

    [ForeignKey("NotaId")]
    [InverseProperty("NotaNavigation")]
    public virtual ICollection<Nota> NotaIncompatibles { get; set; } = new List<Nota>();

    [ForeignKey("NotaIncompatibleId")]
    [InverseProperty("NotaIncompatibles")]
    public virtual ICollection<Nota> NotaNavigation { get; set; } = new List<Nota>();
}
