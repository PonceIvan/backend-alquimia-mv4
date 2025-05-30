using System;
using System.Collections.Generic;

namespace backendAlquimia.Data.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? IdEstado { get; set; }

    public int? IdFormulas { get; set; }

    public int? IdQuiz { get; set; }

    public int? IdSuscripcion { get; set; }

    public bool EsProveedor { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public string? NormalizedEmail { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetUserRole> AspNetUserRoles { get; set; } = new List<AspNetUserRole>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    public virtual ICollection<FinalEntity> FinalEntities { get; set; } = new List<FinalEntity>();

    public virtual Status? IdEstadoNavigation { get; set; }

    public virtual Formula? IdFormulasNavigation { get; set; }

    public virtual Quiz? IdQuizNavigation { get; set; }

    public virtual Subscription? IdSuscripcionNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<UserProductReview> UserProductReviews { get; set; } = new List<UserProductReview>();

    public virtual ICollection<UserProduct> UserProducts { get; set; } = new List<UserProduct>();

    public virtual ICollection<UserProviderReview> UserProviderReviewIdProveedorNavigations { get; set; } = new List<UserProviderReview>();

    public virtual ICollection<UserProviderReview> UserProviderReviewIdUsuarioNavigations { get; set; } = new List<UserProviderReview>();
}
