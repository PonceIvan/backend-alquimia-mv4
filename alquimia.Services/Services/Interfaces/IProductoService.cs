using backendAlquimia.Models;

namespace backendAlquimia.alquimia.Services.Interfaces
{
    public interface IProductoService
    {
        //Task<List<ProductoDTO>> ObtenerProductosPorProveedorAsync(int idProveedor);
        //Task<ProductoDTO> ObtenerProductoPorIdAsync(int idProducto, int idProveedor);
        //Task<ProductoDTO> CrearProductoAsync(CreateProductoDTO dto, int idProveedor);
        //Task<bool> EliminarProductoAsync(int idProducto, int idProveedor);
        //Task<ProductoDTO> ActualizarProductoAsync(int idProducto, UpdateProductoDTO dto, int idProveedor);
        Task<object> GetHomeDataAsync(int idProveedor);

    }
}
