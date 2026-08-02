using Common;
namespace LUCKYGOO.Src.Model
{

    public class User : IisDeleted
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string Password { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int RolId { get; set; }
        public Rol Rol { get; set; } = null!;
    }

}