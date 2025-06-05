using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Entities
{
    [Keyless]
    public partial class NotesPyramidFamily
    {
        [StringLength(50)]
        public string Nota { get; set; } = null!;

        [Column("Nota_Id")]
        public int NotaId { get; set; }

        [Column("Sector_Piramide")]
        public string SectorPiramide { get; set; } = null!;

        [StringLength(80)]
        public string Familia { get; set; } = null!;
    }
}


