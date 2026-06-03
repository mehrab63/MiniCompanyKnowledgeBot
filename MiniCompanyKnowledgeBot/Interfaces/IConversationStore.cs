using MiniCompanyKnowledgeBot.Models;

namespace MiniCompanyKnowledgeBot.Interfaces
{
    public interface IConversationStore
    {
        Task AddMessageAsync(
            string sessionId,
            string role,
            string content);

        Task<List<Message>> GetHistoryAsync(string sessionId);
    }
}
