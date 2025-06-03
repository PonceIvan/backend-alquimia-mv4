using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace alquimia.Data.Data.Entities;

[Table("OlfactoryPyramid")]
public partial class OlfactoryPyramid
{
    [Key]
    public int Id { get; set; }

    public string Sector { get; set; } = null!;

    public TimeOnly Duracion { get; set; }

    [InverseProperty("OlfactoryPyramid")]
    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
