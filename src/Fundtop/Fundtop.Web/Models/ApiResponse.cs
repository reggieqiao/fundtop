namespace Fundtop.Web.Models
{
    public class ApiResponse<T>
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public ApiResponse(int code, string message, T data = default)
        {
            Code = code;
            Message = message;
            Data = data;
        }

        public static ApiResponse<T> Success()
        {
            return new ApiResponse<T>(200, "success");
        }

        public static ApiResponse<T> Success(T data = default)
        {
            return new ApiResponse<T>(200, "success", data);
        }

        public static ApiResponse<T> Success(string message, T data = default)
        {
            return new ApiResponse<T>(200, message, data);
        }

        public static ApiResponse<T> Fail()
        {
            return new ApiResponse<T>(400, "fail");
        }

        public static ApiResponse<T> Fail(int code)
        {
            return new ApiResponse<T>(code, "fail");
        }

        public static ApiResponse<T> Fail(int code, string message)
        {
            return new ApiResponse<T>(code, message);
        }
    }
}
