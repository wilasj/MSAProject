using MSAProject.Application.Cliente.ObtemClientePorId;
using MSAProject.Domain.Cliente;
using MSAProject.Domain.Cliente.Persistencia;
using NSubstitute;

namespace MSAProject.Tests.Cliente;

public class ObtemClientePorIdQueryHandlerTests
{
    private readonly IClienteRepository mockClienteRepository = Substitute.For<IClienteRepository>();
    
    [Fact]
    public async Task DeveRetornar_Cliente()
    {
        var cnpj = Cnpj.Criar("06.810.031/0001-94").Valor;
        var cliente = Domain.Cliente.Cliente.Criar("MSA", cnpj).Valor;
        
        mockClienteRepository
            .ObtemClientePorIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(cliente);

        var handler = new ObtemClientePorIdQueryHandler(mockClienteRepository);
        
        var resultado = await handler.Handle(new ObtemClientePorIdQuery(1), CancellationToken.None);
        
        Assert.True(resultado.IsSucesso);
        Assert.NotNull(resultado.Valor);
        Assert.Equal(cliente.Id, resultado.Valor.Id);
        Assert.Equal(cliente.NomeFantasia, resultado.Valor.NomeFantasia);
        
        await mockClienteRepository.Received(1).ObtemClientePorIdAsync(1, Arg.Any<CancellationToken>());        
    }

    [Fact]
    public async Task DeveRetornarNulo_QuandoNaoEncontrado()
    {
        mockClienteRepository
            .ObtemClientePorIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns((Domain.Cliente.Cliente?) null);
        
        var handler = new ObtemClientePorIdQueryHandler(mockClienteRepository);
        
        var resultado = await handler.Handle(new ObtemClientePorIdQuery(1), CancellationToken.None);
        
        Assert.False(resultado.IsSucesso);
        await mockClienteRepository.Received(1).ObtemClientePorIdAsync(1, Arg.Any<CancellationToken>());       
    }
}