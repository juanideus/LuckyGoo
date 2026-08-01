namespace ApiResponse
{
    public class ApiResponse<T>
    {
        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static ApiResponse<T> Success(T? data, string message = "Ok", int status = 200) => new()
        {
            Status = status,
            Message = message,
            Data = data
        };
        public static ApiResponse<T> Fail(string message, int status = 400) => new()
        {
            Status = status,
            Message = message,
            Data = default
        };
    }
}