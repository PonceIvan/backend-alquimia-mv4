using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

[Index("Familia1Id", Name = "IX_CompatibilidadesFamilias_Familia1Id")]
[Index("Familia2Id", Name = "IX_CompatibilidadesFamilias_Familia2Id")]
public partial class CompatibilidadesFamilia
{
    [Key]
    public int Id { get; set; }

    public int Familia1Id { get; set; }

    public int Familia2Id { get; set; }

    public int GradoDeCompatibilidad { get; set; }

    [ForeignKey("Familia1Id")]
    [InverseProperty("CompatibilidadesFamiliaFamilia1s")]
    public virtual FamiliasOlfativa Familia1 { get; set; } = null!;

    [ForeignKey("Familia2Id")]
    [InverseProperty("CompatibilidadesFamiliaFamilia2s")]
    public virtual FamiliasOlfativa Familia2 { get; set; } = null!;
}
