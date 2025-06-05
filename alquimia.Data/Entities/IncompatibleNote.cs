using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Entities;

[PrimaryKey("NotaId", "NotaIncompatibleId")]
public partial class IncompatibleNote
{
    [Key]
    public int NotaId { get; set; }

    [Key]
    public int NotaIncompatibleId { get; set; }

    public int NotaMenor { get; set; }

    public int NotaMayor { get; set; }

    [ForeignKey("NotaId")]
    [InverseProperty("IncompatibleNoteNota")]
    public virtual Note Nota { get; set; } = null!;

    [ForeignKey("NotaIncompatibleId")]
    [InverseProperty("IncompatibleNoteNotaIncompatibles")]
    public virtual Note NotaIncompatible { get; set; } = null!;
}
