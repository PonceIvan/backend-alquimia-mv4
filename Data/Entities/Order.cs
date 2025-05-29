using System;
using System.Collections.Generic;

namespace backendAlquimia.Data.Entities;

public partial class Order
{
    public int? UsuarioId { get; set; }

    public int Id { get; set; }

    public int? EstadoId { get; set; }

    public virtual Status? Estado { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual User? Usuario { get; set; }
}
