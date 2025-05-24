using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

[Table("PiramideOlfativa")]
public partial class PiramideOlfativa
{
    [Key]
    public int Id { get; set; }

    public string Sector { get; set; } = null!;

    public TimeOnly Duracion { get; set; }

    [InverseProperty("PiramideOlfativa")]
    public virtual ICollection<FormulaNotum> FormulaNota { get; set; } = new List<FormulaNotum>();

    [InverseProperty("PiramideOlfativa")]
    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();
}
