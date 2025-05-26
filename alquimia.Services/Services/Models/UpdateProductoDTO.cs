using System.ComponentModel.DataAnnotations;

namespace backendAlquimia.Models
{
    public class UpdateProductoDTO
    {
        [MaxLength(30)]
        public string? Name { get; set; }

        [MaxLength(50)]
        public string? Description { get; set; }

        [Range(0, float.MaxValue)]
        public decimal? Price { get; set; }

        [Range(0, int.MaxValue)]
        public int? Stock { get; set; }
    }
}
