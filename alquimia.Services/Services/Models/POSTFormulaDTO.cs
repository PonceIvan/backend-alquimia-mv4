using alquimia.Services.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace backendAlquimia.Models
{
    public class POSTFormulaDTO
{
    //public List<int> NotasSalidaIds { get; set; }
    //public List<int> NotasCorazonIds { get; set; }
    
    //public List<int> NotasFondoIds { get; set; }
        public int IdIntensidad { get; set; }
        public int IdCreador { get; set; }
        [Required]
        public POSTFormulaNoteDTO TopNotes { get; set; }
        [Required]
        public POSTFormulaNoteDTO HeartNotes { get; set; }
        [Required]
        public POSTFormulaNoteDTO BaseNotes { get; set; }
    }
}
