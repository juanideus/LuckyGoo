using Microsoft.EntityFrameworkCore;
using LUCKYGOO.Src.Model;
namespace LUCKYGOO.Src.Db

{
    /// <summary>
    /// ContextDb sirve como un ORM para interactuar con la base de datos, permitiendo realizar operaciones CRUD y consultas de manera eficiente y estructurada.
    /// </summary>
    /// <param name="options"></param>
    public class ContextDb(DbContextOptions<ContextDb> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Raffle> Raffles { get; set; }
        public DbSet<RaffleNumbers> RaffleNumbers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketNumbers> TicketNumbers { get; set; }
        public DbSet<Payment> Payments { get; set; }

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
            //CONSTRUIMOS AL MODELO RAFFLE
            modelBuilder.Entity<Raffle>(e =>
            {
                // Definimos la clave primaria para la entidad Raffle
                e.HasKey(r => r.Id);
                // Configuramos la relación uno a muchos entre Raffle y User
                e.HasOne(r => r.CreatedBy)
                .WithMany(u => u.Raffles)
                .HasForeignKey(r => r.UserId);

            });
            modelBuilder.Entity<RaffleNumbers>(e =>
            {
                e.HasOne(rn => rn.Raffle)
                .WithMany(r => r.Numbers)
                .HasForeignKey(rn => rn.RaffleId)
                .OnDelete(DeleteBehavior.Cascade); // Configura la eliminación en cascada para los números de sorteo asociados a un sorteo eliminado
            });
            modelBuilder.Entity<RaffleNumbers>()
                .HasIndex(rn => new
                {
                    rn.RaffleId,
                    rn.Number,
                    rn.Type
                })
            .IsUnique();
            modelBuilder.Entity<Ticket>(e =>
            {
              e.HasKey(t => t.Id);
              e.HasOne(t => t.Payment).WithOne(p => p.Ticket).HasForeignKey<Payment>(p => p.TicketId);
            });
            modelBuilder.Entity<Payment>(e =>
            {
                e.HasKey(p => p.Id);
                
                // Relación uno a uno con Ticket
                e.HasOne(p => p.Ticket)
                    .WithOne(t => t.Payment)
                    .HasForeignKey<Payment>(p => p.TicketId)
                    .OnDelete(DeleteBehavior.Restrict);  // No permitir eliminar ticket si existe pago
                
                // Índice único para evitar pagos duplicados por ticket
                e.HasIndex(p => p.TicketId).IsUnique();
                
                // Índices para queries frecuentes
                e.HasIndex(p => p.PaymentStatus);
                e.HasIndex(p => p.CreatedAt);
                e.HasIndex(p => p.TransactionId);
                
                // Configuración de precisión para moneda
                e.Property(p => p.Amount)
                    .HasPrecision(18, 2);
            });
        }
    }


}