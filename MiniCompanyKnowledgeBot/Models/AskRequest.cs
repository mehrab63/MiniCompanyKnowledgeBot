namespace MiniCompanyKnowledgeBot.Models
{
    public class AskRequest
    {
        public string SessionId { get; set; } = string.Empty;
        public string Question { get; set; } = string.Empty;
    }
}
