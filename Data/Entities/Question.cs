using System;
using System.Collections.Generic;

namespace backendAlquimia.Data.Entities;

public partial class Question
{
    public int Id { get; set; }

    public string Pregunta { get; set; } = null!;

    public int? IdOpciones { get; set; }

    public virtual Option? IdOpcionesNavigation { get; set; }

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
}
