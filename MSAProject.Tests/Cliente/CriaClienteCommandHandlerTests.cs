using MSAProject.Application.Cliente.CriaCliente;
using MSAProject.Domain.Cliente;
using MSAProject.Domain.Cliente.Persistencia;
using NSubstitute;

namespace MSAProject.Tests.Cliente;

public class CriaClienteCommandHandlerTests
{
    private readonly IClienteRepository mockClienteRepository = Substitute.For<IClienteRepository>();
    
    [Fact]
    public async Task DeveCriarCliente_SeDadosValidos()
    {
        mockClienteRepository.ExisteClientePorCnpjAsync(Arg.Any<Cnpj>(), Arg.Any<CancellationToken>()).Returns(false);

        var handler = new CriaClienteCommandHandler(mockClienteRepository);
        
        var resultado = await handler.Handle(new CriaClienteCommand("MSA", "06.810.031/0001-94"), CancellationToken.None);
        
        Assert.True(resultado.IsSucesso);
        
        await mockClienteRepository
            .Received(1)
            .ExisteClientePorCnpjAsync(Arg.Any<Cnpj>(), Arg.Any<CancellationToken>());
        
        await mockClienteRepository
            .Received(1)
            .CriaClienteAsync(Arg.Is<Domain.Cliente.Cliente>(c => c.NomeFantasia == "MSA" && c.Cnpj.Valor == "06810031000194"), Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task NaoDeveCriarCliente_QuandoCnpjJaExiste()
    {
        mockClienteRepository.ExisteClientePorCnpjAsync(Arg.Any<Cnpj>(), Arg.Any<CancellationToken>()).Returns(true);
        
        var handler = new CriaClienteCommandHandler(mockClienteRepository);
        
        var resultado = await handler.Handle(new CriaClienteCommand("MSA", "06.810.031/0001-94"), CancellationToken.None);
        
        Assert.False(resultado.IsSucesso);

        await mockClienteRepository
            .Received(1)
            .ExisteClientePorCnpjAsync(Arg.Any<Cnpj>(), Arg.Any<CancellationToken>());  
    }
    
    [Fact]
    public async Task NaoDeveCriarCliente_QuandoNomeFantasiaVazio()
    {
        var handler = new CriaClienteCommandHandler(mockClienteRepository);
        
        var resultado = await handler.Handle(new CriaClienteCommand("", "06.810.031/0001-94"), CancellationToken.None);
        
        Assert.False(resultado.IsSucesso);

        await mockClienteRepository
            .Received(0)
            .CriaClienteAsync(Arg.Any<Domain.Cliente.Cliente>(), Arg.Any<CancellationToken>());

        await mockClienteRepository
            .Received(0)
            .ExisteClientePorCnpjAsync(Arg.Any<Cnpj>(), Arg.Any<CancellationToken>());  
    }
}