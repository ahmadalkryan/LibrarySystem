using Application.Dtos.Action;
using Application.IService;
using Application.Serializer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LibraryChatBotController : ControllerBase
    {
        private readonly IAIService _aiService;
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;

        public LibraryChatBotController(IAIService  aIService , IJsonFieldsSerializer jsonFieldsSerializer)
        {
            
            _aiService = aIService;
            _jsonFieldsSerializer = jsonFieldsSerializer;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResponse(string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt))
            {
                return BadRequest("Prompt cannot be empty.");
            }
            var result = await _aiService.GetAIresponseAsync(prompt);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                 new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        } 
    }
}
