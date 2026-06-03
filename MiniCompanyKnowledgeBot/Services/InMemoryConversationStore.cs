using MiniCompanyKnowledgeBot.Interfaces;
using MiniCompanyKnowledgeBot.Models;

namespace MiniCompanyKnowledgeBot.Services
{
    public class InMemoryConversationStore : IConversationStore
    {
        private readonly Dictionary<string, List<Message>>
            _conversations = new();

        public Task AddMessageAsync(
            string sessionId,
            string role,
            string content)
        {
            if (!_conversations.ContainsKey(sessionId))
            {
                _conversations[sessionId] = new List<Message>();
            }

            _conversations[sessionId].Add(new Message
            {
                Role = role,
                Content = content
            });

            return Task.CompletedTask;
        }

        public Task<List<Message>> GetHistoryAsync(
            string sessionId)
        {
            if (!_conversations.ContainsKey(sessionId))
            {
                return Task.FromResult(new List<Message>());
            }

            return Task.FromResult(_conversations[sessionId]);
        }
    }
}
