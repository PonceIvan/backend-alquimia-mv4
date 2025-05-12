using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backendAlquimia.Data.Entities;
public class Opinion
{
    public int Id { get; set; }
    public int IdUsuario { get; set; }
    public string Description { get; set; }
    public int IdProveedor { get; set; }
    public DateTime FechaPublicacion { get; set; }
}