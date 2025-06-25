namespace alquimia.Services.Models
{
    public class ChatNode
    {
        public string Id { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public List<ChatOption> Options { get; set; } = new();
        public string Type { get; set; } = "decision"; // "decision" o "generated" en el futuro
    }
}
