using LUCKYGOO.Src.Model;
using BCrypt.Net;
namespace LUCKYGOO.Src.Db.Seeder
{
    public class Seeder(ContextDb contextDb)
    {
        private readonly ContextDb _context = contextDb;


        public async Task Seed()
        {
            if (!_context.Roles.Any())
            {
                var adminRole = new Rol { Name = "Admin" };
                var customer = new Rol { Name = "Customer" };
                var sorter = new Rol { Name = "Sorter" };

                await _context.Roles.AddRangeAsync(adminRole, customer, sorter);
                await _context.SaveChangesAsync();
            }
            if (!_context.Users.Any())
            {
                var admin = new User
                {
                    Name = "Antonio Barraza Guzmán",
                    Email = "antonio.barraza.guzman@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Luckygo23"),
                    IsDeleted = false,
                    DeletedAt = null,
                    RolId = _context.Roles.First(r => r.Name == "Admin").Id
                };
                await _context.Users.AddRangeAsync(admin);
                await _context.SaveChangesAsync();
            }
        }
    }


}