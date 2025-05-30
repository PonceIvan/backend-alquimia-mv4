using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace alquimia.Data.Data.Entities;

[Table("AspNetRoles")]          // 👈 mapea a la tabla que ya existe
public partial class Role : IdentityRole<int>
{

}
