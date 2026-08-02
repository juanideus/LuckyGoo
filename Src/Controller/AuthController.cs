using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using LUCKYGOO.Src.Services.Interfaces;


namespace LUCKYGOO.Src.Controller
{   [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthServices authServices) : ControllerBase
    {

        private readonly IAuthServices _authServices = authServices;

        public async Task<IActionResult> Login()
        {   
            return Ok(new { message = "Login exitoso" });
        }
    }

}