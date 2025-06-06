using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Alquimia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MpController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MpController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("generate-link")]
        public async Task<IActionResult> GeneratePaymentLink([FromBody] string externalReference)
        {
            var accessToken = _configuration["MercadoPago:AccessToken"];

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return StatusCode(500, "Mercado Pago access token is not configured.");
            }

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            var requestBody = new
            {
                items = new[]
                {
                    new
                    {
                        title       = "Custom Payment",
                        quantity    = 1,
                        unit_price  = 1000
                    }
                },
                back_urls = new
                {
                    success = "https://your-site.com/api/mercadopago/success",
                    failure = "https://your-site.com/api/mercadopago/failure",
                    pending = "https://your-site.com/api/mercadopago/pending"
                },
                auto_return = "approved",
                external_reference = externalReference
            };

            var jsonBody = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://api.mercadopago.com/checkout/preferences", content);
            var responseText = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, responseText);
            }

            var json = JsonDocument.Parse(responseText);
            var initPoint = json.RootElement.GetProperty("init_point").GetString();

            return Ok(new { Url = initPoint });
        }

        [HttpGet("success")]
        public IActionResult PaymentSuccess(
            [FromQuery] string payment_id,
            [FromQuery] string status,
            [FromQuery] string external_reference)
        {
            return Ok(new
            {
                Message = "Payment successful.",
                PaymentId = payment_id,
                Status = status,
                ExternalReference = external_reference
            });
        }

        [HttpGet("failure")]
        public IActionResult PaymentFailure()
        {
            return BadRequest("The payment was rejected or cancelled.");
        }

        [HttpGet("pending")]
        public IActionResult PaymentPending()
        {
            return Ok("The payment is pending.");
        }
    }
}
