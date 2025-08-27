using psychoshare_api.Services;
using dao_library;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Register services and repositories directly
builder.Services.AddScoped<IBanService, BanService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IFollowingService, FollowingService>();
builder.Services.AddScoped<IBanRepository, BanRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IFollowingRepository, FollowingRepository>();

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
