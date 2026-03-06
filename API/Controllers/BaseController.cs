using API.MiddleWare;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {

        //protected IActionResult Success<T>(T data ,string message = "Done")
        //{
        //    return Ok(new ApiResponse<T>(data, message));
        //}

        //protected IActionResult Success(string message = "Done")
        //{
        //    return Ok(new ApiResponse(message, 200));
        //}

        //protected IActionResult Created<T>(string actionName, object routeValues, T data,
        //  string message = " Done")
        //{
        //    return CreatedAtAction(actionName, routeValues, new ApiResponse<T>(data, message));
        //}

        //// ❌ فشل مع رسالة
        //protected IActionResult Failure(string message, int statusCode = 400)
        //{
        //    return StatusCode(statusCode, new ApiResponse(message, statusCode));
        //}

        //// ❌ فشل مع أخطاء متعددة
        //protected IActionResult Failure(string message, List<string> errors, int statusCode = 400)
        //{
        //    return StatusCode(statusCode, new ApiResponse(message, statusCode, errors));
        //}

    }
}
