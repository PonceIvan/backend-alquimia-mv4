using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

public partial class FamiliasOlfativa
{
    [Key]
    public int Id { get; set; }

    [StringLength(80)]
    public string Nombre { get; set; } = null!;

    [StringLength(100)]
    public string Description { get; set; } = null!;

    [InverseProperty("Familia1")]
    public virtual ICollection<CompatibilidadesFamilia> CompatibilidadesFamiliaFamilia1s { get; set; } = new List<CompatibilidadesFamilia>();

    [InverseProperty("Familia2")]
    public virtual ICollection<CompatibilidadesFamilia> CompatibilidadesFamiliaFamilia2s { get; set; } = new List<CompatibilidadesFamilia>();

    [InverseProperty("FamiliaOlfativa")]
    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();
}
