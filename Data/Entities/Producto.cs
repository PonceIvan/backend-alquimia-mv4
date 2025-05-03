using System.ComponentModel.DataAnnotations;


namespace backendAlquimia.Data.Entities;

public class Producto
{
    public int id { get; set; }
    public TipoProducto tipoProducto { get; set; }
    [MaxLength(30)]
    public string name { get; set; }
    [MaxLength(50)]
    public string description { get; set; }
    public float price { get; set; }
    public int stock { get; set; }
}