using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace alquimia.Data.Data.Entities;

public partial class User : IdentityUser<int>

[Index("NormalizedEmail", Name = "EmailIndex")]
public partial class User : IdentityUser
{
    public string Name { get; set; } = null!;

    public int? IdEstado { get; set; }

    public int? IdFormulas { get; set; }

    public int? IdQuiz { get; set; }

    public int? IdSuscripcion { get; set; }

    public bool EsProveedor { get; set; }

    [ForeignKey("IdEstado")]
    public virtual Status? IdEstadoNavigation { get; set; }

    [ForeignKey("IdFormulas")]
    public virtual Formula? IdFormulasNavigation { get; set; }

    [ForeignKey("IdQuiz")]
    public virtual Quiz? IdQuizNavigation { get; set; }

    [ForeignKey("IdSuscripcion")]
    public virtual Subscription? IdSuscripcionNavigation { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<FinalEntity> FinalEntities { get; set; } = new List<FinalEntity>();

    [InverseProperty("Usuario")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("IdProveedorNavigation")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<UserProductReview> UserProductReviews { get; set; } = new List<UserProductReview>();

    [InverseProperty("Usuario")]
    public virtual ICollection<UserProduct> UserProducts { get; set; } = new List<UserProduct>();

    [InverseProperty("IdProveedorNavigation")]
    public virtual ICollection<UserProviderReview> UserProviderReviewIdProveedorNavigations { get; set; } = new List<UserProviderReview>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<UserProviderReview> UserProviderReviewIdUsuarioNavigations { get; set; } = new List<UserProviderReview>();
}
