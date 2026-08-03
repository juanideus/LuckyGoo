using LUCKYGOO.Src.Db;
using LUCKYGOO.Src.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using LUCKYGOO.Src.Dtos.User;
using LUCKYGOO.Src.Exceptions;
using LUCKYGOO.Src.Util;
using LUCKYGOO.Src.Model;
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
                throw new BadRequestException("usuario no registrado o contraseña incorrecta");
            }
            //verificamos si el usuario esta eliminado
            if (user.IsDeleted)
            {
                throw new BadRequestException("El usuario esta inactivo, contacte con el administrador");
            }

            var token = GenerateToken.getToken(user, _configuration);

            return token;
        }
        public async Task<RegisterResponseDto> Register(RegisterDto registerDto)
        {
            //verificamos si el usuario ya existe por correo electrónico
            var user = await _contextDb.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == registerDto.Email.ToLower());
            if (user != null)
            {
                throw new BadRequestException("El correo electrónico ingresado ya existe en el sistema");
            }
            //creamos el usuario
            var password = GeneratePassword.GenerateRandomPassword();
            var newUser = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Age = registerDto.Age,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                RolId = _contextDb.Roles.First(r => r.Name == "Sorter").Id,
                IsDeleted = false
            };
            //guardamos el usuario en la base de datos
            await _contextDb.Users.AddAsync(newUser);
            await _contextDb.SaveChangesAsync();
            return new RegisterResponseDto
            {
                Password = password,
                Message = "Usuario registrado exitosamente"
            };
           
        }

    }
}