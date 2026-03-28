using Application.Dtos.Action;
using Application.Dtos.Book;
using Application.Dtos.BookCopy;
using Application.Dtos.BorrowingRecord;
using Application.Dtos.Statistics;
using Application.IService;
using Application.Serializer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {

        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;
        private readonly IBookCopyService _bookCopyService;
        private readonly IBorrowingRecordService _borrowingRecordService;
        private readonly IBookService _bookService;

        public ReportsController(IBorrowingRecordService borrowingRecordService ,
            IBookService bookService ,IBookCopyService bookCopyService ,
            IJsonFieldsSerializer jsonFieldsSerializer
            
            )
        {
            _bookCopyService= bookCopyService;
            _bookService= bookService;
            _jsonFieldsSerializer=jsonFieldsSerializer;
            _borrowingRecordService= borrowingRecordService;

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
        [ProducesResponseType(typeof(ApiResponse<BookCopyStutsStatistics>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStatisticsStuts(int bookId)
        {

            var result = await _bookCopyService.GetStatisticsStuts(bookId);

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


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BookCopyDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBookCopiesThatAddedAfterDate(DateTime date)
        {
            var result = await _bookCopyService.GetBookCopiesThatAddedAfterDate(date);


            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BorrowingRecordDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBorrowingRecordsByUserId(int userid)
        {

            var result = await _borrowingRecordService.GetBorrowingRecordsByUserId(userid);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BorrowingRecordDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBorrowingRecordsByDateRange(DateTime start, DateTime end)
        {
            var result = await _borrowingRecordService.GetBorrowingRecordsByDateRange(start, end);
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


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BookDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBooksCountsByCategoryNameAsync(string categoryname)
        {
            var result = await _bookService.GetBookCountForCategory(categoryname);


            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<MostBorrowedBookDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMostBorrowedBooks(int topN , int fromdays)
        {
            var result = await _borrowingRecordService.GetMostBorrowedBooksAsyncLastMounth(topN , fromdays);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }



        }



}
