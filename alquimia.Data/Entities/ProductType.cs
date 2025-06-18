using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace alquimia.Data.Entities
{
    public partial class ProductType
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; } = null!;

        [InverseProperty("TipoProducto")]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}


