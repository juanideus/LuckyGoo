using Microsoft.EntityFrameworkCore;
using LUCKYGOO.Src.Model;
namespace LUCKYGOO.Src.Db

{
    /// <summary>
    /// ContextDb sirve como un ORM para interactuar con la base de datos, permitiendo realizar operaciones CRUD y consultas de manera eficiente y estructurada.
    /// </summary>
    /// <param name="options"></param>
    public class ContextDb(DbContextOptions<DbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Rol> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //CONSTRUIMOS AL MODELO USUARIO
            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(u => u.Id);
                e.HasOne(u => u.Rol)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RolId);
            });
        }
    }


}