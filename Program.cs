using System.Text.Json.Serialization;
using backend_cine.Dbcontext;
using backend_cine.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string? connectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ShowtimeService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TheaterService>();
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<CinemaService>();



builder.Services.AddDbContext<DbContextCinema>(options =>
{
    //mas de 40 solicitudes hacen que pierda performance
    options.UseSqlServer(connectionString, o => o.MinBatchSize(1));
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
