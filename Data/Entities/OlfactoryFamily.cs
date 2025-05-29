using System;
using System.Collections.Generic;

namespace backendAlquimia.Data.Entities;

public partial class OlfactoryFamily
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Description { get; set; } = null!;

    public byte[]? Image1 { get; set; }

    public virtual ICollection<FamilyCompatibility> FamilyCompatibilityFamilia1s { get; set; } = new List<FamilyCompatibility>();

    public virtual ICollection<FamilyCompatibility> FamilyCompatibilityFamilia2s { get; set; } = new List<FamilyCompatibility>();

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
