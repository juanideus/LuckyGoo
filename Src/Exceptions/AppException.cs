namespace LUCKYGOO.Src.Exceptions
{

    public abstract class AppException(string message, int statusCode) : Exception(message)
    {
        public int StatusCode { get; } = statusCode;
    }
    public class NotFoundException(string message)
        : AppException(message, StatusCodes.Status404NotFound);
    
    public class badRequestException(string message)
        : AppException(message, StatusCodes.Status400BadRequest);

    public class ValidationException(string message)
        : AppException(message, StatusCodes.Status400BadRequest);

    public class UnauthorizedException(string message)
        : AppException(message, StatusCodes.Status401Unauthorized);

    public class ConflictException(string message)
        : AppException(message, StatusCodes.Status409Conflict);

    public class ForbiddenException(string message)
        : AppException(message, StatusCodes.Status403Forbidden);

}