using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

[Table("OlfactoryPyramid")]
public partial class OlfactoryPyramid
{
    [Key]
    public int Id { get; set; }

    public string Sector { get; set; } = null!;

    public TimeSpan Duracion { get; set; }

    [InverseProperty("PiramideOlfativa")]
    public virtual ICollection<FormulaNote> FormulaNotes { get; set; } = new List<FormulaNote>();

    [InverseProperty("PiramideOlfativa")]
    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
