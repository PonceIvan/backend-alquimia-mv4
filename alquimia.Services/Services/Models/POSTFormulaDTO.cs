using alquimia.Services.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace backendAlquimia.Models
{
    public class POSTFormulaDTO
    {
        [Required]
        public int IntensityId { get; set; }
        [Required]
        public int CreatorId { get; set; }
        [Required]
        public POSTFormulaNoteDTO TopNotes { get; set; }
        [Required]
        public POSTFormulaNoteDTO HeartNotes { get; set; }
        [Required]
        public POSTFormulaNoteDTO BaseNotes { get; set; }
    }
}
