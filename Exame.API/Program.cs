using Exame.Infrastructure;
using Exame.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddApplicationServices();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
