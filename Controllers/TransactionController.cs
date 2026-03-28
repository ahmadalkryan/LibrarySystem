using Application.Dtos.Action;
using Application.Dtos.Wallet;
using Application.Dtos.WalletTransaction;
using Application.IService;
using Application.Serializer;
using Infrastructure.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IWalletTransactionService _walletTransactionService;
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;


        public TransactionController(IJsonFieldsSerializer jsonFieldsSerializer , IWalletTransactionService walletTransactionService)
        {
            _jsonFieldsSerializer = jsonFieldsSerializer;
            _walletTransactionService = walletTransactionService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<WalletTransactionDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBorrowTransaction()
        {
            var result = await _walletTransactionService.GetAllBorrowTransaction();
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<WalletTransactionDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTransaction()
        {
            var result = await _walletTransactionService.GetAllTransaction();
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }

        [HttpGet]

        [ProducesResponseType(typeof(ApiResponse<WalletTransactionDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult>GetTransactionById(int Id)
        {
            var result = await _walletTransactionService.GetTransactionByIdAsync(Id);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }

        [HttpGet]

        [ProducesResponseType(typeof(ApiResponse<WalletTransactionDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransactionByNumber(string transactionNumber)
        {
            var result = await _walletTransactionService.GetTransactionByNumberAsync(transactionNumber);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<Dictionary<DateTime ,decimal>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDailyTransactionTotalsAsync(DateTime startDate, DateTime endDate)
        {
            var result = await _walletTransactionService.GetDailyTransactionTotalsAsync(startDate, endDate);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<WalletTransactionDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTopNTransactions(int topN )
        {
            var result = await _walletTransactionService.GetTopNTransactions(topN);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<WalletTransactionDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTRansactionGreaterThan(decimal amount)
        {
            var result = await _walletTransactionService.GetTRansactionGreaterThan(amount);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<WalletTransactionDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransactionsByBorrowingIdAsync(int borrowingId)
        {
            var result = await _walletTransactionService.GetTransactionsByBorrowingIdAsync(borrowingId);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }



        [HttpGet]

        [ProducesResponseType(typeof(ApiResponse<IEnumerable<WalletTransactionDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransactionsByDateRangeAsync(DateTime startDate , DateTime endDate)
        {
            var result = await _walletTransactionService.GetTransactionsByDateRangeAsync(startDate, endDate);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }




        [HttpGet]

        [ProducesResponseType(typeof(ApiResponse<IEnumerable<WalletTransactionDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransactionsByMemberIdAsync(int memberId)
        {
            var result = await _walletTransactionService.GetTransactionsByMemberIdAsync(memberId);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }




        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<WalletTransactionDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransactionsByStatusAsync(string status)
        {
            var result = await _walletTransactionService.GetTransactionsByStatusAsync(status);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }




        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<WalletTransactionDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransactionsByTypeAsync(string transactionType)
        {
            var result = await _walletTransactionService.GetTransactionsByTypeAsync(transactionType);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }








        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<WalletTransactionDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransactionsByWalletIdAsync(int walletId)
        {
            var result = await _walletTransactionService.GetTransactionsByWalletIdAsync(walletId);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }




        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<WalletTransactionDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransactionsByUserIdAsync(int userId)
        {
            var result = await _walletTransactionService.GetTransactionsByUserIdAsync(userId);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }












        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateStatusForTransaction(int id  ,string newStatus)
        {
            var result = await _walletTransactionService.UpdateStatusForTransaction(id, newStatus);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }

















        [HttpPost]

        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ChargeWallet(ChargeDto chargeDto)
        {
            var result = await _walletTransactionService.ChargeWallet(chargeDto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }

        [HttpPost]

        [ProducesResponseType(typeof(ApiResponse<WalletTransactionDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateTransaction(CreateWalletTransactionDto createWalletTransactionDto)
        {
            var result = await _walletTransactionService.CreationTransaction(createWalletTransactionDto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }





    }
}
