using System.ComponentModel.DataAnnotations;
namespace backendAlquimia.Data.Entities;
public class CreacionFinal
{
    public int Id { get; set; }
    public Formula Formula { get; set; }
    public List<Producto> Productos { get; set; }
    public int CreadorId { get; set; }
    public int IdPedidoProveedor { get; set }
    public double ConcentracionAlcohol { get; set; }
    public double ConcentracionAgua { get; set; }
    public double ConcentracionEsencia { get; set; }

}