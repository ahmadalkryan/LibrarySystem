using Application.Dtos.Action;
using Application.Dtos.User;
using Application.IService;
using Application.Serializer;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly IUserService _userservice;
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;


        public AuthenticationController(IUserService userService, IJsonFieldsSerializer jsonFieldsSerializer)
        {
            _jsonFieldsSerializer = jsonFieldsSerializer;
            _userservice = userService;

        }



        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await _userservice.Register(registerDto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
             new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }






        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {

            var response =  await _userservice.Login(loginDto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
           new ApiResponse(true, "Login success", StatusCodes.Status200OK, response), string.Empty));
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrentUser()
        {
            var result = await _userservice.GetCurrentUser();
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
             new ApiResponse(true, "user retrived successfully", StatusCodes.Status200OK, result), string.Empty));



        }
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<UserDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userservice.GetAllUser();


            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
               new ApiResponse(true, "user returved successfully", StatusCodes.Status200OK, result), string.Empty));
           

        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateRole(UpdateUserRole updateUserRole)
        {
            var result = await _userservice.UpdateUSerRole(updateUserRole);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
              new ApiResponse(true, "user updated successfuly", StatusCodes.Status200OK, result), string.Empty));

        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUerById(int id)
        {
            var result =await _userservice.GetUser(id);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
              new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }








    }
}
