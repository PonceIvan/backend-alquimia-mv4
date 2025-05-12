namespace backendAlquimia.Data.Entities
{
    public class PirameOlfativa
    {
        public int Id {  get; set; }
        public string Nombre { get; set; }
        public List<FamiliaOlfativa> FamiliasOlfativas { get; set; } = new();
    }
}
