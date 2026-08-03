using LUCKYGOO.Src.Dtos.User;
namespace LUCKYGOO.Src.Services.Interfaces
{
    public interface IAuthServices
    {
        Task<string> Login(LoginDto loginDto);
        Task<RegisterResponseDto> Register(RegisterDto registerDto);
    }
    
}