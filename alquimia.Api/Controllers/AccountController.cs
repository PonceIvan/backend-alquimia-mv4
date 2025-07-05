using alquimia.Services.Interfaces;
using alquimia.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using User = alquimia.Data.Entities.User;

namespace alquimia.Api.Controllers
{
    [ApiController]
    [Route("/account")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;
        private readonly IEmailTemplateService _emailTemplate;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            ILogger<AccountController> logger, IJwtService jwtService, IEmailService emailService,
            IConfiguration config, IEmailTemplateService emailTemplate)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _jwtService = jwtService;
            _emailService = emailService;
            _config = config;
            _emailTemplate = emailTemplate;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            _logger.LogInformation("Intentando registrar usuario con email: {Email}", dto.Email);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioExistente = await _userManager.FindByEmailAsync(dto.Email);
            if (usuarioExistente != null)
                return BadRequest(new { mensaje = "El email ya está registrado." });

            var nuevoUsuario = new User
            {
                UserName = GenerateUserNameSeguro(dto.Email),
                Email = dto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                Name = dto.Name?.Trim()
            };

            var result = await _userManager.CreateAsync(nuevoUsuario, dto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            if (!await _userManager.IsInRoleAsync(nuevoUsuario, dto.Rol))
                await _userManager.AddToRoleAsync(nuevoUsuario, dto.Rol);

            var roles = await _userManager.GetRolesAsync(nuevoUsuario);
            var token = _jwtService.GenerateToken(nuevoUsuario, roles);
            await _signInManager.SignInAsync(nuevoUsuario, isPersistent: false);

            return Ok(new { mensaje = "Usuario registrado correctamente.", token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest(new { mensaje = "Email y contraseña son obligatorios." });

            var usuario = await _userManager.FindByEmailAsync(dto.Email);
            if (usuario == null)
                return Unauthorized(new { mensaje = "Usuario no encontrado." });

            var result = await _signInManager.CheckPasswordSignInAsync(usuario, dto.Password, false);
            if (!result.Succeeded)
                return Unauthorized(new { mensaje = "Credenciales inválidas." });

            var roles = await _userManager.GetRolesAsync(usuario);
            var token = _jwtService.GenerateToken(usuario, roles);
            await _signInManager.SignInAsync(usuario, false);

            return Ok(new { mensaje = "Login exitoso ✅", token });
        }

        [HttpGet("login-google")]
        public IActionResult LoginWithGoogle([FromQuery] bool debug = false)
        {
            var redirectUrl = Url.Action("GoogleLoginCallback", "Account", null, Request.Scheme);
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);

            if (debug)
            {
                return Ok(new
                {
                    redirectUrl,
                    callback = redirectUrl
                });
            }

            return Challenge(properties, "Google");
        }

        [HttpGet("signin-google")]
        public async Task<IActionResult> GoogleLoginCallback([FromQuery] bool debug = false)
        {
            try
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                    return Redirect("https://frontend-alquimia.vercel.app/Login?error=callback");

                var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
                var frontendRedirect = _config["OAuth:Url"];
                if (string.IsNullOrWhiteSpace(frontendRedirect) || frontendRedirect.Contains("/account/signin-google"))
                {
                    var baseUrl = _config["AppSettings:FrontendBaseUrl"] ?? "https://frontend-alquimia.vercel.app/";
                    frontendRedirect = baseUrl.TrimEnd('/') + "/Login/RedirectGoogle";
                }

                if (result.Succeeded)
                {
                    var existingUser = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                    var roles = await _userManager.GetRolesAsync(existingUser);
                    var token = _jwtService.GenerateToken(existingUser, roles);
                    await _signInManager.SignInAsync(existingUser, false);

                    if (debug)
                        return Ok(new { token, existingUser = true });

                    return Redirect(frontendRedirect);
                }

                // Usuario no existe, se crea
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var name = info.Principal.FindFirstValue(ClaimTypes.Name);

                var newUser = new User
                {
                    Email = email,
                    UserName = GenerateUserNameSeguro(email),
                    Name = name,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var createResult = await _userManager.CreateAsync(newUser);
                if (!createResult.Succeeded)
                    return Redirect("https://frontend-alquimia.vercel.app/Login?error=creation");

                await _userManager.AddLoginAsync(newUser, info);
                var newRoles = await _userManager.GetRolesAsync(newUser);
                var newToken = _jwtService.GenerateToken(newUser, newRoles);
                await _signInManager.SignInAsync(newUser, false);

                if (debug)
                    return Ok(new { token = newToken, newUser = true });

                return Redirect(frontendRedirect);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GoogleLoginCallback");
                return Redirect("https://frontend-alquimia.vercel.app/Login?error=server");
            }
        }

        [HttpGet("debug/google-config")]
        public IActionResult GoogleConfigDebug()
        {
            var loginEndpoint = Url.Action("LoginWithGoogle", "Account", null, Request.Scheme);
            var callback = Url.Action("GoogleLoginCallback", "Account", null, Request.Scheme);
            var frontendRedirect = _config["OAuth:Url"];

            if (string.IsNullOrWhiteSpace(frontendRedirect) || frontendRedirect.Contains("/account/signin-google"))
            {
                var baseUrl = _config["AppSettings:FrontendBaseUrl"] ?? "https://frontend-alquimia.vercel.app/";
                frontendRedirect = baseUrl.TrimEnd('/') + "/Login/RedirectGoogle";
            }

            return Ok(new
            {
                clientIdDefined = !string.IsNullOrWhiteSpace(_config["OAuth:ClientID"]),
                loginEndpoint,
                callback,
                frontendRedirect
            });
        }

        [HttpGet("debug/google-login")]
        public IActionResult GoogleLoginDebug()
        {
            var loginUrl = Url.Action("LoginWithGoogle", "Account", new { debug = true }, Request.Scheme);
            var callbackUrl = Url.Action("GoogleLoginCallback", "Account", new { debug = true }, Request.Scheme);

            return Ok(new
            {
                loginUrl,
                callbackUrl
            });
        }

        [HttpPost("register-provider")]
        public async Task<IActionResult> RegisterProvider([FromBody] RegisterProviderDTO dto)
        {
            var usuarioExistente = await _userManager.FindByEmailAsync(dto.Email);
            if (usuarioExistente != null)
                return BadRequest(new { mensaje = "El email ya está registrado." });

            var nuevoUsuario = new User
            {
                UserName = GenerateUserNameSeguro(dto.Email),
                Email = dto.Email,
                Name = dto.Name?.Trim(),
                EsProveedor = true,
                Empresa = dto.Empresa,
                Cuil = dto.Cuil,
                Rubro = dto.Rubro,
                OtroProducto = string.Join(",", dto.OtroProducto),
                TarjetaNumero = dto.TarjetaNumero,
                TarjetaVencimiento = dto.TarjetaVencimiento,
                TarjetaCVC = dto.TarjetaCVC
            };

            var result = await _userManager.CreateAsync(nuevoUsuario, dto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(nuevoUsuario, "Creador");

            var roles = await _userManager.GetRolesAsync(nuevoUsuario);
            var token = _jwtService.GenerateToken(nuevoUsuario, roles);
            await _signInManager.SignInAsync(nuevoUsuario, false);

            var mensajeBienvenida = _emailTemplate.GetWelcomeEmail(dto.Name);
            await _emailService.SendEmailAsync(dto.Email, "Bienvenido a Alquimia - Cuenta en revisión", mensajeBienvenida);

            return Ok(new { mensaje = "Proveedor registrado correctamente como creador en espera de aprobación.", token });
        }

        [HttpGet("auth/status")]
        public IActionResult State()
        {
            return Ok(new
            {
                autenticado = User.Identity?.IsAuthenticated ?? false,
                nombre = User.Identity?.Name
            });
        }

        [Authorize]
        [HttpGet("perfil")]
        public async Task<IActionResult> ObtenerPerfil()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(userEmail);
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(new
            {
                id = user.Id,
                nombre = user.Name,
                email = user.Email,
                rol = roles.FirstOrDefault()
            });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                throw new KeyNotFoundException("Usuario no encontrado");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebUtility.UrlEncode(token);
            var frontendBaseUrl = _config["AppSettings:FrontendBaseUrl"];
            var callbackUrl = $"{frontendBaseUrl}restablecer-contrasenia?email={model.Email}&token={encodedToken}";
            var message = _emailTemplate.GetPasswordResetEmail(user.Name, callbackUrl);
            await _emailService.SendEmailAsync(model.Email, "Recuperar contraseña - Alquimia", message);

            return Ok("Se envió un enlace para restablecer la contraseña.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                throw new KeyNotFoundException("Usuario no encontrado");

            var result = await _userManager.ResetPasswordAsync(user, WebUtility.UrlDecode(model.Token), model.NewPassword);
            if (!result.Succeeded)
                throw new ArgumentException("Ocurrió un error: " + string.Join(", ", result.Errors.Select(e => e.Description)));

            return Ok("Contraseña restablecida correctamente.");
        }

        private string GenerateUserNameSeguro(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                return Guid.NewGuid().ToString("N")[..8];

            var nombre = email.Split('@')[0];
            nombre = new string(nombre.Where(char.IsLetterOrDigit).ToArray());

            return string.IsNullOrWhiteSpace(nombre)
                ? Guid.NewGuid().ToString("N")[..8]
                : nombre;
        }
    }
}
