using alquimia.Services.Interfaces;
using alquimia.Services.Models;

namespace alquimia.Services.Handler
{
    public class DinamicProviderHelp : IChatDynamicNodeHandler
    {
        public bool CanHandle(string nodeId) => nodeId == "proveedor-ayuda-dinamico";

        public async Task<ChatNode?> HandleAsync(string nodeId)
        {
            var msg = "¡Perfecto! Si ya sos proveedor en Alquimia, podés elegir alguna de estas opciones para recibir ayuda:";

            return new ChatNode
            {
                Id = "proveedor-ayuda-dinamico",
                Message = msg,
                Type = "decision",
                Options = new List<ChatOption>
                    {
                        new ChatOption { Label = "¿Cómo gestiono mis productos?", NextNodeId = "proveedor-ayuda-gestion" },
                        new ChatOption { Label = "Quiero conocer el estado de mi cuenta", NextNodeId = "proveedor-ayuda-estado-dinamico" },
                        new ChatOption { Label = "Volver atrás", NextNodeId = "proveedor" },
                        new ChatOption { Label = "Volver al menú principal", NextNodeId = "inicio" }
                    }
            };
        }
    }
}
