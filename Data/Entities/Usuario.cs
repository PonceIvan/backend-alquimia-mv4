using System.ComponentModel.DataAnnotations;
namespace backendAlquimia.Data.Entities;


public class Usuario
{
    [Key]
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

}
