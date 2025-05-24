using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

public partial class Opcione
{
    [Key]
    public int Id { get; set; }

    [StringLength(256)]
    public string? Option1 { get; set; }

    [StringLength(256)]
    public string? Option2 { get; set; }

    [StringLength(256)]
    public string? Option3 { get; set; }

    [StringLength(256)]
    public string? Option4 { get; set; }

    [InverseProperty("IdOpcionesNavigation")]
    public virtual ICollection<Pregunta> Pregunta { get; set; } = new List<Pregunta>();
}
