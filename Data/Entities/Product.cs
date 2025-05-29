using System;
using System.Collections.Generic;

namespace backendAlquimia.Data.Entities;

public partial class Product
{
    public int Id { get; set; }

    public int TipoProductoId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public int? IdProveedor { get; set; }

    public int? UsuarioId { get; set; }

    public virtual ICollection<Design> Designs { get; set; } = new List<Design>();

    public virtual ICollection<FinalEntity> FinalEntities { get; set; } = new List<FinalEntity>();

    public virtual User? IdProveedorNavigation { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual ProductType TipoProducto { get; set; } = null!;

    public virtual ICollection<UserProductReview> UserProductReviews { get; set; } = new List<UserProductReview>();

    public virtual ICollection<UserProduct> UserProducts { get; set; } = new List<UserProduct>();
}
