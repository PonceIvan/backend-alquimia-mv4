namespace backendAlquimia.alquimia.Services.Services.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        //public decimal Price { get; set; }
        //public int Stock { get; set; }
        public string ProductType { get; set; } = null!;
        public string SupplierName { get; set; } = null!;

        public List<ProductVariantDTO> Variants { get; set; } = new();
    }

}