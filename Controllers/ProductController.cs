using backendAlquimia.alquimia.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace backendAlquimia.Controllers
{
    [Authorize] // solo puede acceder el creador
    [Route("product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productoservice)
        {
            _productService = productoservice;
        }

        [HttpGet("price-range")]
        public async Task<IActionResult> GetPriceRange([FromQuery] int noteId)
        {
            var PriceRange = await _productService.GetPriceRangeFromProductAsync(noteId);
            return Ok(PriceRange);
        }
    }
}
