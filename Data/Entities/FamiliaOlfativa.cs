
using System.ComponentModel.DataAnnotations;

namespace backendAlquimia.Data.Entities
{
    public class FamiliaOlfativa
    {
        public int Id { get; set; }                       

        [StringLength(80)]
        public string Nombre { get; set; }                  

        public System.Collections.Generic.ICollection<Nota> Notas { get; set; }
        [MaxLength(100)]
        public string Description {  get; set; }
    }
}
