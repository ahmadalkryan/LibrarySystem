using Application.Dtos.Action;
using Application.Dtos.Book;
using Application.Dtos.BorrowingRecord;
using Application.Dtos.User;
using Application.IService;
using Application.Serializer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    // دائما الراةت تاكد من الشواغر شو العنوان لل الاكشن في الكونترولر 



    // 

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly IBorrowingRecordService _borrowingRecordService;
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;

        public BorrowingController(IJsonFieldsSerializer jsonFieldsSerializer,
            IBorrowingRecordService borrowingRecordService)
        {
            _borrowingRecordService = borrowingRecordService;
            _jsonFieldsSerializer = jsonFieldsSerializer;
        }

        #region Borrowing Operatins

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BorrowingRecordDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBorrowingRecords()
        {
            var result = await _borrowingRecordService.GetAllBorrowingRecords();

            return ApiResponse(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<BorrowingRecordDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateBorrowingRecord(CreateBorrowingRecordDto createBorrowingRecordDto)
        {
            
            var result = await _borrowingRecordService.CreateBorrowing(createBorrowingRecordDto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<BorrowingRecordDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBorrowingRecordById(int id)
        {
            var result = await _borrowingRecordService.GetBorrowingRecordById(id);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BorrowingRecordDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBorrowingRecordsByUserId(int userid)
        {

            var result = await _borrowingRecordService.GetBorrowingRecordsByUserId(userid);

            return ApiResponse(result);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteBorrowingRecord(int id)
        {
            var result = await _borrowingRecordService.DeleteBorrowingRecord(id);

            return ApiResponse(result ,"Borrowing record deleted successfully");
        }





        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> MarkCopyAsLostAsync(int borrowId)
        {
            var result = await _borrowingRecordService.MarkCopyAsLostAsync(borrowId);

            return ApiResponse(result, result ? "Copy marked as lost" : "Failed to mark copy as lost");

        }



        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<BorrowingRecordDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ReturnBookAsync(int borrowId)
        {
            var result = await _borrowingRecordService.ReturnBookAsync(borrowId);

            return ApiResponse(result, result != null ? "Book returned successfully" : "Failed to return book");
        }


        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<BorrowingRecordDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> RenewBorrowingAsync(int borrowId)
        {
            var result = await _borrowingRecordService.RenewBorrowingAsync(borrowId);

            return ApiResponse(result, result != null ? "Borrowing renewed successfully" : "Failed to renew borrowing");
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BorrowingRecordDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOverdueBorrowingRecords()
        {
            var result = await _borrowingRecordService.GetOverdueBorrowingRecords();
          return ApiResponse(result);
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckBookCopyAvailability(int bookcopy)
        {
            var result = await _borrowingRecordService.IsBookCopyAvailableAsync(bookcopy);
          
            return ApiResponse(result, result ? "Book copy is available" : "Book copy is not available");

        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BorrowingRecordDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActiveBorrowingRecords()
        {
            var result = await _borrowingRecordService.GetActiveBorrowingRecords();
            return ApiResponse(result);
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BorrowingRecordDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBorrowingRecordsByBookCopyId(int copyId)
        {
            var result = await _borrowingRecordService.GetBorrowingRecordsByBookCopyId(copyId);

            return  ApiResponse(result);
        }



        

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BorrowingRecordDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBorrowingRecordsByMemberId(int memberID)
        {
            var result = await _borrowingRecordService.GetBorrowingRecordsByMemberId(memberID);

            return  ApiResponse(result);
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<BorrowingRecordDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBorrowingRecordsByTransactin(string transaction)
        {
            var result = await _borrowingRecordService.GetBorrowingRecordByTransaction(transaction);

            return ApiResponse(result);     
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BorrowingRecordDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOverdueBorrowingRecordsAndNotLost()
        {
            var result = await _borrowingRecordService.GetOverdueBorrowingRecordsAndNotLost();

            return ApiResponse(result);
        }




        #endregion

        private IActionResult ApiResponse<T>(T data, string message = "", int statusCode = StatusCodes.Status200OK)
        {
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                new ApiResponse(true, message, statusCode, data), string.Empty));
        }

    }
}
