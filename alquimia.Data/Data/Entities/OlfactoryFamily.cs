using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace alquimia.Data.Data.Entities;

public partial class OlfactoryFamily
{
    [Key]
    public int Id { get; set; }

    [StringLength(80)]
    public string Nombre { get; set; } = null!;

    [StringLength(100)]
    public string Description { get; set; } = null!;

    public string? Image1 { get; set; }

    [InverseProperty("Familia1")]
    public virtual ICollection<FamilyCompatibility> FamilyCompatibilityFamilia1s { get; set; } = new List<FamilyCompatibility>();

    [InverseProperty("Familia2")]
    public virtual ICollection<FamilyCompatibility> FamilyCompatibilityFamilia2s { get; set; } = new List<FamilyCompatibility>();

    [InverseProperty("OlfactoryFamily")]
    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
