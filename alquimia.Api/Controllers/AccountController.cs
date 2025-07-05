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

        [HttpGet("login-google")]
        public IActionResult LoginWithGoogle([FromQuery] bool debug = false)
        {
            var redirectUrl = Url.Action("GoogleLoginCallback", "Account", null, Request.Scheme);
            _logger.LogInformation("\ud83d\udd01 redirect_uri usado para Google OAuth: {RedirectUrl}", redirectUrl);
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
                _logger.LogInformation("Callback de login con Google recibido");

                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    _logger.LogWarning("No se pudo obtener la información externa del login.");
                    if (debug)
                        return BadRequest(new { mensaje = "No se pudo obtener la información externa." });

                    return Redirect("https://frontend-alquimia.vercel.app/Login?error=callback");
                }

                var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

                var frontendRedirect = _config["OAuth:Url"];
                if (string.IsNullOrWhiteSpace(frontendRedirect) ||
                    frontendRedirect.Contains("/account/signin-google", StringComparison.OrdinalIgnoreCase))
                {
                    var baseUrl = _config["AppSettings:FrontendBaseUrl"] ?? "https://frontend-alquimia.vercel.app/";
                    frontendRedirect = baseUrl.TrimEnd('/') + "/Login/RedirectGoogle";
                }

                if (result.Succeeded)
                {
                    var existingUser = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                    var roles = await _userManager.GetRolesAsync(existingUser);
                    var token = _jwtService.GenerateToken(existingUser, roles);
                    await _signInManager.SignInAsync(existingUser, isPersistent: false);

                    _logger.LogInformation("Usuario existente logueado con Google: {Email}", info.Principal.FindFirstValue(ClaimTypes.Email));

                    if (debug)
                    {
                        return Ok(new
                        {
                            token,
                            existingUser = true,
                            email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            name = info.Principal.FindFirstValue(ClaimTypes.Name)
                        });
                    }

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
                {
                    _logger.LogError("Error al crear usuario con Google: {Errores}", createResult.Errors);
                    return Redirect("https://frontend-alquimia.vercel.app/Login?error=creation");
                }

                await _userManager.AddLoginAsync(newUser, info);
                var newRoles = await _userManager.GetRolesAsync(newUser);
                var newToken = _jwtService.GenerateToken(newUser, newRoles);
                await _signInManager.SignInAsync(newUser, isPersistent: false);

                _logger.LogInformation("Nuevo usuario creado desde Google: {Email}", email);

                if (debug)
                {
                    return Ok(new
                    {
                        token = newToken,
                        newUser = true,
                        email,
                        name
                    });
                }

                return Redirect(frontendRedirect);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado en GoogleLoginCallback.");
                if (debug)
                {
                    return StatusCode(500, new { mensaje = "Error interno", error = ex.Message });
                }
                return Redirect("https://frontend-alquimia.vercel.app/Login?error=server");
            }
        }
    }
}
