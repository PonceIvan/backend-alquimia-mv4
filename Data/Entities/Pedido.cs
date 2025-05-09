namespace backendAlquimia.Data.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public List<Producto> Productos { get; set; }
    }
}
