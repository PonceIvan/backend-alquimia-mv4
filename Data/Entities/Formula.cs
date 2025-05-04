using System.ComponentModel.DataAnnotations;

namespace backendAlquimia.Data.Entities
{
    public class Formula
    {
        public int Id { get; set; }
        public int IntensidadId { get; set; }
        [Required]
        public Intensidad Intensidad { get; set; }
        public int CombinacionId { get; set; }
        public Combinacion Combinacion { get; set; }

        
    }
}
