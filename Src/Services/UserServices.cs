

using LUCKYGOO.Src.Model;
using LUCKYGOO.Src.Db;
using LUCKYGOO.Src.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using LUCKYGOO.Src.Dtos.User;
namespace LUCKYGOO.Src.Services
{
    public class UserServices(ContextDb contextDb) : IUserServices
    {

        private readonly ContextDb _context = contextDb;
        public async Task<List<GetUserDto>> GetUsers()
        {
            var users = await _context.Users.Where(u => u.Rol.Name == "Sorter")
                .Select(u => new GetUserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Age = u.Age
                })
                .ToListAsync();
            return users;
        }
    }
}