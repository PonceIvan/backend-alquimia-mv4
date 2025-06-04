namespace backendAlquimia.alquimia.Services.Services.Models
{
    public class ProductVariantDTO
    {
        public int Id { get; set; }
        public decimal Volume { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool? IsHypoallergenic { get; set; }
        public bool? IsVegan { get; set; }
        public bool? IsParabenFree { get; set; }
    }
}
