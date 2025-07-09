namespace alquimia.Services.Models
{
    public class ChatNode
    {
        public string Id { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public List<ChatOption> Options { get; set; } = new();
        public string Type { get; set; } = "decision";
        public string? InputType { get; set; }
        public string? NextNodeId { get; set; }
    }
}

