using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

[Table("AspNetRoles")]          // 👈 mapea a la tabla que ya existe
public partial class Role : IdentityRole<int>
{


    [StringLength(256)]
    public string? Name { get; set; }

   
}
