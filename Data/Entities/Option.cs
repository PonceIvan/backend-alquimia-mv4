using System;
using System.Collections.Generic;

namespace backendAlquimia.Data.Entities;

public partial class Option
{
    public int Id { get; set; }

    public string? Option1 { get; set; }

    public string? Option2 { get; set; }

    public string? Option3 { get; set; }

    public string? Option4 { get; set; }

    public byte[]? Image1 { get; set; }

    public byte[]? Image2 { get; set; }

    public byte[]? Image3 { get; set; }

    public byte[]? Image4 { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
