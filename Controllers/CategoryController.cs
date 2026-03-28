using Application.Dtos.Action;
using Application.Dtos.Category;
using Application.IService;
using Application.Serializer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;


        public CategoryController(IJsonFieldsSerializer jsonFieldsSerializer ,ICategoryService categoryService )
        {
            _categoryService = categoryService;
            _jsonFieldsSerializer = jsonFieldsSerializer;

        }

        [HttpGet]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]

        [ProducesResponseType(typeof(ApiResponse<IEnumerable<CategoryDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _categoryService.GetAllCategories();

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                         new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetCategoryByID(int id)
        {
            var result = await _categoryService.GetCategoryById(id);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                      new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }


        [HttpPost]

        [ProducesResponseType(typeof(ApiResponse<CategoryDto>), StatusCodes.Status200OK)]

        public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
        {
            var result =await _categoryService.CreateCategory(dto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                  new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }



        [HttpPut]

        [ProducesResponseType(typeof(ApiResponse<CreateCategoryDto>), StatusCodes.Status200OK)]

        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {


         var result =await _categoryService.UpdateCategory(updateCategoryDto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                     new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategory(id);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
             new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));  

        }






    }
}
