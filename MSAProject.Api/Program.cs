using MSAProject.Api.Middlewares;
using MSAProject.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AdicionaInfraestrutura();

var app = builder.Build();

app.UseMiddleware<UnitOfWorkMiddleware>();

app.MapGet("/", () => "Hello World!");

app.Run();