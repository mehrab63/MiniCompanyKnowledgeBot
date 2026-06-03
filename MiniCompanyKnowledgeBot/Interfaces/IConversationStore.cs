using MiniCompanyKnowledgeBot.Models;

namespace MiniCompanyKnowledgeBot.Interfaces
{
    public interface IConversationStore
    {
        void AddMessage(string sessionId, Message message);

        IReadOnlyList<Message> GetMessages(string sessionId);
    }
}
