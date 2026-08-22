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
        [HttpGet("raffles")]
        public async Task<IActionResult> GetRaffles()
        {
            var raffles = await _raffleServices.GetRaffles();

            return Ok(new ApiResponse<List<RaffleResponseDto>>
            {
                Status = StatusCodes.Status200OK,
                Message = "Sorteos obtenidos correctamente",
                Data = raffles
            });
        }
        [HttpGet("raffle")]
        public async Task<IActionResult> GetRaffleInCourse()
        {
            var raffleInCourse = await _raffleServices.GetRaffleInCourse();

            return Ok(new ApiResponse<RaffleInCourseDto>
            {
                Status = StatusCodes.Status200OK,
                Message = "Sorteo en curso obtenido correctamente",
                Data = raffleInCourse
            });
        }
        [HttpPost("buy")]
        public async Task<IActionResult> BuyRaffle([FromBody] BuyRaffleDto buyRaffleDto)
        {
            //llamamos al servicio para comprar el sorteo
            var result = await _raffleServices.BuyRaffle(buyRaffleDto);
            return Ok(new ApiResponse<string>
            {
                Status = StatusCodes.Status200OK,
                Message = "Sorteo comprado correctamente",
                Data = result
            });
        }
    }

}