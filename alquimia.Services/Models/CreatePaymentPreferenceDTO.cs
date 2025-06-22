namespace alquimia.Services.Models
{


    public class CreatePaymentPreferenceDTO
    {
       
        public int ProductVariantId { get; set; }

       
        public int Quantity { get; set; } = 1;

    
        public string? ExternalReference { get; set; }
    }
}
