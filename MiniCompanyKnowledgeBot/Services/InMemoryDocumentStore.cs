using MiniCompanyKnowledgeBot.Interfaces;
using MiniCompanyKnowledgeBot.Models;

namespace MiniCompanyKnowledgeBot.Services
{
    public class InMemoryDocumentStore : IDocumentStore
    {
        private readonly List<KnowledgeDocument> _documents;

        public InMemoryDocumentStore(IWebHostEnvironment env)
        {
            var docsPath = Path.Combine(env.ContentRootPath, "docs");

            _documents = Directory.GetFiles(docsPath, "*.txt")
                .Select(file => new KnowledgeDocument
                {
                    FileName = Path.GetFileName(file),
                    Content = File.ReadAllText(file)
                }).ToList();
        }

        public IReadOnlyList<KnowledgeDocument> GetAll()
        {
            return _documents;
        }
    }
}
