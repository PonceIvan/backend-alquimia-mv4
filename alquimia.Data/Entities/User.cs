﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace alquimia.Data.Entities
{
    public class User : IdentityUser<int>
    {
        [Required]
        public string Name { get; set; } = null!;

        public int? IdEstado { get; set; }
        public int? IdQuiz { get; set; }
        public int? IdSuscripcion { get; set; }
        public bool EsProveedor { get; set; }
        public string? Empresa { get; set; }
        public string? Cuil { get; set; }
        public string? Rubro { get; set; }
        public string? TarjetaNumero { get; set; }
        public string? TarjetaVencimiento { get; set; }
        public string? TarjetaCVC { get; set; }
        public string? OtroProducto { get; set; }
        [ForeignKey("IdEstado")]
        public virtual Status? IdEstadoNavigation { get; set; }

        [ForeignKey("IdQuiz")]
        public virtual Quiz? IdQuizNavigation { get; set; }

        [ForeignKey("IdSuscripcion")]
        public virtual Subscription? IdSuscripcionNavigation { get; set; }

        public virtual ICollection<FinalEntity> FinalEntities { get; set; } = new List<FinalEntity>();

        [InverseProperty(nameof(Formula.Creator))]
        public virtual ICollection<Formula> Formulas { get; set; } = new List<Formula>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<UserProductReview> UserProductReviews { get; set; } = new List<UserProductReview>();
        public virtual ICollection<UserProduct> UserProducts { get; set; } = new List<UserProduct>();
        public virtual ICollection<UserProviderReview> UserProviderReviewIdProveedorNavigations { get; set; } = new List<UserProviderReview>();
        public virtual ICollection<UserProviderReview> UserProviderReviewIdUsuarioNavigations { get; set; } = new List<UserProviderReview>();

    }
}

