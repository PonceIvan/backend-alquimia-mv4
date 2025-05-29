using System;
using System.Collections.Generic;

namespace backendAlquimia.Data.Entities;

public partial class FinalEntity
{
    public int Id { get; set; }

    public int? IdUsuario { get; set; }

    public int? ProductosId { get; set; }

    public int? DesignId { get; set; }

    public virtual Design? Design { get; set; }

    public virtual User? IdUsuarioNavigation { get; set; }

    public virtual Product? Productos { get; set; }
}
