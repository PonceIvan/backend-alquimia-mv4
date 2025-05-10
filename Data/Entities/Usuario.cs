using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace backendAlquimia.Data.Entities;


public class Usuario : IdentityUser<int>
{
    [Key]
    public int Id { get; set; }
    public string Name;

}
