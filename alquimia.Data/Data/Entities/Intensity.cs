using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace alquimia.Data.Data.Entities;

public partial class Intensity
{
    [Key]
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    [StringLength(100)]
    public string Description { get; set; }

    [InverseProperty("Intensidad")]
    public virtual ICollection<Formula> Formulas { get; set; } = new List<Formula>();
}
