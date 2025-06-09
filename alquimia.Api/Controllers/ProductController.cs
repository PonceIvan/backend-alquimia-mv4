using alquimia.Services.Interfaces;
using alquimia.Services.Models;
using Microsoft.AspNetCore.Mvc;
namespace alquimia.Api.Controllers
{
    //[Authorize]
    [Route("product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productoservice)
        {
            _productService = productoservice;
        }

        [HttpGet("price-range/{noteId}")]
        public async Task<IActionResult> GetPriceRange(int noteId)
        {
            var priceRange = await _productService.GetPriceRangeFromProductAsync(noteId);
            return Ok(priceRange);
        }

        [HttpPost("get-products-by-formula")]
        public async Task<IActionResult> GetProductsByFormula([FromBody] SearchByFormulaDTO dto)
        {
            var products = await _productService.GetProductsByFormulaAsync(dto.FormulaId);
            return Ok(products);
        }
        
        
        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductoDTO dto)
        {
            // Simular proveedor autenticado
            var idProveedor = 1; // TODO: reemplazar por el id real del proveedor autenticado

            try
            {
                var product = await _productService.CreateProductAsync(dto, idProveedor);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                return Ok(product);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Producto con ID {id} no encontrado." });
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetProductsByFormula()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }


    }
}
