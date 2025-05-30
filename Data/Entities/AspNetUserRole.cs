using System;
using System.Collections.Generic;

namespace backendAlquimia.Data.Entities;

public partial class AspNetUserRole
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public virtual User User { get; set; } = null!;
}
