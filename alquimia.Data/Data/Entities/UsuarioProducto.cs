using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

public partial class UsuarioProducto
{
    [Key]
    public int Id { get; set; }

    public int ProductoId { get; set; }

    public int UsuarioId { get; set; }

    [ForeignKey("ProductoId")]
    [InverseProperty("UsuarioProductos")]
    public virtual Producto Producto { get; set; } = null!;

    [ForeignKey("UsuarioId")]
    [InverseProperty("UsuarioProductos")]
    public virtual Usuario Usuario { get; set; } = null!;
}
