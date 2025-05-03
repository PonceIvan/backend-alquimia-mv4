namespace backendAlquimia.Data.Entities;
public class Combinacion
{
    public ICollection<Nota> NotaSalida { get; set; }
    public ICollection<Nota> NotaCorazon { get; set; }
    public ICollection<Nota> NotaFondo { get; set; }
}