namespace backendAlquimia.alquimia.Services.Services.Models
{
    public class ProductVariantDTO
    {
        public int Id { get; set; } // este campo lo necesitás para edición
        public decimal Volume { get; set; }
        public string Unit { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool IsHypoallergenic { get; set; }
    }
}
