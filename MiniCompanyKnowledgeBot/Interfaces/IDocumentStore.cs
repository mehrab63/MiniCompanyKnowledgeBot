using MiniCompanyKnowledgeBot.Models;

namespace MiniCompanyKnowledgeBot.Interfaces
{
    public interface IDocumentStore
    {
        IReadOnlyList<KnowledgeDocument> GetAll();
    }
}
