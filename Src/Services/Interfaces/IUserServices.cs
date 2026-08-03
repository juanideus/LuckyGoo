using LUCKYGOO.Src.Dtos.User;
using LUCKYGOO.Src.Model;
namespace LUCKYGOO.Src.Services.Interfaces
{
    public interface IUserServices
    {
        public Task<List<GetUserDto>> GetUsers();
    }
}