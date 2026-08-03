

using LUCKYGOO.Src.Model;
using LUCKYGOO.Src.Db;
using LUCKYGOO.Src.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using LUCKYGOO.Src.Dtos.User;
using LUCKYGOO.Src.Exceptions;
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
                    Age = u.Age,
                    IsDeleted = u.IsDeleted
                })
                .ToListAsync();
            return users;
        }
        public async Task<string> ChangeUserStatus(int userId)
        {
            //verificamos si existe el usuario con el id proporcionado
            var user =await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new NotFoundException("El usuario con el id proporcionado no existe");
            }
            //cambiamos el estado del usuario
            user.IsDeleted = !user.IsDeleted;
            await _context.SaveChangesAsync();
            return "El estado del usuario ha sido cambiado correctamente.";
        }
    }
}