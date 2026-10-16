using HooPulse.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

builder.Services.AddInfrastructure(
    config);

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();