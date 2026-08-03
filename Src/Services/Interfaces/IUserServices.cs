using LUCKYGOO.Src.Dtos.User;
namespace LUCKYGOO.Src.Services.Interfaces
{
    public interface IUserServices
    {
        public Task<List<GetUserDto>> GetUsers();
        public Task<string> ChangeUserStatus(int userId);

    }
}