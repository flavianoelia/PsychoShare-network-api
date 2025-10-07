using Microsoft.EntityFrameworkCore;
using dao_library;
using psychoshare_api;
using dao_library.Contexts;
using DotNetEnv;

// Load .env.local file
Env.Load("../.env.local");

var builder = WebApplication.CreateBuilder(args);

// Build connection string using environment variables
var connectionString = $"Server={Environment.GetEnvironmentVariable("DB_SERVER") ?? "localhost"};" +
                       $"Port={Environment.GetEnvironmentVariable("DB_PORT") ?? "3306"};" +
                       $"Database={Environment.GetEnvironmentVariable("DB_NAME") ?? "psychoshare"};" +
                       $"Uid={Environment.GetEnvironmentVariable("DB_USER") ?? "root"};" +
                       $"Pwd={Environment.GetEnvironmentVariable("DB_PASSWORD") ?? ""};";

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

app.UseAuthorization();

app.MapControllers();

app.Run();