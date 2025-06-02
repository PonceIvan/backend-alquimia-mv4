using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace backendAlquimia.Models;

[Keyless]
public partial class NotesPyramidFamily
{
    public int Id { get; set; }

    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    public int FamiliaOlfativaId { get; set; }

    [StringLength(80)]
    public string FamiliaOlfativa { get; set; } = null!;

    public int PiramideOlfativaId { get; set; }

    public string SectorPiramide { get; set; } = null!;
}
