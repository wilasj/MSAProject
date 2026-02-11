using MSAProject.Api.Endpoints.Cliente;
using MSAProject.Api.Middlewares;
using MSAProject.Application;
using MSAProject.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AdicionaInfraestrutura().AdicionaAplicacao();

var app = builder.Build();

app.UseMiddleware<UnitOfWorkMiddleware>();

app.MapeiaClienteEndpoints();

app.Run();