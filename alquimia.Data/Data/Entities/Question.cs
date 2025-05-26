using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

public partial class Question
{
    [Key]
    public int Id { get; set; }

    [StringLength(256)]
    public string Pregunta { get; set; } = null!;

    public int? IdOpciones { get; set; }

    [ForeignKey("IdOpciones")]
    [InverseProperty("Questions")]
    public virtual Option? IdOpcionesNavigation { get; set; }

    [InverseProperty("IdPreguntaNavigation")]
    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
}
