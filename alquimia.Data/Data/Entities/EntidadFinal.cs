using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

[Table("EntidadFinal")]
public partial class EntidadFinal
{
    [Key]
    public int Id { get; set; }

    public int? IdUsuario { get; set; }

    public int? ProductosId { get; set; }

    public int? DesignId { get; set; }

    [ForeignKey("DesignId")]
    [InverseProperty("EntidadFinals")]
    public virtual Design? Design { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("EntidadFinals")]
    public virtual Usuario? IdUsuarioNavigation { get; set; }

    [ForeignKey("ProductosId")]
    [InverseProperty("EntidadFinals")]
    public virtual Producto? Productos { get; set; }
}
