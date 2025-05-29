using System;
using System.Collections.Generic;

namespace backendAlquimia.Data.Entities;

public partial class Intensity
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Formula> Formulas { get; set; } = new List<Formula>();
}
