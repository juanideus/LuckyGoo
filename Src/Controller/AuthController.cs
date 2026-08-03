using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using LUCKYGOO.Src.Services.Interfaces;

using LUCKYGOO.Src.Dtos.User;
using ApiResponse;
namespace LUCKYGOO.Src.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthServices authServices,IHostEnvironment env) : ControllerBase
    {

        private readonly IAuthServices _authServices = authServices;
        private readonly IHostEnvironment _env = env;
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            //vamamos al servicio para que nos haga el login y nos devuelva el token
            var result = await _authServices.Login(loginDto);

            //enviamos el token por la cokie
            AppendToken(result);
            
            return Ok(new ApiResponse<string>
            {
                Status = StatusCodes.Status200OK,
                Message = "Login exitoso",
            });
        }
        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _authServices.Register(registerDto);
            return Ok(new ApiResponse<string>
            {
                Status = StatusCodes.Status201Created,
                Message = result.Message,
                Data = result.Password
            });
        }
        private void AppendToken(string token)
        {
            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = !_env.IsDevelopment(),
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7)
            });
        }

    }
}