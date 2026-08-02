namespace LUCKYGOO.Src.Model
{

    public class Rol
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>(); // Relación uno a muchos con la entidad User
    }
}