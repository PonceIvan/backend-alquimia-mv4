using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tests.Data.Entities;

public partial class Intensity
{
    [Key]
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    [InverseProperty("Intensidad")]
    public virtual ICollection<Formula> Formulas { get; set; } = new List<Formula>();
}
