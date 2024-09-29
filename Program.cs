using System.Text.Json.Serialization;
using backend_cine.Dbcontext;
using backend_cine.mapping;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string? connectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
builder.Services.AddAutoMapper(typeof(CinemaProfile).Assembly);

builder.Services.AddDbContext<DbContextCinema>(options =>
{
    options.UseSqlServer(connectionString);
    options.EnableSensitiveDataLogging();
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => "The server is running here");
app.Run();
