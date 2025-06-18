using alquimia.Services.Models;

namespace alquimia.Services.Interfaces
{
    public interface IMercadoPagoService
    {
     
        Task<string> GeneratePaymentLinkAsync(CreatePaymentPreferenceDTO dto);
    }
}