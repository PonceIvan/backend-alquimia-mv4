using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backendAlquimia.Models;

public partial class Intensity
{
    [Key]
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? Description { get; set; }

    [InverseProperty("Intensidad")]
    public virtual ICollection<Formula> Formulas { get; set; } = new List<Formula>();
}
