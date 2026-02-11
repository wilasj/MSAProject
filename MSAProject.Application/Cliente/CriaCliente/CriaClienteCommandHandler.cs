using MSAProject.Application.Interfaces;
using MSAProject.Common;
using MSAProject.Domain.Cliente;
using MSAProject.Domain.Cliente.Persistencia;

namespace MSAProject.Application.Cliente.CriaCliente;

internal sealed class CriaClienteCommandHandler(
    IClienteRepository clienteRepository
    ): ICommandHandler<CriaClienteCommand, int>
{
    public async Task<Resultado<int>> Handle(CriaClienteCommand command, CancellationToken token)
    {
        //como não tô usando FluentValidations aqui, vou utilizar a propria criação do Cliente/Cnpj pra validação
        var resultadoCnpj = Cnpj.Criar(command.Cnpj);

        if (!resultadoCnpj.IsSucesso)
        {
            return Resultado.Falha<int>(resultadoCnpj.Erro);
        }
        
        var resultadoCliente = Domain.Cliente.Cliente.Criar(command.NomeFantasia, resultadoCnpj.Valor);

        if (!resultadoCliente.IsSucesso)
        {
            return Resultado.Falha<int>(resultadoCliente.Erro);
        }
        
        if (await clienteRepository.ExisteClientePorCnpjAsync(resultadoCnpj.Valor, token))
        {
            return Resultado.Falha<int>(CnpjErros.CnpjJaExiste);
        }
        
        await clienteRepository.CriaClienteAsync(resultadoCliente.Valor, token);

        return Resultado.Ok(resultadoCliente.Valor.Id);
    }
}