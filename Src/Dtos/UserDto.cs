using System.ComponentModel.DataAnnotations;
namespace LUCKYGOO.Src.Dtos.User
{

    public class UserLoginDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
    public class LoginDto
    {   [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [Required(ErrorMessage = "Debe ingresar su correo electrónico para iniciar sesión")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Debe ingresar su contraseña para iniciar sesión")]
        public required string Password { get; set; }
    }
}