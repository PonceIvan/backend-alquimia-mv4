using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tests.Data.Entities;

[Table("Subscription")]
public partial class Subscription
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Monto { get; set; }

    public int? IdEstado { get; set; }

    [ForeignKey("IdEstado")]
    [InverseProperty("Subscriptions")]
    public virtual Status? IdEstadoNavigation { get; set; }

    [InverseProperty("IdSuscripcionNavigation")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
