namespace backendAlquimia.alquimia.Services.Services.Models
{
    public class UpdateProductVariantDTO
    {
        public decimal? Volume { get; set; }
        public string? Unit { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public bool? IsHypoallergenic { get; set; }
    }
}
