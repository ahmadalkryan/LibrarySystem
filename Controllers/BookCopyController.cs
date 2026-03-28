using Application.Dtos.Action;
using Application.Dtos.Book;
using Application.Dtos.BookCopy;
using Application.Dtos.Statistics;
using Application.IService;
using Application.Serializer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookCopyController : ControllerBase
    {
        private readonly IBookCopyService _bookCopyService;
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;


        public BookCopyController(IJsonFieldsSerializer jsonFieldsSerializer ,IBookCopyService bookCopyService)
        {
                
            _jsonFieldsSerializer = jsonFieldsSerializer;
            _bookCopyService = bookCopyService;
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BookCopyDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBookCopy()
        {
            var result = await _bookCopyService.GetAllBookCopiesAsync();

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                   new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }


        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<BookCopyDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateBookCopy(CreateBookCopyDto createBookCopyDto)
        {
            var result = await _bookCopyService.CreateBookCopyAsync(createBookCopyDto);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                   new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<BookCopyDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBookCopyById(int id)
        {
            var result =await _bookCopyService.GetBookCopyByIdAsync(id);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                   new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<BookCopyDto>), StatusCodes.Status200OK)]

        public async Task<IActionResult> UpdateBookCopy(UpdateBookCopyDto updateBookCopyDto)
        {
            var result = await _bookCopyService.UpdateBookCopyAsync(updateBookCopyDto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                   new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }


        [HttpPatch]
        [ProducesResponseType(typeof(ApiResponse<BookCopyDto>), StatusCodes.Status200OK)]

        public async Task<IActionResult> SetAvailable(int bookcopy)
        {
            var result = await _bookCopyService.SetAvailable(bookcopy);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                   new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteBookCopy(int id)
        {

            var result = await _bookCopyService.DeleteBookCopyAsync(id);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                   new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<BookCopyDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBookCopyByBookId(int bookId)
        {
            var result = await _bookCopyService.GetBookCopiesByBookIdAsync(bookId);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                   new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BookCopyDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBookCopyByStatus(string status)
        {
            var result = await _bookCopyService.GetBookCopyByStatusAsync(status);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                   new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }


        [HttpGet]

        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BookCopyDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBookCopyByUserID(int userId)
        {
            var result = await _bookCopyService.GetBookCopyByUserId(userId);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                   new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }



        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BookCopyDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBookCopyByTiltle(string tiltle)
        {

            var result = await _bookCopyService.GetBookCopiesByTitle(tiltle);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                   new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BookCopyDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBookCopiesThatAddedAfterDate(DateTime date)
        {
            var result = await _bookCopyService.GetBookCopiesThatAddedAfterDate(date);


            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BookCopyDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAvilableCopyForBook(int bookId)
        {
            var result = await _bookCopyService.GetAvilableCopyForBook(bookId);



            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }




        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BookCopyDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBookCopyByCategoryName(string categoryname)
        {
            var result = await _bookCopyService.GetBookCopyByCategoryName(categoryname);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                   new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBookCopiesCountForStutus(string stutus)
        {
            var result = await _bookCopyService.GetBookCopiesCountForStatusAsync(stutus);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                   new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBookCopiesCountForBook(int id)
        {
            var result = await _bookCopyService.GetBookCopiesCountByBookId(id);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                   new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTotalBookCopiesCount()
        {

            var result = await _bookCopyService.GetTotalBookCopiesCountAsync();

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));


        }

       

       






    }
}
