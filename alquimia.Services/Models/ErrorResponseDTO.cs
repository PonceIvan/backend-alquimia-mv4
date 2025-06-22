namespace alquimia.Services.Models
{
    public class ErrorResponseDTO
    {
        public int Status { get; set; }
        public string Error { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string? TraceId { get; set; }
    }
}
