using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

[Table("Suscripcion")]
public partial class Suscripcion
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Monto { get; set; }

    public int? IdEstado { get; set; }

    [ForeignKey("IdEstado")]
    [InverseProperty("Suscripcions")]
    public virtual Estado? IdEstadoNavigation { get; set; }

    [InverseProperty("IdSuscripcionNavigation")]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
