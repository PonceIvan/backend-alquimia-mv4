using System;
using System.Collections.Generic;

namespace backendAlquimia.Data.Entities;

public partial class UserProduct
{
    public int Id { get; set; }

    public int ProductoId { get; set; }

    public int UsuarioId { get; set; }

    public virtual Product Producto { get; set; } = null!;

    public virtual User Usuario { get; set; } = null!;
}
