using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backendAlquimia.Data.Entities;
public class Admin : Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; }
}