using System;
using System.Collections.Generic;

namespace backendAlquimia.Data.Entities;

public partial class OrderProduct
{
    public int PedidoProductoId { get; set; }

    public int? IdPedido { get; set; }

    public int? ProductosId { get; set; }

    public virtual Order? IdPedidoNavigation { get; set; }

    public virtual Product? Productos { get; set; }
}
