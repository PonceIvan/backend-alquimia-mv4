using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

[Table("PedidoProducto")]
public partial class PedidoProducto
{
    [Key]
    public int PedidoProductoId { get; set; }

    public int? IdPedido { get; set; }

    public int? ProductosId { get; set; }

    [ForeignKey("IdPedido")]
    [InverseProperty("PedidoProductos")]
    public virtual Pedido? IdPedidoNavigation { get; set; }

    [ForeignKey("ProductosId")]
    [InverseProperty("PedidoProductos")]
    public virtual Producto? Productos { get; set; }
}
