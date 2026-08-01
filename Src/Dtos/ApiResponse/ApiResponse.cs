namespace ApiResponse
{
    /// <summary>
    /// Esta clase Permite crear una respuesta estandarizada para las respuestas de la API.
    public class ApiResponse<T>
    {
        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static ApiResponse<T> success(T? data, string message = "Ok",int status = 200) => new()
        {
            Status = status,
            Message = message,
            Data = data
        };
        public static ApiResponse<T> error(string message, int status = 400) => new()
        {
            Status = status,
            Message = message,
            Data = default
        };
    }
}