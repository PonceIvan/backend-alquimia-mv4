using System.ComponentModel.DataAnnotations;

namespace backendAlquimia.Data.Entities
{
    public class Formula
    {
        public int Id { get; set; }
        public int IntensidadId { get; set; }
        public int CombinacionId { get; set; }

        
    }
}
