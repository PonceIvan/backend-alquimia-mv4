using System.ComponentModel.DataAnnotations;


namespace backendAlquimia.Data.Entities;

public class Producto
{
    public int id { get; set; }
    public TipoProducto TipoProducto { get; set; }
    [MaxLength(30)]
    public string Name { get; set; }
    [MaxLength(50)]
    public string Description { get; set; }
    public float Price { get; set; }
    public int Stock { get; set; }
    public Proveedor Proveedor { get; set; }
}