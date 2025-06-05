using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Entities
{
    public partial class UserProductReview
    {
        [Key]
        public int Id { get; set; }

        public int IdUsuario { get; set; }

        public string Description { get; set; } = null!;

        public int? IdProducto { get; set; }

        public DateTime FechaPublicacion { get; set; }

        [ForeignKey("IdProducto")]
        [InverseProperty("UserProductReviews")]
        public virtual Product? IdProductoNavigation { get; set; }

        [ForeignKey("IdUsuario")]
        [InverseProperty("UserProductReviews")]
        public virtual User IdUsuarioNavigation { get; set; } = null!;
    }
}


