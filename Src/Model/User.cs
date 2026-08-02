using Common;
namespace User
{

    public class User : IisDeleted
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public string Password { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

}