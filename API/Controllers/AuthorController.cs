using Application.Dtos.Action;
using Application.Dtos.Author;
using Application.Dtos.Publisher;
using Application.IService;
using Application.Serializer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {

        private readonly IAuthorService _authorService;
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;

        public AuthorController(IAuthorService authorService ,IJsonFieldsSerializer jsonFieldsSerializer)
        {
            _authorService = authorService;
            _jsonFieldsSerializer = jsonFieldsSerializer;

        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<AuthorDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAuthor()
        {
            var result = await _authorService.GetAllAuthor();
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse<AuthorDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await _authorService.DeleteAuthor(id);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

         [HttpPost]
            [ProducesResponseType(typeof(ApiResponse<AuthorDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAuthor(CreateAuthorDto createAuthorDto)
        {
            var result = await _authorService.CreateAuthor(createAuthorDto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<AuthorDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAuthor(UpdateAuthorDto updateAuthorDto)
        {
            var result = await _authorService.UpdateAuthor(updateAuthorDto);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<AuthorDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var result = await _authorService.GetAuthorById(id);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }


        }
}
