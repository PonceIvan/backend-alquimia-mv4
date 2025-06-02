using alquimia.Services.Services.Models;

namespace backendAlquimia.Models
{
    public class GETFormulaDTO
    {
        public GETFormulaNoteDTO NotasSalidaIds { get; set; }
        public GETFormulaNoteDTO NotasCorazonIds { get; set; }
        public GETFormulaNoteDTO NotasFondoIds { get; set; }
        public IntensityDTO Intensity { get; set; }
        public int IdCreador { get; set; }
        public double ConcentracionAlcohol { get; set; }
        public double ConcentracionAgua { get; set; }
        public double ConcentracionEsencia { get; set; }
    }
}