using System;
using System.Collections.Generic;

namespace backendAlquimia.Data.Entities;

public partial class FamilyCompatibility
{
    public int Id { get; set; }

    public int Familia1Id { get; set; }

    public int Familia2Id { get; set; }

    public int GradoDeCompatibilidad { get; set; }

    public int FamiliaMenor { get; set; }

    public int FamiliaMayor { get; set; }

    public virtual OlfactoryFamily Familia1 { get; set; } = null!;

    public virtual OlfactoryFamily Familia2 { get; set; } = null!;
}
