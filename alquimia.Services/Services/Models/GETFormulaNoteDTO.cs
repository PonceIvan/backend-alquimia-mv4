using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alquimia.Services.Services.Models
{
    public class GETFormulaNoteDTO
    {
        public GETNoteDTO Note1 { get; set; }
        public GETNoteDTO? Note2 { get; set; }
        public GETNoteDTO? Note3 { get; set; }
        public GETNoteDTO? Note4 { get; set; }
    }
}
