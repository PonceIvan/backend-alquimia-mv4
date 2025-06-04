using backendAlquimia.alquimia.Services.Models;
using backendAlquimia.alquimia.Services.Services.Models;
using backendAlquimia.Models;

namespace backendAlquimia.alquimia.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDTO>> ObtenerProductosPorProveedorAsync(int idProveedor);
        Task<ProductDTO> ObtenerProductoPorIdAsync(int idProducto, int idProveedor);
        Task<ProductDTO> CrearProductoAsync(CreateProductoDTO dto, int idProveedor);
        Task<bool> EliminarProductoAsync(int idProducto, int idProveedor);
        Task<ProductDTO> ActualizarProductoAsync(int idProducto, UpdateProductoDTO dto, int idProveedor);
        Task<HomeProviderDataDTO> GetHomeDataAsync(int idProveedor);
        Task<PriceRangeDTO> GetPriceRangeFromProductAsync(int noteId);
        Task AddVariantsToProductAsync(int productId, CreateProductVariantDTO dto);
        Task ActualizarVarianteAsync(int variantId, UpdateProductVariantDTO dto);
        Task<bool> EliminarVarianteAsync(int variantId);
        Task<bool> UpdateVariantAsync(int variantId, ProductVariantDTO dto);
        Task<List<ProductDTO>> GetProductsByFormulaAsync(int formulaId);
    }
}
