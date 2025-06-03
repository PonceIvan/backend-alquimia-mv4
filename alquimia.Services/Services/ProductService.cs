using alquimia.Data.Data.Entities;
using backendAlquimia.alquimia.Services.Interfaces;
using backendAlquimia.alquimia.Services.Models;
using backendAlquimia.alquimia.Services.Services.Models;
using backendAlquimia.Models;
using Microsoft.EntityFrameworkCore;
using Product = alquimia.Data.Data.Entities.Product;
using ProductVariant = alquimia.Data.Data.Entities.ProductVariant;

namespace backendAlquimia.alquimia.Services
{
    public class ProductService : IProductService
    {
        private readonly AlquimiaDbContext _context;

        public ProductService(AlquimiaDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDTO>> ObtenerProductosPorProveedorAsync(int idProveedor)
        {
            return await _context.Products
                .Include(p => p.ProductVariants)
                .Include(p => p.TipoProducto)
                .Where(p => p.IdProveedor == idProveedor)
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.ProductVariants.Min(v => v.Price),
                    Stock = p.ProductVariants.Sum(v => v.Stock),
                    TipoProducto = p.TipoProducto.Description
                })
                .ToListAsync();
        }

        public async Task<ProductDTO> ObtenerProductoPorIdAsync(int idProducto, int idProveedor)
        {
            var producto = await _context.Products
                .Include(p => p.ProductVariants)
                .Include(p => p.TipoProducto)
                .FirstOrDefaultAsync(p => p.Id == idProducto && p.IdProveedor == idProveedor);

            if (producto == null)
                throw new KeyNotFoundException("Producto no encontrado");

            return new ProductDTO
            {
                Id = producto.Id,
                Name = producto.Name,
                Description = producto.Description,
                TipoProducto = producto.TipoProducto.Description,
                Variants = producto.ProductVariants.Select(v => new ProductVariantDTO
                {
                    Id = v.Id,
                    Volume = v.Volume,
                    Unit = v.Unit,
                    Price = v.Price,
                    Stock = v.Stock,
                    IsHypoallergenic = v.IsHypoallergenic ?? false
                }).ToList()
            };
        }

        public async Task<ProductDTO> CrearProductoAsync(CreateProductoDTO dto, int idProveedor)
        {
            try
            {
                var tipoProducto = await _context.ProductTypes
                    .FirstOrDefaultAsync(t => t.Description == dto.TipoProductoDescription);

                if (tipoProducto == null)
                    throw new KeyNotFoundException("Tipo de producto no válido");

                var producto = new Product
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    TipoProductoId = tipoProducto.Id,
                    IdProveedor = idProveedor,
                    ProductVariants = dto.Variants?.Select(v => new ProductVariant
                    {
                        Volume = v.Volume,
                        Unit = v.Unit,
                        Price = v.Price,
                        Stock = v.Stock,
                        IsHypoallergenic = v.IsHypoallergenic
                    }).ToList() ?? new List<ProductVariant>()
                };

                _context.Products.Add(producto);

                await _context.SaveChangesAsync();

                return await ObtenerProductoPorIdAsync(producto.Id, idProveedor);
            }
            catch (DbUpdateException ex)
            {
                // Log de la excepción con inner exception
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine($"[Error al guardar producto] {innerMessage}");
                throw new Exception($"Error al guardar producto: {innerMessage}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error general] {ex.Message}");
                throw;
            }
        }

        public async Task<bool> EliminarProductoAsync(int idProducto, int idProveedor)
        {
            var producto = await _context.Products
                .Include(p => p.ProductVariants)
                .FirstOrDefaultAsync(p => p.Id == idProducto && p.IdProveedor == idProveedor);

            if (producto == null)
                return false;

            // Eliminar variantes primero
            _context.ProductVariants.RemoveRange(producto.ProductVariants);

            // Luego eliminar el producto
            _context.Products.Remove(producto);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ProductDTO> ActualizarProductoAsync(int idProducto, UpdateProductoDTO dto, int idProveedor)
        {
            var producto = await _context.Products
                .Include(p => p.ProductVariants)
                .FirstOrDefaultAsync(p => p.Id == idProducto && p.IdProveedor == idProveedor);

            if (producto == null)
                throw new KeyNotFoundException("Producto no encontrado o no pertenece al proveedor");

            if (!string.IsNullOrWhiteSpace(dto.Name))
                producto.Name = dto.Name;

            if (!string.IsNullOrWhiteSpace(dto.Description))
                producto.Description = dto.Description;

            await _context.SaveChangesAsync();
            return await ObtenerProductoPorIdAsync(idProducto, idProveedor);
        }

        public async Task<HomeProviderDataDTO> GetHomeDataAsync(int idProveedor)
        {
            var productos = await ObtenerProductosPorProveedorAsync(idProveedor);

            return new HomeProviderDataDTO
            {
                TotalProductos = productos.Count,
                StockTotal = productos.Sum(p => p.Stock),
                UltimosProductos = productos.OrderByDescending(p => p.Id).Take(5).ToList()
            };
        }

        public async Task<PriceRangeDTO> GetPriceRangeFromProductAsync(int noteId)
        {
            var note = await _context.Notes.FindAsync(noteId);

            var query = _context.ProductVariants
                .Where(v =>
                    v.Product.Name.ToLower().Contains(note.Name.ToLower()) ||
                    v.Product.Description.ToLower().Contains(note.Name.ToLower()) ||
                    v.Product.Name.ToLower().Contains(note.Description.ToLower()) ||
                    v.Product.Description.ToLower().Contains(note.Description.ToLower())
                );

            var prices = await query.Select(v => v.Price).ToListAsync();
            if (!prices.Any())
                return new PriceRangeDTO { MinPrice = 0, MaxPrice = 0 };

            return new PriceRangeDTO
            {
                MinPrice = await query.MinAsync(v => (decimal?)v.Price) ?? 0,
                MaxPrice = await query.MaxAsync(v => (decimal?)v.Price) ?? 0
            };
        }

        public async Task AddVariantsToProductAsync(int productId, CreateProductVariantDTO dto)
        {
            var producto = await _context.Products.Include(p => p.ProductVariants)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (producto == null)
                throw new KeyNotFoundException("Producto no encontrado");

            var variante = new ProductVariant
            {
                Volume = dto.Volume,
                Unit = dto.Unit,
                Price = dto.Price,
                Stock = dto.Stock,
                IsHypoallergenic = dto.IsHypoallergenic,
                ProductId = productId
            };

            _context.ProductVariants.Add(variante);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarVarianteAsync(int variantId, UpdateProductVariantDTO dto)
        {
            var variante = await _context.ProductVariants.FindAsync(variantId);
            if (variante == null)
                throw new Exception("Variante no encontrada");

            if (dto.Volume.HasValue)
                variante.Volume = dto.Volume.Value;

            if (!string.IsNullOrEmpty(dto.Unit))
                variante.Unit = dto.Unit;

            if (dto.Price.HasValue)
                variante.Price = dto.Price.Value;

            if (dto.Stock.HasValue)
                variante.Stock = dto.Stock.Value;

            if (dto.IsHypoallergenic.HasValue)
                variante.IsHypoallergenic = dto.IsHypoallergenic.Value;

            await _context.SaveChangesAsync();
        }



        public async Task<bool> UpdateVariantAsync(int variantId, ProductVariantDTO dto)
        {
            var variante = await _context.ProductVariants.FindAsync(variantId);
            if (variante == null) return false;

            variante.Volume = dto.Volume;
            variante.Unit = dto.Unit;
            variante.Price = dto.Price;
            variante.Stock = dto.Stock;
            variante.IsHypoallergenic = dto.IsHypoallergenic;

            await _context.SaveChangesAsync();
            return true;
        }




        public async Task<bool> EliminarVarianteAsync(int variantId)
        {
            var variante = await _context.ProductVariants.FindAsync(variantId);
            if (variante == null)
                return false;

            _context.ProductVariants.Remove(variante);
            await _context.SaveChangesAsync();

            return true;
        }


    }

}