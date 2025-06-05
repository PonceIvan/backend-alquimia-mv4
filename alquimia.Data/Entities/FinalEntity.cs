using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Entities;

[Table("FinalEntity")]
public partial class FinalEntity
{
    [Key]
    public int Id { get; set; }

    public int? IdUsuario { get; set; }

    public int? ProductosId { get; set; }

    public int? DesignId { get; set; }

    [ForeignKey("DesignId")]
    [InverseProperty("FinalEntities")]
    public virtual Design? Design { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("FinalEntities")]
    public virtual User? IdUsuarioNavigation { get; set; }

    [ForeignKey("ProductosId")]
    [InverseProperty("FinalEntities")]
    public virtual Product? Productos { get; set; }
}
