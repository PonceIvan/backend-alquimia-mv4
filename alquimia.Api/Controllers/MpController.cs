using alquimia.Services;
using alquimia.Services.Interfaces;
using alquimia.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace Alquimia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MpController : ControllerBase
    {
        private readonly IMercadoPagoService _mpService;

        public MpController(IMercadoPagoService mpService)
        {
            _mpService = mpService;
        }

        /// <summary>
        /// Crea la preferencia y devuelve la URL init_point.
        /// Body: { productVariantId, quantity, externalReference }
        /// </summary>
        [HttpPost("generate-link")]
        public async Task<IActionResult> GeneratePaymentLink(
            [FromBody] CreatePaymentPreferenceDTO dto)
        {
            try
            {
                var url = await _mpService.GeneratePaymentLinkAsync(dto);
                return Ok(new { url });
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(knf.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // ────────────────────── CALLBACKS ──────────────────────
        // Mercado Pago redirige aquí según el estado del pago.
        // Si deseas manejar IPN/webhooks, crea otro endpoint aparte.

        [HttpGet("success")]
        public IActionResult PaymentSuccess(
            [FromQuery] string payment_id,
            [FromQuery] string status,
            [FromQuery] string external_reference)
            => Redirect(
                $"{Request.Scheme}://pago-exitoso.onrender.com/success.html" +
                $"?payment_id={payment_id}&status={status}&external_reference={external_reference}");

        [HttpGet("failure")]
        public IActionResult PaymentFailure()
            => Redirect("https://pago-exitoso.onrender.com/failure.html");

        [HttpGet("pending")]
        public IActionResult PaymentPending()
            => Redirect("https://pago-exitoso.onrender.com/pending.html");
    }
}
