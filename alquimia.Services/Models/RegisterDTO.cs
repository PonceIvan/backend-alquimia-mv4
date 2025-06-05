using System.ComponentModel.DataAnnotations;

namespace alquimia.Services.Models
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Rol { get; set; } // "Admin", "Creador", "Proveedor"

    }
}