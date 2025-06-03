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
            try
            {
                var priceRange = await _productService.GetPriceRangeFromProductAsync(noteId);
                return Ok(priceRange);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Unexpected error", detail = ex.Message });
            }
        }

    }
}
