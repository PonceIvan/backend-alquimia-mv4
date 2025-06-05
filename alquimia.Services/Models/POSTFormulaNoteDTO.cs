using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alquimia.Services.Models
{
    public class POSTFormulaNoteDTO
    {
        public POSTNoteIdDTO Note1 { get; set; } = null!;
        public POSTNoteIdDTO? Note2 { get; set; }
        public POSTNoteIdDTO? Note3 { get; set; }
        public POSTNoteIdDTO? Note4 { get; set; }
    }
}
