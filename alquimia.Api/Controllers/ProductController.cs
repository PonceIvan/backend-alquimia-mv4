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

        [HttpGet("all")]
        public async Task<IActionResult> GetProductsByFormula()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }
    }
}
