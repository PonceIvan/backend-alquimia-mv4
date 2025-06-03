using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backendAlquimia.alquimia.Services.Services.Models;
namespace backendAlquimia.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string TipoProducto { get; set; } = null!;

        public List<ProductVariantDTO> Variants { get; set; } = new(); // 👈 Necesario
    }

}