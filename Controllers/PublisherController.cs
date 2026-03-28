using Application.Dtos.Action;
using Application.Dtos.BorrowingRecord;
using Application.Dtos.Publisher;
using Application.IService;
using Application.Serializer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;

        public PublisherController(IPublisherService publisherService, IJsonFieldsSerializer jsonFieldsSerializer)
        {
            _publisherService = publisherService;
            _jsonFieldsSerializer = jsonFieldsSerializer;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<PublisherDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPublshers()
        {
            var result = await _publisherService.GetAllPublishers();

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
  new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<PublisherDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreatePublisher(CreatePublisherDto createPublisherDto)
        {
            var result = await _publisherService.CreatePublisher(createPublisherDto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
               new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }



        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var result = await _publisherService.DeletePublisher(id);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
  new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<PublisherDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePublisher(UpdatePublisherDto updatePublisherDto)
        {
            var result = await _publisherService.UpdatePublisher(updatePublisherDto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
  new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PublisherDto>), StatusCodes.Status200OK)]
        
            public async Task<IActionResult> GetPublisherById(int id)
            {
                var result = await _publisherService.GetPublisherById(id);
                return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }
    }
}
