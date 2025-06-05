using System.ComponentModel.DataAnnotations;

namespace alquimia.Services.Models
{
    public class UpdateProductoDTO
    {
        [MaxLength(30)]
        public string? Name { get; set; }

        [MaxLength(50)]
        public string? Description { get; set; }

    }
}