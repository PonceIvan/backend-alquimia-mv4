using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

[Table("OpinionUsuarioProveedor")]
public partial class OpinionUsuarioProveedor
{
    [Key]
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public string Description { get; set; } = null!;

    public int IdProveedor { get; set; }

    public DateTime FechaPublicacion { get; set; }

    [ForeignKey("IdProveedor")]
    [InverseProperty("OpinionUsuarioProveedorIdProveedorNavigations")]
    public virtual Usuario IdProveedorNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("OpinionUsuarioProveedorIdUsuarioNavigations")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
