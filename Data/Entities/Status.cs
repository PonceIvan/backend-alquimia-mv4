using System;
using System.Collections.Generic;

namespace backendAlquimia.Data.Entities;

public partial class Status
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
