using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

public partial class ProductType
{
    [Key]
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    [InverseProperty("TipoProducto")]
    public virtual ICollection<Design> Designs { get; set; } = new List<Design>();

    [InverseProperty("TipoProducto")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
