namespace API.MiddleWare
{
    //partial class ApiResponse<T>
    //{
    //    public bool Success { get; set; }
    //    public string Message { get; set; }

    //    public T Data { get; set; }
    //    public List<string> Errors { get; set; }

    //    public int StatusCode { get; set; }

    //    // success
    //    public ApiResponse(T data , string message ="Done")
    //    {
    //        Success = true;
    //        Message = message;
    //        Data = data;
    //        StatusCode = 200;
    //        Errors = null;
    //    }
    //    //failed
    //    public ApiResponse(string message , int statusCode = 400 ,List<string > error= null )
    //    {
    //        Success =false ;
    //        Message = message;
    //        StatusCode=statusCode;
    //        Errors= error ?? new List<string> ();
    //        Data = default;
    //    }
    //    public ApiResponse(string message, string error, int statusCode = 400)
    //        : this(message, statusCode, new List<string> { error })
    //    {
    //    }



    //}

    //partial class ApiResponse :ApiResponse<object>
    //{
    //    public ApiResponse(object data, string message = "Done") : base(data, message)
    //    {
    //    }

    //    public ApiResponse(string message , int statusCode =400 , List<string > errors =null):base(message,statusCode, errors)
    //    {

            
    //    }
  //  }

}
