using backendAlquimia.alquimia.Services.Services.Models;

namespace backendAlquimia.alquimia.Services
{
    public class HomeProviderDataDTO
    {
        public int TotalProductos { get; internal set; }
        public int StockTotal { get; internal set; }
        public List<ProductDTO> UltimosProductos { get; internal set; }
    }
}