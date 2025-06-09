using alquimia.Services.Models;
using System.ComponentModel.DataAnnotations;
namespace alquimia.Services.Models
{
    public class CreateProductoDTO
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public string TipoProductoDescription { get; set; } = null!;

        [Required]
        public List<CreateProductVariantDTO> Variants { get; set; } = new();

        public string? ImagenBase64 { get; set; }
    }
}
