using alquimia.Services.Models;

namespace alquimia.Services.Models
{
    public class HomeProviderDataDTO
    {
        public int TotalProductos { get; internal set; }
        public int StockTotal { get; internal set; }
        public List<ProductDTO> UltimosProductos { get; internal set; }
    }
}