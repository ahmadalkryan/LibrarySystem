using Application.Dtos.Action;
using Application.Dtos.Book;
using Application.Dtos.BorrowingRecord;
using Application.IService;
using Application.Serializer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;

        public BookController(IJsonFieldsSerializer jsonFieldsSerializer ,IBookService bookService)
        {
            _jsonFieldsSerializer = jsonFieldsSerializer;
            _bookService = bookService;

        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BookDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _bookService.GetAllBooksAsync();


            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpPost]

        [ProducesResponseType(typeof(ApiResponse<BookDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult > CreateBook(CreateBookDto createBookDto)
        {
            var result = await _bookService.AddBookAsync(createBookDto);


            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }



        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<BookDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult >GetBookById(int id)
        {
            var result= await _bookService.GetBookByIdAsync(id);


            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<BookDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateBook(UpdateBookDto updateBookDto)
        {
            var result =await _bookService.UpdateBookAsync(updateBookDto);


            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBook(id);


            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BookDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult > GetBooksByAuthor(int authorId)
        {
            var result = await _bookService.GetBooksByAuthorIdAsync(authorId);


            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BookDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBooksByCategoryNameAsync(string categoryname)
        {
            var result = await _bookService.GetBooksByCategoryNameAsync(categoryname);


            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }
        [HttpGet ]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BookDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBooksByUserID(int userId)
        {
            var result = await _bookService.GetBooksByUserID(userId);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                   new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet ]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BookDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBooksByLanguage(string language)
        {
            var result = await _bookService.GetBooksByLanguageAsync(language);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                 new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));


        }

        [HttpGet ]
        [ProducesResponseType(typeof(ApiResponse<BookDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBooksByIsbn(string isbn)
        {
            var result = await _bookService.GetBooksByISBN(isbn);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                 new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BookDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBooksByPublisherId(int id)
        {
            var result = await _bookService.GetBooksByPublisherIdAsync(id);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                 new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }




        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BookDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBooksCountsByCategoryNameAsync(string categoryname)
        {
            var result = await _bookService.GetBookCountForCategory(categoryname);


            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }
    }
}
