

using LUCKYGOO.Src.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LUCKYGOO.Src.Dtos.User;
using ApiResponse;
[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserServices userServices) : ControllerBase
{
    private readonly IUserServices _userServices = userServices;
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userServices.GetUsers();
        
        return Ok(new ApiResponse<List<GetUserDto>>
        {
            Status = StatusCodes.Status200OK,
            Message = "Usuarios obtenidos correctamente",
            Data = users
        });
    }
    [HttpPatch("Status")]
    public async Task<IActionResult> ChangeUserStatus([FromQuery]int userId)
    {
        var result = await _userServices.ChangeUserStatus(userId);

        return Ok(new ApiResponse<string>
        {
            Status = StatusCodes.Status200OK,
            Message = result,
            Data = null
        });
    }
}