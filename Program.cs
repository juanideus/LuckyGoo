using LUCKYGOO.Src.Db;
using Microsoft.EntityFrameworkCore;
using LUCKYGOO.Src.Db.Seeder;

var builder = WebApplication.CreateBuilder(args);

// 👇 Todo el registro de servicios va ANTES de Build()
builder.Services.AddOpenApi();
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

// Si tienes CORS, también se registra el servicio antes de Build()
// builder.Services.AddCors(options => { ... }); 

var app = builder.Build();   // 👈 AHORA sí, con todo ya registrado

// A partir de aquí, solo configuras el pipeline (middlewares), no registras servicios
app.UseCors("AllowFrontend");

using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<ContextDb>();
        await new Seeder(context).Seed();
        Console.WriteLine("Base de datos inicializada correctamente.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al inicializar la base de datos: {ex.Message}");
    }
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();