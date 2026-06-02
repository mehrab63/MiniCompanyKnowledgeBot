namespace MiniCompanyKnowledgeBot.Services
{
    public class KnowledgeService
    {
        private readonly string _docsPath;

        public KnowledgeService(IWebHostEnvironment env)
        {
            _docsPath = Path.Combine(env.ContentRootPath, "docs");
        }

        public (string answer, string source) Ask(string question)
        {
            question = question.ToLower();

            var files = Directory.GetFiles(_docsPath, "*.txt");

            string bestMatch = "";
            string source = "";

            foreach (var file in files)
            {
                var content = File.ReadAllText(file);

                if (content.ToLower().Contains(question))
                {
                    return (content, Path.GetFileName(file));
                }

                var score = Score(content.ToLower(), question);

                if (score > bestMatch.Length)
                {
                    bestMatch = content;
                    source = Path.GetFileName(file);
                }
            }

            if (string.IsNullOrWhiteSpace(bestMatch))
            {
                return ("اطلاعاتی پیدا نشد.", "");
            }

            return (bestMatch, source);
        }

        private int Score(string content, string question)
        {
            var words = question.Split(' ',
                StringSplitOptions.RemoveEmptyEntries);

            return words.Count(content.Contains);
        }
    }
}
