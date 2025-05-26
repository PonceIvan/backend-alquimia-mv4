using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tests.Data.Entities;

[Index("Familia1Id", Name = "IX_CompatibilidadesFamilias_Familia1Id")]
[Index("Familia2Id", Name = "IX_CompatibilidadesFamilias_Familia2Id")]
[Index("FamiliaMenor", "FamiliaMayor", Name = "IX_Unique_Compatibilities", IsUnique = true)]
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
