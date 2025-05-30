using System;
using System.Collections.Generic;

namespace backendAlquimia.Data.Entities;

public partial class Formula
{
    public int Id { get; set; }

    public int FormulaCorazon { get; set; }

    public int FormulaSalida { get; set; }

    public int FormulaFondo { get; set; }

    public int IntensidadId { get; set; }

    public double ConcentracionAlcohol { get; set; }

    public double ConcentracionAgua { get; set; }

    public double ConcentracionEsencia { get; set; }

    public int? CreadorId { get; set; }

    public virtual FormulaNote FormulaCorazonNavigation { get; set; } = null!;

    public virtual FormulaNote FormulaFondoNavigation { get; set; } = null!;

    public virtual FormulaNote FormulaSalidaNavigation { get; set; } = null!;

    public virtual Intensity Intensidad { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
