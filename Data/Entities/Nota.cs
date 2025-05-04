
using System.ComponentModel.DataAnnotations;

namespace backendAlquimia.Data.Entities
{
    public class Nota
    {
        public int Id { get; set; }                       

        [StringLength(80)]
        public string Nombre { get; set; }

        
        public int FamiliaOlfativaId { get; set; }
        public FamiliaOlfativa FamiliaOlfativa { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }
    }
}
