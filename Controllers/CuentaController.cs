using System.Security.Claims;
using backendAlquimia.Data.Entities;
using backendAlquimia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backendAlquimia.Controllers
{
    [ApiController]
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

        [HttpGet("login-google")]
        public IActionResult LoginWithGoogle()
        {
            var redirectUrl = Url.Action("GoogleLoginCallback", "Cuenta");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return Challenge(properties, "Google");
        }

        [HttpGet("signin-google")]
        public async Task<IActionResult> GoogleLoginCallback()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return Redirect("http://localhost:3000/Login?error=callback");

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

            if (result.Succeeded)
            {
                // El usuario ya existía
                return Redirect("http://localhost:3000/login/redirectgoogle");
            }

            // Crear el usuario si no existe
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var name = info.Principal.FindFirstValue(ClaimTypes.Name);

            var newUser = new Usuario
            {
                Email = email,
                UserName = email,
                Name = name
            };

            var createResult = await _userManager.CreateAsync(newUser);
            if (!createResult.Succeeded)
                return Redirect("http://localhost:3000/Login?error=creation");

            await _userManager.AddLoginAsync(newUser, info);
            await _signInManager.SignInAsync(newUser, isPersistent: false);

            return Redirect("http://localhost:3000/Login/RedirectGoogle");
        }

        [HttpGet("auth/status")]
        public IActionResult Estado()
        {
            var usuario = User.Identity;
            return Ok(new
            {
                autenticado = usuario?.IsAuthenticated ?? false,
                nombre = usuario?.Name
            });
        }
    }
}
