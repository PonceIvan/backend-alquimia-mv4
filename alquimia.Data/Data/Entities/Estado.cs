using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

[Table("Estado")]
public partial class Estado
{
    [Key]
    public int Id { get; set; }

    [StringLength(30)]
    public string Description { get; set; } = null!;

    [InverseProperty("IdEstadoNavigation")]
    public virtual ICollection<Suscripcion> Suscripcions { get; set; } = new List<Suscripcion>();

    [InverseProperty("IdEstadoNavigation")]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
