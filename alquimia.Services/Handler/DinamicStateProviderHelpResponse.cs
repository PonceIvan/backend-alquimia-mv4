using alquimia.Services.Interfaces;
using alquimia.Services.Models;

namespace alquimia.Services.Handler
{
    public class DinamicStateProviderHelpResponse : IChatDynamicNodeHandler
    {
        //private readonly IUserService _userService;

        //public DinamicStateProviderResponse(IUserService userService)
        //{
        //    _userService = userService;
        //}

        ///admin/getProviderByEmail/email
        public bool CanHandle(string nodeId) => nodeId == "proveedor-ayuda-estado-respuesta";

        public async Task<ChatNode?> HandleAsync(string nodeId)
        {
            // Suponemos que el email llega en un parámetro query (?input=correo)
            var email = nodeId.Split("::").Length > 1 ? nodeId.Split("::")[1] : null;

            if (string.IsNullOrEmpty(email))
            {
                return new ChatNode
                {
                    Id = "proveedor-ayuda-estado-respuesta",
                    Message = "No se recibió un correo válido. Por favor, intentá de nuevo.",
                    Type = "decision",
                    Options = new List<ChatOption>
                {
                    new ChatOption { Label = "Reintentar", NextNodeId = "proveedor-ayuda-estado-dinamico" },
                    new ChatOption { Label = "Volver al menú", NextNodeId = "inicio" }
                }
                };
            }

            //var estado = await _userService.GetAccountStatusByEmailAsync(email); // deberías implementar este método

            return new ChatNode
            {
                Id = "proveedor-ayuda-estado-respuesta",
                //Message = $"El estado de tu cuenta es: {estado}",
                Type = "decision",
                Options = new List<ChatOption>
            {
                new ChatOption { Label = "Volver atrás", NextNodeId = "proveedor-ayuda-dinamico" },
                new ChatOption { Label = "Volver al menú", NextNodeId = "inicio" }
            }
            };
        }
    }
}
