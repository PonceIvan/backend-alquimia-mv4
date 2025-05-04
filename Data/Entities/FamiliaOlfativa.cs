
using System.ComponentModel.DataAnnotations;

namespace backendAlquimia.Data.Entities
{
    public class FamiliaOlfativa
    {
        public int Id { get; set; }                       

        [StringLength(80)]
        public string Nombre { get; set; }

        [StringLength(250)]
        public string Nota { get; set; }                  

        public System.Collections.Generic.ICollection<Nota> Notas { get; set; }
    }
}
