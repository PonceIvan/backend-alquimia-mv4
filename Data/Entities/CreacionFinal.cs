using System.ComponentModel.DataAnnotations;
namespace backendAlquimia.Data.Entities;
public class CreacionFinal
{
    public Formula formula { get; set; }
    public List<Producto> productos { get; set; }
}