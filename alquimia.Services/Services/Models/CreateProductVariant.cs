using System.ComponentModel.DataAnnotations;

namespace backendAlquimia.alquimia.Services.Services.Models
{
    public class CreateProductVariantDTO
    {
        public decimal Volume { get; set; }
        public string Unit { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool IsHypoallergenic { get; set; }
    }
}
