using alquimia.Data.Data.Entities;
using backendAlquimia.alquimia.Services.Interfaces;
using backendAlquimia.alquimia.Services.Services.Models;
using backendAlquimia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace backendAlquimia.Controllers
{
    [Authorize(Roles = "Proveedor")]
    [Route("provider")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AlquimiaDbContext _context;


        public ProviderController(
            IProductService productoservice,
            IHttpContextAccessor httpcontextaccessor,
            AlquimiaDbContext context)
        {
            _productService = productoservice;
            _httpContextAccessor = httpcontextaccessor;
            _context = context;
        }

        private int ObtenerIdProveedor()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            return int.Parse(userIdClaim.Value);
        }

        /// /////////////////////////////////////////////////////////////////////

        [HttpGet("home")]
        public async Task<IActionResult> GetHomeData()
        {
            var idProveedor = ObtenerIdProveedor();
            var data = await _productService.GetHomeDataAsync(idProveedor);
            return Ok(data);
        }

        /// /////////////////////////////////////////////////////////////////////

        [HttpGet("productos")]
        public async Task<IActionResult> GetProductos()
        {
            var idProveedor = ObtenerIdProveedor();
            var productos = await _productService.ObtenerProductosPorProveedorAsync(idProveedor);
            return Ok(productos);
        }


        /// /////////////////////////////////////////////////////////////////////

        [HttpGet("tipos-producto")]
        public async Task<IActionResult> GetTiposProducto()
        {
            var tipos = await _context.ProductTypes
                .Select(t => new
                {
                    Descripcion = t.Description
                })
                .ToListAsync();

            return Ok(tipos);
        }
        /// /////////////////////////////////////////////////////////////////////

        [HttpPost("create/{idProveedor:int}")]
        public async Task<IActionResult> CrearProducto(int idProveedor, [FromBody] CreateProductoDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var productoCreado = await _productService.CrearProductoAsync(dto, idProveedor);
                return Ok(productoCreado);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno: " + ex.Message });
            }
        }
        /// /////////////////////////////////////////////////////////////////////

        [HttpGet("productos/{idProducto}")]
        public async Task<IActionResult> GetProducto(int idProducto)
        {
            var idProveedor = ObtenerIdProveedor();
            var producto = await _productService.ObtenerProductoPorIdAsync(idProducto, idProveedor);

            if (producto == null)
                return NotFound(new { mensaje = "Producto no encontrado" });

            return Ok(producto);
        }
        /// /////////////////////////////////////////////////////////////////////
        [HttpDelete("productos/{idProducto}")]
        public async Task<IActionResult> EliminarProducto(int idProducto)
        {
            try
            {
                var idProveedor = ObtenerIdProveedor();
                var resultado = await _productService.EliminarProductoAsync(idProducto, idProveedor);

                if (!resultado)
                    return NotFound(new { mensaje = "Producto no encontrado o no pertenece al proveedor" });

                return NoContent(); // 204 No Content es lo estándar para DELETE exitoso
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar producto: {ex}");
                return StatusCode(500, new { mensaje = "Error interno al eliminar el producto" });
            }
        }
        /// /////////////////////////////////////////////////////////////////////

        // POST api/products/{productId}/variants
        [HttpPost("{productId}/variants")]
        public async Task<IActionResult> AddVariants(int productId, [FromBody] CreateProductVariantDTO dto)
        {
            try
            {
                await _productService.AddVariantsToProductAsync(productId, dto);
                return NoContent(); // 204 OK, porque solo agregamos datos
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Loggear ex.Message si tenés un logger configurado
                return StatusCode(500, "Error interno al agregar variantes");
            }
        }
        /// /////////////////////////////////////////////////////////////////////
        //ACTUALIZAR VARIANTE!!!!

        [HttpPut("variants/{variantId}")]
        public async Task<IActionResult> ActualizarVariante(int variantId, [FromBody] UpdateProductVariantDTO dto)
        {
            await _productService.ActualizarVarianteAsync(variantId, dto);
            return NoContent();
        }

        /// /////////////////////////////////////////////////////////////////////

        [HttpDelete("variants/{variantId}")]
        public async Task<IActionResult> EliminarVariante(int variantId)
        {
            var eliminado = await _productService.EliminarVarianteAsync(variantId);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }


        /// /////////////////////////////////////////////////////////////////////


        [HttpPut("productos/{idProducto}")]
        public async Task<IActionResult> ActualizarProducto(int idProducto, [FromBody] UpdateProductoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var idProveedor = ObtenerIdProveedor();
                var producto = await _productService.ActualizarProductoAsync(idProducto, dto, idProveedor);
                return Ok(producto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar producto: {ex}");
                return StatusCode(500, new { mensaje = "Error interno al actualizar el producto" });
            }
        }

    }
}