using backendAlquimia.Data.Entities;
using backendAlquimia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backendAlquimia.Controllers
{
    [Route("cuenta")]
    public class CuentaController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public CuentaController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("registrar-json")]
        public async Task<IActionResult> RegistrarJson([FromBody] RegisterDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioExistente = await _userManager.FindByEmailAsync(dto.Email);
            if (usuarioExistente != null)
                return BadRequest(new { mensaje = "El email ya está registrado." });

            var nuevoUsuario = new Usuario
            {
                UserName = dto.Email,
                Email = dto.Email,
                Name = dto.Name?.Trim()
            };

            var result = await _userManager.CreateAsync(nuevoUsuario, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Autenticamos automáticamente al usuario después del registro
            await _signInManager.SignInAsync(nuevoUsuario, isPersistent: false);

            return Ok(new { mensaje = "Usuario registrado correctamente." });
        }

        [HttpPost("login-json")]
        public async Task<IActionResult> LoginJson([FromBody] LoginDTO dto)
        {
            
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest(new { mensaje = "Email y contraseña son obligatorios." });

            
            var usuario = await _userManager.FindByEmailAsync(dto.Email);

            if (usuario == null)
                return Unauthorized(new { mensaje = "Usuario no encontrado." });

        
            if (string.IsNullOrWhiteSpace(usuario.UserName) || usuario.Id == 0)
                return StatusCode(500, new { mensaje = "El usuario tiene datos incompletos (UserName o Id)." });

            var result = await _signInManager.CheckPasswordSignInAsync(usuario, dto.Password, lockoutOnFailure: false);

            if (!result.Succeeded)
                return Unauthorized(new { mensaje = "Credenciales inválidas." });

            await _signInManager.SignInAsync(usuario, isPersistent: false);

            return Ok(new { mensaje = "Login exitoso ✅" });
        }


    }
}
