using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Entities;

public partial class FamilyCompatibility
{
    [Key]
    public int Id { get; set; }

    public int Familia1Id { get; set; }

    public int Familia2Id { get; set; }

    public int GradoDeCompatibilidad { get; set; }

    public int FamiliaMenor { get; set; }

    public int FamiliaMayor { get; set; }

    [ForeignKey("Familia1Id")]
    [InverseProperty("FamilyCompatibilityFamilia1s")]
    public virtual OlfactoryFamily Familia1 { get; set; } = null!;

    [ForeignKey("Familia2Id")]
    [InverseProperty("FamilyCompatibilityFamilia2s")]
    public virtual OlfactoryFamily Familia2 { get; set; } = null!;
}
