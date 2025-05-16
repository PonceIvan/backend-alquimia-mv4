using System.ComponentModel.DataAnnotations.Schema;

namespace backendAlquimia.Data.Entities
{
    public class CompatibilidadesFamilias
    {
        public int Id { get; set; }

        // FK a FamiliaOlfativa (familia1)
        [ForeignKey("Familia1")]
        public int Familia1Id { get; set; }
        public FamiliaOlfativa Familia1 { get; set; }

        // FK a FamiliaOlfativa (familia2)
        [ForeignKey("Familia2")]
        public int Familia2Id { get; set; }
        public FamiliaOlfativa Familia2 { get; set; }

        // Grado de compatibilidad entre familia1 y familia2
        public int GradoDeCompatibilidad { get; set; }
    }
}
