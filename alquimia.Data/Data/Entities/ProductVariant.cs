using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace alquimia.Data.Data.Entities;

[Table("ProductVariant")]
public partial class ProductVariant
{
    [Key]
    public int Id { get; set; }

    public int ProductId { get; set; }

    [Column(TypeName = "decimal(10, 0)")]
    public decimal Volume { get; set; }

    [StringLength(20)]
    public string Unit { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    public int Stock { get; set; }
    public bool? IsHypoallergenic { get; set; }
    public bool? IsVegan { get; set; }
    public bool? IsParabenFree { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductVariants")]
    [JsonIgnore]
    public virtual Product Product { get; set; } = null!;
}
