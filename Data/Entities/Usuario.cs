using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace backendAlquimia.Data.Entities;


public class Usuario : IdentityUser<int>
{
    public string Name { get; set; }

}
