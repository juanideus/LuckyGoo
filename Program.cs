using LUCKYGOO.Src.Db;
using Microsoft.EntityFrameworkCore;
using LUCKYGOO.Src.Db.Seeder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Src.Middleware;
using LUCKYGOO.Src.Services;
using LUCKYGOO.Src.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


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
//services
builder.Services.AddScoped<IAuthServices,AuthServices>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Configuración de los parámetros de validación del token JWT.
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            // Validación del emisor del token JWT.
            ValidateIssuer = true,
            // Validación de la audiencia del token JWT.
            ValidateAudience = true,
            // Validación del tiempo de vida del token JWT.
            ValidateLifetime = true,
            // Validación de la clave de firma del token JWT.
            ValidateIssuerSigningKey = true,
            // Configuración del emisor válido del token JWT, obtenido desde appsettings.json.
            ValidIssuer = builder.Configuration["Jwt:issuer"],
            // Configuración de la audiencia válida del token JWT, obtenida desde appsettings.json.
            ValidAudience = builder.Configuration["Jwt:audience"],
            // Configuración de la clave de firma del token JWT, utilizando una clave simétrica obtenida desde appsettings.json.
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]!))
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = ctx =>
            {
                ctx.Token = ctx.Request.Cookies["jwt"];
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                // Evitar que se añada el encabezado WWW-Authenticate en la respuesta
                context.HandleResponse();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                var result = System.Text.Json.JsonSerializer.Serialize(new { error = "No autorizado" });
                return context.Response.WriteAsync(result);
            }
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseExceptionHandler();
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