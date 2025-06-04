using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace alquimia.Data.Data.Entities;

public partial class Product
{
    [Key]
    public int Id { get; set; }

    public int TipoProductoId { get; set; }

    [StringLength(30)]
    public string Name { get; set; } = null!;

    [StringLength(256)]
    public string Description { get; set; } = null!;
    //public decimal Price { get; set; }
    //public int Stock { get; set; }
    public int? IdProveedor { get; set; }
    //public int? UsuarioId { get; set; }
    //[InverseProperty("IdProductoNavigation")]
    public virtual ICollection<Design> Designs { get; set; } = new List<Design>();
    [InverseProperty("Productos")]
    public virtual ICollection<FinalEntity> FinalEntities { get; set; } = new List<FinalEntity>();
    [ForeignKey("IdProveedor")]
    [InverseProperty("Products")]
    public virtual User? IdProveedorNavigation { get; set; }

    [InverseProperty("Productos")]
    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<UserProductReview> UserProductReviews { get; set; } = new List<UserProductReview>();

    [InverseProperty("Producto")]
    public virtual ICollection<UserProduct> UserProducts { get; set; } = new List<UserProduct>();

    [ForeignKey("TipoProductoId")]
    [InverseProperty("Products")]
    public virtual ProductType TipoProducto { get; set; } = null!;
}
