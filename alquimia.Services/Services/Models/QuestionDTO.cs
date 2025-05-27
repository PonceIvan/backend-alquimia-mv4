using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alquimia.Services.Services.Models
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Pregunta { get; set; }
        public List<OptionDTO> Opciones { get; set; }
    }
}
