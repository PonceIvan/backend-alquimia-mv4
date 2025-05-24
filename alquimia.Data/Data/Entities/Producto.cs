using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

public partial class Producto
{
    [Key]
    public int Id { get; set; }

    public int TipoProductoId { get; set; }

    [StringLength(30)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    public string Description { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    public int Stock { get; set; }

    public int? IdProveedor { get; set; }

    public int? UsuarioId { get; set; }

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<Design> Designs { get; set; } = new List<Design>();

    [InverseProperty("Productos")]
    public virtual ICollection<EntidadFinal> EntidadFinals { get; set; } = new List<EntidadFinal>();

    [ForeignKey("IdProveedor")]
    [InverseProperty("ProductoIdProveedorNavigations")]
    public virtual Usuario? IdProveedorNavigation { get; set; }

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<OpinionUsuarioProducto> OpinionUsuarioProductos { get; set; } = new List<OpinionUsuarioProducto>();

    [InverseProperty("Productos")]
    public virtual ICollection<PedidoProducto> PedidoProductos { get; set; } = new List<PedidoProducto>();

    [ForeignKey("TipoProductoId")]
    [InverseProperty("Productos")]
    public virtual TipoProducto TipoProducto { get; set; } = null!;

    [ForeignKey("UsuarioId")]
    [InverseProperty("ProductoUsuarios")]
    public virtual Usuario? Usuario { get; set; }

    [InverseProperty("Producto")]
    public virtual ICollection<UsuarioProducto> UsuarioProductos { get; set; } = new List<UsuarioProducto>();
}
