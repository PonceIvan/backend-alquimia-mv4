using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alquimia.Services.Services.Models
{
    public class DesignDTO
    {
        public string Text { get; set; } = null!;
        public int? Volume { get; set; }
        public string? Image { get; set; }
        public string? Shape { get; set; }
        public string? LabelColor { get; set; }
        public string? Typography { get; set; }
        public string? TextColor { get; set; }
    }

}

/*front
 
 <!-- Vista previa simple -->
<div id="preview">
    <p style="color: {{ textColor }}; font-family: {{ typography }};">
        {{ text }}
    </p>
    <div style="background-color: {{ labelColor }};">
        Shape: {{ shape }}, Volume: {{ volume }} ml
    </div>
    <img :src="imageUrl" alt="Imagen" />
</div>

<button @click="descargarPdf()">Descargar PDF</button>
*/