using alquimia.Services.Interfaces;
using alquimia.Services.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace alquimia.Services
{
    public class MercadoPagoService : IMercadoPagoService
    {
        private readonly IConfiguration _config;
        private readonly IProductService _productService;
        private readonly IHttpClientFactory _httpClientFactory;

        public MercadoPagoService(
            IConfiguration config,
            IProductService productService,
            IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _productService = productService;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GeneratePaymentLinkAsync(CreatePaymentPreferenceDTO dto)
        {
            var variant = await _productService.GetVariantEntityAsync(dto.ProductVariantId);

            var accessToken = _config["MercadoPago:AccessToken"];
            if (string.IsNullOrWhiteSpace(accessToken))
                throw new InvalidOperationException("Access Token de Mercado Pago no configurado.");

            using var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            var body = new
            {
                items = new[]
                {
                    new
                    {
                        id          = variant.Id.ToString(),
                        title       = variant.Product.Name,
                        description = variant.Product.Description,
                        quantity    = dto.Quantity,
                        unit_price  = variant.Price,
                        currency_id = _config["MercadoPago:Currency"] ?? "ARS",
                        category_id = "others"
                    }
                },
                back_urls = new
                {
                    success = _config["MercadoPago:SuccessUrl"],
                    failure = _config["MercadoPago:FailureUrl"],
                    pending = _config["MercadoPago:PendingUrl"]
                },
                auto_return = "approved",
                external_reference = dto.ExternalReference ?? $"variant-{variant.Id}"
            };

            var jsonBody = JsonSerializer.Serialize(body);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(
                "https://api.mercadopago.com/checkout/preferences", content);

            var responseText = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException(
                    $"MercadoPago error {response.StatusCode}: {responseText}");

            using var doc = JsonDocument.Parse(responseText);
            var initPoint = doc.RootElement.GetProperty("init_point").GetString();

            return initPoint ?? throw new ApplicationException("Respuesta de Mercado Pago sin init_point.");
        }
    }
}
