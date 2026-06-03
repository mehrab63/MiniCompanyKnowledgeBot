using MiniCompanyKnowledgeBot.Interfaces;
using MiniCompanyKnowledgeBot.Models;

namespace MiniCompanyKnowledgeBot.Services
{
    public class InMemoryConversationStore : IConversationStore
    { 
        private readonly Dictionary<string, List<Message>> _store = new();
        public void AddMessage(string sessionId, Message message)
        {
            if (!_store.ContainsKey(sessionId))
            {
                _store[sessionId] = [];
            }

            _store[sessionId].Add(message);
        }

        public IReadOnlyList<Message> GetMessages(string sessionId)
        {
            return _store.TryGetValue(
                sessionId,
                out var messages)
                ? messages
                : [];
        }


    }
}
