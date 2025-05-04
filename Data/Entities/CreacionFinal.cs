using System.ComponentModel.DataAnnotations;
namespace backendAlquimia.Data.Entities;
public class CreacionFinal
{
    public int Id { get; set; }
    public Formula Formula { get; set; }
    public List<Producto> Productos { get; set; }
}