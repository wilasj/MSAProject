using MSAProject.Application.Interfaces;
using MSAProject.Common;
using MSAProject.Domain.Cliente;
using MSAProject.Domain.Cliente.Persistencia;

namespace MSAProject.Application.Cliente.ObtemClientePorId;

public class ObtemClientePorIdQueryHandler(IClienteRepository clienteRepository): IQueryHandler<ObtemClientePorIdQuery, Domain.Cliente.Cliente>
{
    public async Task<Resultado<Domain.Cliente.Cliente>> Handle(ObtemClientePorIdQuery command, CancellationToken token)
    {
        var cliente = await clienteRepository.ObtemClientePorIdAsync(command.Id, token);

        return cliente is null ? Resultado.Falha<Domain.Cliente.Cliente>(ClienteErros.NaoEncontrado) : Resultado.Ok(cliente);
    }
}