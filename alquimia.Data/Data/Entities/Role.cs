using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

public partial class Role : IdentityRole<int>
{
    [Key]
    public int Id { get; set; }

    [StringLength(256)]
    public string? Name { get; set; }
}
