using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniCompanyKnowledgeBot.Interfaces;
using MiniCompanyKnowledgeBot.Models;

namespace MiniCompanyKnowledgeBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnowledgeController : ControllerBase
    {
        private readonly IKnowledgeService _knowledgeService;
        public KnowledgeController(IKnowledgeService knowledgeService)
        {
            _knowledgeService = knowledgeService;
        }
        [HttpPost("/ask")]
        public IActionResult AskQuestion(AskRequest askRequest)
        {
            var result = _knowledgeService.Ask(askRequest);
            
            return Ok(result);
        }
    }
}
