using MSAProject.Application.Cliente.CriaCliente;
using MSAProject.Application.Cliente.ObtemClientePorId;
using MSAProject.Application.Interfaces;

namespace MSAProject.Api.Endpoints.Cliente;

internal static class ClienteEndpoints
{
    public static void MapeiaClienteEndpoints(this WebApplication app)
    {
        app.MapPost("/cliente", async (
            CriaClienteRequest request,
            ICommandHandler<CriaClienteCommand, int> handler,
            CancellationToken token
        ) =>
        {
            var command = new CriaClienteCommand(request.Nome, request.Cnpj);

            var resultado = await handler.Handle(command, token);

            return !resultado.IsSucesso ? Results.BadRequest(resultado.Erro) : Results.Ok(resultado.Valor);
        });


        app.MapGet("/cliente/{id}", async (
            int id,
            IQueryHandler<ObtemClientePorIdQuery, Domain.Cliente.Cliente> handler,
            CancellationToken token
        ) =>
        {
            var command = new ObtemClientePorIdQuery(id);

            var resultado = await handler.Handle(command, token);

            return !resultado.IsSucesso ? Results.BadRequest(resultado.Erro) : Results.Ok(resultado.Valor);
        });
    }
}