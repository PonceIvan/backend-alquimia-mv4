using alquimia.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace alquimia.Api.Controllers
{
    [ApiController]
    [Route("chatbot")]
    public class ChatbotController : ControllerBase
    {
        private readonly IChatbotService _chatbotService;

        public ChatbotController(IChatbotService chatbotService)
        {
            _chatbotService = chatbotService;
        }

        [HttpGet("node/{id}")]
        public async Task<IActionResult> GetNode(string id)
        {
            // Buscar primero en nodos estáticos
            var node = await _chatbotService.GetNodeByIdAsync(id);
            if (node != null) return Ok(node);

            // Luego buscar en nodos dinámicos
            var dynamicNode = await _chatbotService.GetDynamicNodeAsync(id);
            if (dynamicNode != null) return Ok(dynamicNode);

            return NotFound("Nodo no encontrado");
        }

        [HttpGet("start")]
        public async Task<IActionResult> Start()
        {
            // Ahora "inicio" es estático, no dinámico
            var node = await _chatbotService.GetNodeByIdAsync("inicio");
            if (node != null) return Ok(node);

            return NotFound("Nodo 'inicio' no encontrado");
        }
    }
}
