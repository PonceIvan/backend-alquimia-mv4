using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alquimia.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string destinatario, string asunto, string mensajeHtml);
    }
}
