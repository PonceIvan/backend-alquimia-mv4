using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alquimia.Data.Data.Entities;

namespace alquimia.Services.Services.Models
{
    public class GETNoteDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family{get; set;}                
        public string Sector { get; set; }
        public string Description { get; set; }
        public TimeOnly Duration { get; set; }
        
    }
}
