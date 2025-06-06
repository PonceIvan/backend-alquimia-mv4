namespace alquimia.Services.Models
{
    public class ProviderDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool EsAprobado { get; set; }
    }
}