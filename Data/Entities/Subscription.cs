using System;
using System.Collections.Generic;

namespace backendAlquimia.Data.Entities;

public partial class Subscription
{
    public int Id { get; set; }

    public decimal Monto { get; set; }

    public int? IdEstado { get; set; }

    public virtual Status? IdEstadoNavigation { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
