using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

[Table("Pedido")]
public partial class Pedido
{
    public int? UsuarioId { get; set; }

    [Key]
    public int Id { get; set; }

    public int? EstadoId { get; set; }

    [ForeignKey("EstadoId")]
    [InverseProperty("InverseEstado")]
    public virtual Pedido? Estado { get; set; }

    [InverseProperty("Estado")]
    public virtual ICollection<Pedido> InverseEstado { get; set; } = new List<Pedido>();

    [InverseProperty("IdPedidoNavigation")]
    public virtual ICollection<PedidoProducto> PedidoProductos { get; set; } = new List<PedidoProducto>();

    [ForeignKey("UsuarioId")]
    [InverseProperty("Pedidos")]
    public virtual Usuario? Usuario { get; set; }
}
