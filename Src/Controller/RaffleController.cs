using LUCKYGOO.Src.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LUCKYGOO.Src.Dtos;
using Microsoft.AspNetCore.Authorization;
using ApiResponse;
using System.Security.Claims;
namespace LUCKYGOO.Src.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class RaffleController(IRaffleServices raffleServices) : ControllerBase
    {
        private readonly IRaffleServices _raffleServices = raffleServices;

        [HttpPost("register")]
        [Authorize(Roles = "Admin,Sorter")]
        public async Task<IActionResult> RegisterRaffle([FromBody] RaffleDto raffleDto)
        {
            //OBTENEMOS EL ID DEL USUARIO LOGUEADO DEL TOKEN
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(rawUserId, out int userId))
            {
                return Unauthorized(new ApiResponse<string>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "No se pudo validar la sesión del usuario."
                });
            }

            var result = await _raffleServices.RegisterRaffle(raffleDto, userId);

            return StatusCode(StatusCodes.Status201Created, new ApiResponse<string>
            {
                Status = StatusCodes.Status201Created,
                Message = result
            });
        }
    }
}