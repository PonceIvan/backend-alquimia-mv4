using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace alquimia.Data.Entities
{
    public partial class Role : IdentityRole<int>
    {
        //public int Id { get; set; }

        //public string? Name { get; set; }
    }
}


