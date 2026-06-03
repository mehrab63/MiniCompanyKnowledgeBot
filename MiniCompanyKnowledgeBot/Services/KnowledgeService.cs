using MiniCompanyKnowledgeBot.Interfaces;
using MiniCompanyKnowledgeBot.Models;
using MiniCompanyKnowledgeBot.Models.Dtos; 
namespace MiniCompanyKnowledgeBot.Services
{
    public class KnowledgeService:IKnowledgeService
    {
        private readonly string _docsPath;

        public KnowledgeService(IWebHostEnvironment env)
        {
            _docsPath = Path.Combine(env.ContentRootPath, "docs");
        }

        public ResultDto<AskResponse> Ask(string question)
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
                    return new ResultDto<AskResponse>(true)
                    {
                        Data = new AskResponse
                        {
                            Answer = content,
                            Source = Path.GetFileName(file)
                        },
                        Message = ["اطلاعاتی پیدا نشد."]
                    }; 
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
                return new ResultDto<AskResponse>(false)
                {
                    Data = new AskResponse
                    {
                        Answer = bestMatch,
                        Source = source
                    },
                    Message = ["اطلاعاتی پیدا نشد."]
                };
            }

            return new ResultDto<AskResponse>(true)
            {
                Data = new AskResponse
                {
                    Answer = bestMatch,
                    Source = source
                }
            };
        }

        private int Score(string content, string question)
        {
            var words = question.Split(' ',
                StringSplitOptions.RemoveEmptyEntries);

            return words.Count(content.Contains);
        }
    }
}
