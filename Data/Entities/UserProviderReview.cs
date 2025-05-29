using System;
using System.Collections.Generic;

namespace backendAlquimia.Data.Entities;

public partial class UserProviderReview
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public string Description { get; set; } = null!;

    public int IdProveedor { get; set; }

    public DateTime FechaPublicacion { get; set; }

    public virtual User IdProveedorNavigation { get; set; } = null!;

    public virtual User IdUsuarioNavigation { get; set; } = null!;
}
