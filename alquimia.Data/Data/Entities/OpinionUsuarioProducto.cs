using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

[Table("OpinionUsuarioProducto")]
public partial class OpinionUsuarioProducto
{
    [Key]
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public string Description { get; set; } = null!;

    public int? IdProducto { get; set; }

    public DateTime FechaPublicacion { get; set; }

    [ForeignKey("IdProducto")]
    [InverseProperty("OpinionUsuarioProductos")]
    public virtual Producto? IdProductoNavigation { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("OpinionUsuarioProductos")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
