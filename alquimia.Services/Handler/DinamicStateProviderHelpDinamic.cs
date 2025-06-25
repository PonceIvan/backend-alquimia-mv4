namespace alquimia.Services.Handler
{
    using global::alquimia.Services.Interfaces;
    using global::alquimia.Services.Models;

    namespace alquimia.Services.Handler
    {
        public class DinamicStateProviderHelp : IChatDynamicNodeHandler
        {
            public bool CanHandle(string nodeId) => nodeId == "proveedor-ayuda-estado-dinamico";

            public async Task<ChatNode?> HandleAsync(string nodeId)
            {
                var msg = "Para conocer el estado de tu cuenta, por favor ingresá tu correo electrónico:";

                return new ChatNode
                {
                    Id = "proveedor-ayuda-estado-dinamico",
                    Message = msg,
                    Type = "input",
                    InputType = "email",
                    NextNodeId = "proveedor-ayuda-estado-respuesta-dinamico",
                };
            }
        }
    }

}
