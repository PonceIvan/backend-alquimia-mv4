using System;
using System.Collections.Generic;

namespace backendAlquimia.Data.Entities;

public partial class Note
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int FamiliaOlfativaId { get; set; }

    public string Descripcion { get; set; } = null!;

    public int PiramideOlfativaId { get; set; }

    public virtual OlfactoryFamily FamiliaOlfativa { get; set; } = null!;

    public virtual ICollection<FormulaNote> FormulaNoteNotaId1Navigations { get; set; } = new List<FormulaNote>();

    public virtual ICollection<FormulaNote> FormulaNoteNotaId2Navigations { get; set; } = new List<FormulaNote>();

    public virtual ICollection<FormulaNote> FormulaNoteNotaId3Navigations { get; set; } = new List<FormulaNote>();

    public virtual ICollection<FormulaNote> FormulaNoteNotaId4Navigations { get; set; } = new List<FormulaNote>();

    public virtual ICollection<IncompatibleNote> IncompatibleNoteNota { get; set; } = new List<IncompatibleNote>();

    public virtual ICollection<IncompatibleNote> IncompatibleNoteNotaIncompatibles { get; set; } = new List<IncompatibleNote>();

    public virtual OlfactoryPyramid PiramideOlfativa { get; set; } = null!;
}
