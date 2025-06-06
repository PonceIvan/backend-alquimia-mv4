using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace alquimia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MercadoPagoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MercadoPagoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("generar-link")]
        public async Task<IActionResult> GenerarLinkPago([FromBody] string externalUrl)
        {
            var accessToken = _configuration["MercadoPago:AccessToken"];

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return StatusCode(500, "AccessToken de MercadoPago no configurado.");
            }

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var body = new
            {
                items = new[]
                {
                    new
                    {
                        title = "Pago personalizado",
                        quantity = 1,
                        unit_price = 1000
                    }
                },
                back_urls = new
                {
                    success = "https://tusitio.com/api/mercadopago/success",
                    failure = "https://tusitio.com/api/mercadopago/failure",
                    pending = "https://tusitio.com/api/mercadopago/pending"
                },
                auto_return = "approved",
                external_reference = externalUrl
            };

            var jsonBody = JsonSerializer.Serialize(body);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://api.mercadopago.com/checkout/preferences", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, responseContent);
            }

            var json = JsonDocument.Parse(responseContent);
            var initPoint = json.RootElement.GetProperty("init_point").GetString();

            return Ok(new { url = initPoint });
        }

        [HttpGet("success")]
        public IActionResult PagoExitoso([FromQuery] string payment_id, [FromQuery] string status, [FromQuery] string external_reference)
        {
            return Ok(new
            {
                Mensaje = "Pago exitoso",
                PaymentId = payment_id,
                Estado = status,
                Referencia = external_reference
            });
        }

        [HttpGet("failure")]
        public IActionResult PagoFallido()
        {
            return BadRequest("El pago fue rechazado o cancelado.");
        }

        [HttpGet("pending")]
        public IActionResult PagoPendiente()
        {
            return Ok("El pago está pendiente.");
        }
    }
}
