using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alquimia.Services.Models
{
    public class UserProfileDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool EsProveedor { get; set; }
        public string? CUIL { get; set; }
        public string? Ubicacion { get; set; }
        public string? CodigoPostal { get; set; }
    }
}
