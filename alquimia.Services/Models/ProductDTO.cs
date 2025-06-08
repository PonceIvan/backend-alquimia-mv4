namespace alquimia.Services.Models
{

        public class ProductDTO
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string ProductType { get; set; }
            public ProviderDTO Provider { get; set; }
            public List<ProductVariantDTO> Variants { get; set; }
            public decimal Price { get; set; }
            public int? Volume { get; set; }
            public string? Unit { get; set; }
        }

    }

