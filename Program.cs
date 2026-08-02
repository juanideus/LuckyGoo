using LUCKYGOO.Src.Db;
using Microsoft.EntityFrameworkCore;
using LUCKYGOO.Src.Db.Seeder;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

var app = builder.Build();
app.UseCors("AllowFrontend");
builder.Services.AddControllers();

var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ContextDb>(
    options => options.UseNpgsql(
        ConnectionString,
        npgsqlOptions =>
        {
            npgsqlOptions.EnableRetryOnFailure();
        }
    )
);

using (var scope = app.Services.CreateScope())
{
    try
    {
        // Inicializamos la base de datos y ejecutamos el seeder para poblarla con datos iniciales
        var context = scope.ServiceProvider.GetRequiredService<ContextDb>();
        await new Seeder(context).Seed();
        Console.WriteLine("Base de datos inicializada correctamente.");
    }catch (Exception ex)
    {
        Console.WriteLine($"Error al inicializar la base de datos: {ex.Message}");
    }
}
app.UseAuthentication(); // añadimos la autenticacion como middleware para que se ejecute en cada request y valide el token
app.UseAuthorization();
app.MapControllers();
app.Run();