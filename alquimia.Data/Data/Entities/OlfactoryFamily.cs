using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

public partial class OlfactoryFamily
{
    [Key]
    public int Id { get; set; }

    [StringLength(80)]
    public string Nombre { get; set; } = null!;

    [StringLength(100)]
    public string Description { get; set; } = null!;

    public byte[]? Image1 { get; set; }

    [InverseProperty("Familia1")]
    public virtual ICollection<FamilyCompatibility> FamilyCompatibilityFamilia1s { get; set; } = new List<FamilyCompatibility>();

    [InverseProperty("Familia2")]
    public virtual ICollection<FamilyCompatibility> FamilyCompatibilityFamilia2s { get; set; } = new List<FamilyCompatibility>();

    [InverseProperty("FamiliaOlfativa")]
    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
