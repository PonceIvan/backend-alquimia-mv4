namespace alquimia.Services.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ProductType { get; set; } = null!;
        public ProviderDTO? Provider { get; set; }
        public string SupplierName { get; set; } = null!;

        public List<ProductVariantDTO> Variants { get; set; } = new();
    }

}