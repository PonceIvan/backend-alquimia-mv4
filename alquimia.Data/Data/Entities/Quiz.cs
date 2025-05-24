using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

[Table("Quiz")]
public partial class Quiz
{
    [Key]
    public int Id { get; set; }

    public int? IdPregunta { get; set; }

    [ForeignKey("IdPregunta")]
    [InverseProperty("Quizzes")]
    public virtual Pregunta? IdPreguntaNavigation { get; set; }

    [InverseProperty("IdQuizNavigation")]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
