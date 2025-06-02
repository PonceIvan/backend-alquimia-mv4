using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backendAlquimia.Models;

[Table("Status")]
public partial class Status
{
    [Key]
    public int Id { get; set; }

    [StringLength(30)]
    public string Description { get; set; } = null!;

    [InverseProperty("Estado")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("IdEstadoNavigation")]
    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    [InverseProperty("IdEstadoNavigation")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
