using MiniCompanyKnowledgeBot.Interfaces;
using MiniCompanyKnowledgeBot.Models;
using MiniCompanyKnowledgeBot.Models.Dtos;
namespace MiniCompanyKnowledgeBot.Services
{
    public class KnowledgeService : IKnowledgeService
    {
        private readonly IConversationStore _conversationStore;
        private readonly IDocumentStore _documentStore;

        public KnowledgeService(IConversationStore conversationStore, IDocumentStore documentStore)
        {
            _conversationStore = conversationStore;
            _documentStore = documentStore;
        }

        public ResultDto<AskResponse> Ask(AskRequest askRequest)
        {
            if (string.IsNullOrWhiteSpace(askRequest.Question))
            {
                return new ResultDto<AskResponse>(false)
                {
                    Message = ["سوال نمی‌تواند خالی باشد."]
                };
            }

            var history = _conversationStore.GetMessages(askRequest.SessionId);

            var lastQuestion = history.LastOrDefault(x => x.Role == "user");

            string effectiveQuestion = askRequest.Question.Trim().ToLower();

            if (lastQuestion != null && askRequest.Question.Length < 20)
            {
                effectiveQuestion = $"{lastQuestion.Content} {effectiveQuestion}";
            }
            var documents = _documentStore.GetAll();

            string bestMatch = string.Empty;
            string source = string.Empty;
            int bestScore = -1;

            foreach (var document in documents)
            {
                var content =
                    document.Content.ToLower();
                if (content.Contains(effectiveQuestion))
                {
                    var response = new AskResponse
                    {
                        Answer = document.Content,
                        Source = document.FileName
                    };

                    SaveConversation(
                        askRequest.SessionId,
                        askRequest.Question,
                        response.Answer);

                    return new ResultDto<AskResponse>(true)
                    {
                        Data = response
                    };
                }

                var score = Score(content, effectiveQuestion);

                if (score > bestScore)
                {
                    bestScore = score;
                    bestMatch = document.Content;
                    source = document.FileName;
                }
            }
            if (bestScore <= 0)
            {
                SaveConversation(askRequest.SessionId, askRequest.Question, "اطلاعاتی پیدا نشد.");

                return new ResultDto<AskResponse>(false)
                {
                    Message = ["اطلاعاتی پیدا نشد."]
                };
            }
            var result = new AskResponse
            {
                Answer = bestMatch,
                Source = source
            };

            SaveConversation(
                askRequest.SessionId,
                askRequest.Question,
                result.Answer);

            return new ResultDto<AskResponse>(true)
            {
                Data = result
            };
        }

        private void SaveConversation(string sessionId, string userQuestion, string assistantAnswer)
        {
            _conversationStore.AddMessage(
                sessionId,
                new Message
                {
                    Role = "user",
                    Content = userQuestion
                });

            _conversationStore.AddMessage(
                sessionId,
                new Message
                {
                    Role = "assistant",
                    Content = assistantAnswer
                });
        }

        private int Score(string content, string question)
        {
            var words = question.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return words.Count(content.Contains);
        }

    }
}
