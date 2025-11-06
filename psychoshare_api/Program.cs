using Microsoft.EntityFrameworkCore;
using dao_library;
using psychoshare_api;
using dao_library.Contexts;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// Load .env.local file
Env.Load("../.env.local");

var builder = WebApplication.CreateBuilder(args);

#region Token
builder.Services.AddScoped<TokenService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Valida la firma del token usando la clave secreta
            ValidateIssuerSigningKey = true, 
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            
            // Valida el emisor (debe coincidir con "Jwt:Issuer")
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            
            // Valida la audiencia (debe coincidir con "Jwt:Audience")
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            
            // Valida que el token no haya expirado
            ValidateLifetime = true
        };
    });
#endregion

#region Conexion
// Build connection string using environment variables
var dbServer = Environment.GetEnvironmentVariable("DB_SERVER") ?? "localhost";
var dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306";
var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "psychoshare";
var dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "root";
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "";

Console.WriteLine($"DEBUG: DB_SERVER = {dbServer}");
Console.WriteLine($"DEBUG: DB_PORT = {dbPort}");
Console.WriteLine($"DEBUG: DB_NAME = {dbName}");
Console.WriteLine($"DEBUG: DB_USER = {dbUser}");
Console.WriteLine($"DEBUG: DB_PASSWORD = {(string.IsNullOrEmpty(dbPassword) ? "EMPTY" : "SET")}");

var connectionString = $"Server={dbServer};" +
                       $"Port={dbPort};" +
                       $"Database={dbName};" +
                       $"Uid={dbUser};" +
                       $"Pwd={dbPassword};";


#endregion

Console.WriteLine($"DEBUG: Connection String = {connectionString.Replace(dbPassword ?? "", "***")}");

builder.Services.AddDbContext<AppDbContext>(options =>
    options
        .UseMySql(
            connectionString,
            new MySqlServerVersion(new Version(8, 0, 36))
        )
        .UseLazyLoadingProxies()
);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

builder.Services.AddScoped<DAOFactory, EFDAOFactory>();

var app = builder.Build();



app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();