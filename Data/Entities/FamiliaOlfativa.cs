// FamiliaOlfativa.cs
using System.ComponentModel.DataAnnotations;

namespace backendAlquimia.Data.Entities
{
    public class FamiliaOlfativa
    {
        public int Id { get; set; }                       // PK por convención

        [StringLength(80)]
        public string Nombre { get; set; }

        [StringLength(250)]
        public string Nota { get; set; }                  // Descripción breve

        // 1‑a‑N: una familia contiene muchas notas
        public System.Collections.Generic.ICollection<Nota> Notas { get; set; }
    }
}
