using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniCompanyKnowledgeBot.Interfaces;
using MiniCompanyKnowledgeBot.Models;

namespace MiniCompanyKnowledgeBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IKnowledgeService _knowledgeService;
        public QuestionController(IKnowledgeService knowledgeService)
        {
            _knowledgeService = knowledgeService;
        }
        [HttpPost("/ask")]
        public IActionResult AskQuestion(string question)
        {
            var result = _knowledgeService.Ask(question);
            
            return Ok(result);
        }
    }
}
