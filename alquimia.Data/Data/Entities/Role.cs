using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace alquimia.Data.Data.Entities;

public partial class Role : IdentityRole
{
    [Key]
    public int Id { get; set; }

    [StringLength(256)]
    public string? Name { get; set; }
}
