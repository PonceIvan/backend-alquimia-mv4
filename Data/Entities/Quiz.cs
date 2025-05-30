using System;
using System.Collections.Generic;

namespace backendAlquimia.Data.Entities;

public partial class Quiz
{
    public int Id { get; set; }

    public int? IdPregunta { get; set; }

    public virtual Question? IdPreguntaNavigation { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
