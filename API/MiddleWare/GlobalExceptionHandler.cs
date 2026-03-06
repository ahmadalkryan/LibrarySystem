
using Application.Serializer;
using Domain.Entities;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;


namespace API.MiddleWare
{
    public class GlobalExceptionHandler
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;
       // private readonly IHostEnvironment _env;





        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
           
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                //if (context.Request.Path.StartsWithSegments("/swagger"))
                //{
                //    await _next(context);
                //    return;
                //}
                await _next(context);
              
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);

            }

        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            var response = new ApiResponse(
                Result: false,
                Message: ex.Message,
                Code: StatusCodes.Status500InternalServerError,
                Data: ex.Data?[ApiConsts.ExceptionKey]);

            // معالجة أنواع خاصة من الاستثناءات
            switch (ex)
            {
                case UnauthorizedAccessException:
                    response.Code = StatusCodes.Status401Unauthorized;
                    break;
                case KeyNotFoundException:
                    response.Code = StatusCodes.Status404NotFound;
                    break;

                case NullReferenceException:
                    response.Code = StatusCodes.Status404NotFound;
                    break;
                case ValidationException:
                    response.Code = StatusCodes.Status400BadRequest;
                    break;
            }

            if (ex.Data is not null)
            {
                if (ex.Data[ApiConsts.StatusCodeKey] is int statusCode)
                    response.Code = statusCode;

                if (ex.Data[ApiConsts.DataKey] is object data)
                    response.Data = data;
            }


            context.Response.StatusCode = response.Code;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));



            }
        }
    }

