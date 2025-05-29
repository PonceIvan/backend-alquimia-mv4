using System;
using System.Collections.Generic;

namespace backendAlquimia.Data.Entities;

public partial class IncompatibleNote
{
    public int NotaId { get; set; }

    public int NotaIncompatibleId { get; set; }

    public int NotaMenor { get; set; }

    public int NotaMayor { get; set; }

    public virtual Note Nota { get; set; } = null!;

    public virtual Note NotaIncompatible { get; set; } = null!;
}
