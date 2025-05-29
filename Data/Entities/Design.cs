using System;
using System.Collections.Generic;

namespace backendAlquimia.Data.Entities;

public partial class Design
{
    public int Id { get; set; }

    public int? TipoProductoId { get; set; }

    public int? IdProducto { get; set; }

    public string Text { get; set; } = null!;

    public int? Volume { get; set; }

    public string? Image { get; set; }

    public string? Shape { get; set; }

    public string? LabelColor { get; set; }

    public string? Typography { get; set; }

    public string? TextColor { get; set; }

    public virtual ICollection<FinalEntity> FinalEntities { get; set; } = new List<FinalEntity>();

    public virtual Product? IdProductoNavigation { get; set; }
}
