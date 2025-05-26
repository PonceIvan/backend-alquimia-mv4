using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

public partial class UserProviderReview
{
    [Key]
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public string Description { get; set; } = null!;

    public int IdProveedor { get; set; }

    public DateTime FechaPublicacion { get; set; }

    [ForeignKey("IdProveedor")]
    [InverseProperty("UserProviderReviewIdProveedorNavigations")]
    public virtual User IdProveedorNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("UserProviderReviewIdUsuarioNavigations")]
    public virtual User IdUsuarioNavigation { get; set; } = null!;
}
