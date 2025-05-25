using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

public partial class Usuario : IdentityUser
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? IdEstado { get; set; }

    public int? IdFormulas { get; set; }

    public int? IdQuiz { get; set; }

    public int? IdSuscripcion { get; set; }

    public bool EsProveedor { get; set; }

    [StringLength(256)]
    public string? UserName { get; set; }

    [StringLength(256)]
    public string? Email { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<EntidadFinal> EntidadFinals { get; set; } = new List<EntidadFinal>();

    [ForeignKey("IdEstado")]
    [InverseProperty("Usuarios")]
    public virtual Estado? IdEstadoNavigation { get; set; }

    [ForeignKey("IdFormulas")]
    [InverseProperty("Usuarios")]
    public virtual Formula? IdFormulasNavigation { get; set; }

    [ForeignKey("IdQuiz")]
    [InverseProperty("Usuarios")]
    public virtual Quiz? IdQuizNavigation { get; set; }

    [ForeignKey("IdSuscripcion")]
    [InverseProperty("Usuarios")]
    public virtual Suscripcion? IdSuscripcionNavigation { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<OpinionUsuarioProducto> OpinionUsuarioProductos { get; set; } = new List<OpinionUsuarioProducto>();

    [InverseProperty("IdProveedorNavigation")]
    public virtual ICollection<OpinionUsuarioProveedor> OpinionUsuarioProveedorIdProveedorNavigations { get; set; } = new List<OpinionUsuarioProveedor>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<OpinionUsuarioProveedor> OpinionUsuarioProveedorIdUsuarioNavigations { get; set; } = new List<OpinionUsuarioProveedor>();

    [InverseProperty("Usuario")]
    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    [InverseProperty("IdProveedorNavigation")]
    public virtual ICollection<Producto> ProductoIdProveedorNavigations { get; set; } = new List<Producto>();

    [InverseProperty("Usuario")]
    public virtual ICollection<Producto> ProductoUsuarios { get; set; } = new List<Producto>();

    [InverseProperty("Usuario")]
    public virtual ICollection<UsuarioProducto> UsuarioProductos { get; set; } = new List<UsuarioProducto>();
}
