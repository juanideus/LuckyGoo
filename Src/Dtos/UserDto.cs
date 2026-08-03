using System.ComponentModel.DataAnnotations;
using LUCKYGOO.Src.Model;
namespace LUCKYGOO.Src.Dtos.User
{

    public class UserLoginDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
    public class LoginDto
    {
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [Required(ErrorMessage = "Debe ingresar su correo electrónico para iniciar sesión")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Debe ingresar su contraseña para iniciar sesión")]
        public required string Password { get; set; }
    }
    public class RegisterDto
    {
        [Required(ErrorMessage = "Debe ingresar el campo nombre del sorteador")]
        public required string Name { get; set; }

        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [Required(ErrorMessage = "Debe ingresar el correo electrónico del sorteador")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Debe ingresar la edad del sorteador")]
        [Range(18, 65, ErrorMessage = "La edad del sorteador no puede ser inferior a 18 y mayor a 65")]
        //validamos que sea int
        [RegularExpression(@"^\d+$", ErrorMessage = "La edad del sorteador debe ser numérica")]
        public required int Age { get; set; }

    }
    public class RegisterResponseDto
    {
        //TEMPORAL HASTA QUE CONFIGUREMOS EL SERVICIO DE CORREO PARA ENVIAR EL MENSAJE DE CONFIRMACION
        public required string Password { get; set; }
        public required string Message { get; set; }
    }
    public class GetUserDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required int Age { get; set; }
        public required bool IsDeleted { get; set; }
        
    }
}