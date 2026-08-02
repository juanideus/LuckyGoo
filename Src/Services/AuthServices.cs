using LUCKYGOO.Src.Db;
using LUCKYGOO.Src.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using LUCKYGOO.Src.Dtos.User;
using LUCKYGOO.Src.Exceptions;
using LUCKYGOO.Src.Util;
namespace LUCKYGOO.Src.Services
{
    public class AuthServices(IConfiguration configuration, ContextDb contextDb) : IAuthServices
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ContextDb _contextDb = contextDb;

        public async Task<string> Login(LoginDto loginDto)
        {
            //primero buscamos al usuario en la base de datos por su email
            var user = await _contextDb.Users.Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.Email.ToLower() == loginDto.Email.ToLower());
            //si no encontramos al usuario lanzamos un NotFoundException
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                throw new badRequestException("Usuario o contraseña incorrectos");
            }
            //verificamos si el usuario esta eliminado
            if (user.IsDeleted)
            {
                throw new badRequestException("El usuario esta inactivo, contacte con el administrador");
            }
            
            var  token = GenerateToken.getToken(user, _configuration);

            return token;
            
        }

    }
}