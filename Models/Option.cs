using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backendAlquimia.Models;

public partial class Option
{
    [Key]
    public int Id { get; set; }

    [StringLength(256)]
    public string? Option1 { get; set; }

    [StringLength(256)]
    public string? Option2 { get; set; }

    [StringLength(256)]
    public string? Option3 { get; set; }

    [StringLength(256)]
    public string? Option4 { get; set; }

    public byte[]? Image1 { get; set; }

    public byte[]? Image2 { get; set; }

    public byte[]? Image3 { get; set; }

    public byte[]? Image4 { get; set; }

    [InverseProperty("IdOpcionesNavigation")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
