using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

public partial class Pregunta
{
    [Key]
    public int Id { get; set; }

    [Column("Pregunta")]
    [StringLength(256)]
    public string Pregunta1 { get; set; } = null!;

    public int? IdOpciones { get; set; }

    [ForeignKey("IdOpciones")]
    [InverseProperty("Pregunta")]
    public virtual Opcione? IdOpcionesNavigation { get; set; }

    [InverseProperty("IdPreguntaNavigation")]
    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
}
