using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tests.Data.Entities;

[Table("Quiz")]
public partial class Quiz
{
    [Key]
    public int Id { get; set; }

    public int? IdPregunta { get; set; }

    [ForeignKey("IdPregunta")]
    [InverseProperty("Quizzes")]
    public virtual Question? IdPreguntaNavigation { get; set; }

    [InverseProperty("IdQuizNavigation")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
