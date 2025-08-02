using psychoshare_api.Services;
using dao_library;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Repository Factory (Abstract Factory Pattern)
// Use MockRepositoryFactory for development (Swagger testing without database)
// Use DatabaseRepositoryFactory for production (real database connections)
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddScoped<IRepositoryFactory, MockRepositoryFactory>();
}
else
{
    builder.Services.AddScoped<IRepositoryFactory, DatabaseRepositoryFactory>();
}

// Register Ban, Report and Following services
builder.Services.AddScoped<IBanService, BanService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IFollowingService, FollowingService>();

// Repositories are created through the factory pattern
// This provides flexibility to switch between mock and real implementations
// Development: Uses mock data for Swagger testing
// Production: Uses real database connections
builder.Services.AddScoped<IBanRepository>(provider => 
    provider.GetRequiredService<IRepositoryFactory>().CreateBanRepository());
builder.Services.AddScoped<IReportRepository>(provider => 
    provider.GetRequiredService<IRepositoryFactory>().CreateReportRepository());
builder.Services.AddScoped<IFollowingRepository>(provider => 
    provider.GetRequiredService<IRepositoryFactory>().CreateFollowingRepository());

var app = builder.Build();

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
