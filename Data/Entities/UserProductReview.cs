using System;
using System.Collections.Generic;

namespace backendAlquimia.Data.Entities;

public partial class UserProductReview
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public string Description { get; set; } = null!;

    public int? IdProducto { get; set; }

    public DateTime FechaPublicacion { get; set; }

    public virtual Product? IdProductoNavigation { get; set; }

    public virtual User IdUsuarioNavigation { get; set; } = null!;
}
