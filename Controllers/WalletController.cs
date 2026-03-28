using Application.Dtos.Action;
using Application.Dtos.Category;
using Application.Dtos.Wallet;
using Application.IService;
using Application.Serializer;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WalletController : ControllerBase
    {

        private readonly IWalletService _walletService;
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;


        public WalletController(IJsonFieldsSerializer jsonFieldsSerializer, IWalletService walletService)
        {
            _jsonFieldsSerializer = jsonFieldsSerializer;
            _walletService = walletService;



        }

        [HttpPatch]
        [ProducesResponseType(typeof(ApiResponse<WalletDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ActivateWallet(int memberId)
        {
            var result = await _walletService.ActivateWalletAsync(memberId);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }

        [HttpPatch]
        [ProducesResponseType(typeof(ApiResponse<WalletDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeactivateWallet(int memberId)
        {
            var result = await _walletService.DeactivateWalletAsync(memberId);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<WalletDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> OpenCreateWallet(CreateWalletDto createWalletDto)
        {
            var result = await _walletService.CreateWallet(createWalletDto);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }

        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteWallet(int walletId)
        {
            var result = await _walletService.DeleteWallet(walletId);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));


        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<WalletDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActiveWallets()
        {
            var result = await _walletService.GetActiveWallets();
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<WalletDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWalletById(int walletId)
        {
            var result = await _walletService.GetWalletById(walletId);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<WalletDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWalletByMemberID(int memberId)
        {
            var result = await _walletService.GetWalletByMemberIdAsync(memberId);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<WalletDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDeavtivateWallets()
        {
            var result = await _walletService.GetDeavtivateWallets();
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> HasSufficientBalanceAsync(int memberId, decimal amount)
        {
            var result = await _walletService.HasSufficientBalanceAsync(memberId, amount);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }





    }
}
