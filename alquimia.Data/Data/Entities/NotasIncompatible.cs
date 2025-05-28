using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

[Keyless]
public partial class NotasIncompatible
{
    [StringLength(50)]
    public string Nota { get; set; } = null!;

    public int NotaId { get; set; }

    public int NotaIncompatibleId { get; set; }

    [StringLength(50)]
    public string Incompatible { get; set; } = null!;
}
