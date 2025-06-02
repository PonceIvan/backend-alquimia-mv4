using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backendAlquimia.Models;

[Keyless]
public partial class NotasIncompatible
{
    public int NotaId { get; set; }

    [StringLength(50)]
    public string Nota { get; set; } = null!;

    public int NotaIncompatibleId { get; set; }

    [StringLength(50)]
    public string NotaIncompatible { get; set; } = null!;
}
