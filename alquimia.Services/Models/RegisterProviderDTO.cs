using System.ComponentModel.DataAnnotations;

namespace alquimia.Services.Models
{
    public class RegisterProviderDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        public string Empresa { get; set; } = "";
        public string Cuil { get; set; } = "";
        public string Rubro { get; set; } = "";
        public string OtroProducto { get; set; } = "";
        public string TarjetaNumero { get; set; } = "";
        public string TarjetaVencimiento { get; set; } = "";
        public string TarjetaCVC { get; set; } = "";
    }
}