using Microsoft.AspNetCore.Mvc;
using alquimia.Services.Interfaces;
using alquimia.Services.Models;
using alquimia.Services;

namespace alquimia.Api.Controllers
{
    [ApiController]
    [Route("/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("home")]
        public async Task<IActionResult> GetAllProviders()
        {
            var proveedores = await _adminService.GetPendingAndApprovedProvidersAsync();
            return Ok(proveedores);
        }

        [HttpGet("listProviders")]
        public async Task<IActionResult> GetOnlyProviders()
        {
            var proveedores = await _adminService.GetPendingAndApprovedProvidersAsync(); 
            return Ok(proveedores);
        }

        [HttpGet("getProvider/{id}")]
        public async Task<IActionResult> GetProviderById(int id)
        {
            var proveedor = await _adminService.GetPendingOrApprovedProviderByIdAsync(id);
            if (proveedor == null) return NotFound("Proveedor no encontrado.");
            return Ok(proveedor);
        }

        [HttpPost("approveProvider/{id}")]
        public async Task<IActionResult> ApproveProvider(int id)
        {
            var aprobado = await _adminService.ApprovePendingProviderAsync(id);
            if (!aprobado) return BadRequest("No se pudo aprobar al proveedor.");
            return Ok("Proveedor aprobado correctamente.");
        }

        [HttpPut("deactivateProvider/{id}")]
        public async Task<IActionResult> DeactivateProvider(int id)
        {
            var resultado = await _adminService.DeactivateProviderAsync(id);
            if (!resultado) return BadRequest("No se pudo desactivar al proveedor.");
            return Ok("Proveedor dado de baja correctamente.");
        }
    }
}
