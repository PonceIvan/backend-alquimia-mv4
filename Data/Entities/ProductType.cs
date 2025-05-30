using System;
using System.Collections.Generic;

namespace backendAlquimia.Data.Entities;

public partial class ProductType
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
