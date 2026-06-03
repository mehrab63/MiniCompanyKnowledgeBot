using MiniCompanyKnowledgeBot.Models;
using MiniCompanyKnowledgeBot.Models.Dtos;

namespace MiniCompanyKnowledgeBot.Interfaces
{
    public interface IKnowledgeService
    {
        ResultDto<AskResponse> Ask(string question);
    }
}
