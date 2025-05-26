using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tests.Data.Entities;

public partial class Order
{
    public int? UsuarioId { get; set; }

    [Key]
    public int Id { get; set; }

    public int? EstadoId { get; set; }

    [ForeignKey("EstadoId")]
    [InverseProperty("Orders")]
    public virtual Status? Estado { get; set; }

    [InverseProperty("IdPedidoNavigation")]
    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    [ForeignKey("UsuarioId")]
    [InverseProperty("Orders")]
    public virtual User? Usuario { get; set; }
}
