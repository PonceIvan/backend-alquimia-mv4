namespace backendAlquimia.Data.Entities
{
    public class Proveedor : Usuario
    {
        public string Cuil { get; set; }
        public List<Producto> Productos { get; set; } = new();
    }
}
