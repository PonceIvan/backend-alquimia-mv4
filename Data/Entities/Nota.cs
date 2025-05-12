
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backendAlquimia.Data.Entities
{
    public class Nota
    {
        public int Id { get; set; }                         

        [StringLength(80)]
        public string Nombre { get; set; }

        public int IdIntensidad { get; set; }
        public Intensidad Intensidad { get; set; } = new();
        public int FamiliaOlfativaId { get; set; }
        public FamiliaOlfativa FamiliaOlfativa { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }
        public List<int> NotasCompatiblesIds { get; set; } = new List<int>();
        public List<int> NotasIncompatiblesIds { get; set; } = new List<int>();

        // Propiedades de navegación (opcional)
        [NotMapped] // Esto evita que EF intente mapearlas
        public List<Nota> NotasCompatibles { get; set; }
        [NotMapped]
        public List<Nota> NotasIncompatibles { get; set; }
        [NotMapped]
        public int ValorIntensidad => Intensidad?.Grado ?? 0;
    }
}
