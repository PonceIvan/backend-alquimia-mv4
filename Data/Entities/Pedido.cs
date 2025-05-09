namespace backendAlquimia.Data.Entities
{
    public class Pedido
    {
        public int id { get; set; }
        public List<Producto> productos { get; set; } = new List<Producto>();
    }
}
