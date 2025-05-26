using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tests.Data.Entities;

public partial class UserProduct
{
    [Key]
    public int Id { get; set; }

    public int ProductoId { get; set; }

    public int UsuarioId { get; set; }

    [ForeignKey("ProductoId")]
    [InverseProperty("UserProducts")]
    public virtual Product Producto { get; set; } = null!;

    [ForeignKey("UsuarioId")]
    [InverseProperty("UserProducts")]
    public virtual User Usuario { get; set; } = null!;
}
