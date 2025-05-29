using Microsoft.AspNetCore.Mvc;
using alquimia.Services.Services.Interfaces;
using alquimia.Services.Services.Models;
using alquimia.Services.Services;

namespace backendAlquimia.Controllers
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
            var proveedores = await _adminService.GetAllProvidersAsync();
            return Ok(proveedores);
        }

        [HttpGet("proveedores")]
        public async Task<IActionResult> GetOnlyProviders()
        {
            var proveedores = await _adminService.GetAllProvidersAsync(); // Misma lógica por ahora
            return Ok(proveedores);
        }

        [HttpGet("proveedores/{id}")]
        public async Task<IActionResult> GetProviderById(int id)
        {
            var proveedor = await _adminService.GetProviderByIdAsync(id);
            if (proveedor == null) return NotFound("Proveedor no encontrado.");
            return Ok(proveedor);
        }

        [HttpPost("proveedor/{id}")]
        public async Task<IActionResult> ApproveProvider(int id)
        {
            var aprobado = await _adminService.ApproveProviderAsync(id);
            if (!aprobado) return BadRequest("No se pudo aprobar al proveedor.");
            return Ok("Proveedor aprobado correctamente.");
        }

        [HttpPut("proveedor/{id}/baja")]
        public async Task<IActionResult> DeactivateProvider(int id)
        {
            var resultado = await _adminService.DeactivateProviderAsync(id);
            if (!resultado) return BadRequest("No se pudo desactivar al proveedor.");
            return Ok("Proveedor dado de baja correctamente.");
        }
    }
}
