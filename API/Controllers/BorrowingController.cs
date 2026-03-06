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


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BorrowingRecordDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBorrowingRecords()
        {
            var result = await _borrowingRecordService.GetAllBorrowingRecords();

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
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

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteBorrowingRecord(int id)
        {
            var result = await _borrowingRecordService.DeleteBorrowingRecord(id);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<BorrowingRecordDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateBorrowingRecord(string transaction)
        {
            var result = await _borrowingRecordService.UpdateBorrowingRecord(transaction);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BorrowingRecordDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOverdueBorrowingRecords()
        {
            var result = await _borrowingRecordService.GetOverdueBorrowingRecords();
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ISBookBookAvailble(int bookcopy)
        {
            var result = await _borrowingRecordService.IsBookCopyAvailableAsync(bookcopy);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
       new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));


        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BorrowingRecordDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActiveBorrowingRecords()
        {
            var result = await _borrowingRecordService.GetActiveBorrowingRecords();
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
       new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BorrowingRecordDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBorrowingRecordsByBookCopyId(int copyId)
        {
            var result = await _borrowingRecordService.GetBorrowingRecordsByBookCopyId(copyId);

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
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BorrowingRecordDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBorrowingRecordsByMemberId(int memberID)
        {
            var result = await _borrowingRecordService.GetBorrowingRecordsByMemberId(memberID);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<BorrowingRecordDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBorrowingRecordsByTransactin(string transaction)
        {
            var result = await _borrowingRecordService.GetBorrowingRecordByTransaction(transaction);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<BorrowingRecordDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOverdueBorrowingRecordsAndNotLost()
        {
            var result = await _borrowingRecordService.GetOverdueBorrowingRecordsAndNotLost();

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }


        
        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SetLost(int copyId)
        {
            await _borrowingRecordService.SetLost(copyId);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, null), string.Empty));

        }
        }
    }
