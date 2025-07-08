using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace alquimia.Data.Entities;

[Table("Notes")]
public partial class Note
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(50)]
    public string Name { get; set; } = null!;

    [Required]
    public int OlfactoryFamilyId { get; set; }

    [Required, StringLength(50)]
    public string Description { get; set; } = null!;

    [Required]
    public int OlfactoryPyramidId { get; set; }

    [StringLength(256)]
    public string? Image { get; set; }

    [ForeignKey("OlfactoryFamilyId")]
    [InverseProperty("Notes")]
    public virtual OlfactoryFamily OlfactoryFamily { get; set; } = null!;

    [ForeignKey("OlfactoryPyramidId")]
    [InverseProperty("Notes")]
    public virtual OlfactoryPyramid OlfactoryPyramid { get; set; } = null!;

    [InverseProperty("NotaId1Navigation")]
    public virtual ICollection<FormulaNote> FormulaNoteNotaId1Navigations { get; set; } = new List<FormulaNote>();

    [InverseProperty("NotaId2Navigation")]
    public virtual ICollection<FormulaNote> FormulaNoteNotaId2Navigations { get; set; } = new List<FormulaNote>();

    [InverseProperty("NotaId3Navigation")]
    public virtual ICollection<FormulaNote> FormulaNoteNotaId3Navigations { get; set; } = new List<FormulaNote>();

    [InverseProperty("NotaId4Navigation")]
    public virtual ICollection<FormulaNote> FormulaNoteNotaId4Navigations { get; set; } = new List<FormulaNote>();

    [InverseProperty("Nota")]
    public virtual ICollection<IncompatibleNote> IncompatibleNoteNota { get; set; } = new List<IncompatibleNote>();

    [InverseProperty("NotaIncompatible")]
    public virtual ICollection<IncompatibleNote> IncompatibleNoteNotaIncompatibles { get; set; } = new List<IncompatibleNote>();
}
