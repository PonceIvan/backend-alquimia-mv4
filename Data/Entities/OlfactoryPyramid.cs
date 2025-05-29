using System;
using System.Collections.Generic;

namespace backendAlquimia.Data.Entities;

public partial class OlfactoryPyramid
{
    public int Id { get; set; }

    public string Sector { get; set; } = null!;

    public TimeOnly Duracion { get; set; }

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
