using alquimia.Services.Services.Models;
using backendAlquimia.alquimia.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace backendAlquimia.Controllers
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
    }
}
