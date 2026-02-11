using MSAProject.Domain.Cliente;
using MSAProject.Domain.Cliente.Persistencia;
using MSAProject.Infrastructure.Data.NHibernate.Cliente;
using MSAProject.Tests.Fixtures;

namespace MSAProject.Tests.Cliente;

public class ClienteRepositoryTests(BancoDadosFixture bancoDadosFixture) : IClassFixture<BancoDadosFixture>
{
    private readonly IClienteRepository _clienteRepository = new ClienteRepository(bancoDadosFixture.Sessao);
    
    [Fact]
    public async Task DeveCriar_Cliente()
    {
        var cliente = Domain.Cliente.Cliente.Criar("MSA RH", Cnpj.Criar("06.810.031/0001-94").Valor).Valor;
        
        await _clienteRepository.CriaClienteAsync(cliente, TestContext.Current.CancellationToken);
        
        var clienteCriado = await _clienteRepository.ObtemClientePorIdAsync(cliente.Id, TestContext.Current.CancellationToken);
        
        Assert.NotNull(clienteCriado);
    }

    [Fact]
    public async Task DeveRetornar_False_QuandoCnpjNaoExiste()
    {
        var existe = await _clienteRepository.ExisteClientePorCnpjAsync(Cnpj.Criar("00.000.000/0000-00").Valor, TestContext.Current.CancellationToken);
        
        Assert.False(existe);
    }
    
    [Fact]
    public async Task DeveRetornar_True_QuandoCnpjExiste()
    {
        var cliente = Domain.Cliente.Cliente.Criar("Outro RH", Cnpj.Criar("78.441.468/0001-09").Valor).Valor;

        await _clienteRepository.CriaClienteAsync(cliente, TestContext.Current.CancellationToken);
        
        var existe = await _clienteRepository.ExisteClientePorCnpjAsync(cliente.Cnpj, TestContext.Current.CancellationToken);
        
        Assert.True(existe);
    }

    [Fact]
    public async Task DeveRetornar_Cliente()
    {
        var cliente = Domain.Cliente.Cliente.Criar("Mais Um RH", Cnpj.Criar("74.503.895/0001-13").Valor).Valor;

        await _clienteRepository.CriaClienteAsync(cliente, TestContext.Current.CancellationToken);
        
        var criado = await _clienteRepository.ObtemClientePorIdAsync(1, TestContext.Current.CancellationToken);

        Assert.NotNull(criado);
        Assert.Equal(criado.Id, cliente.Id);
        Assert.Equal(criado.NomeFantasia, cliente.NomeFantasia);
    }

    [Fact]
    public async Task DeveRetornar_Null_QuandoNaoExiste()
    {
        var cliente = await _clienteRepository.ObtemClientePorIdAsync(100, TestContext.Current.CancellationToken);

        Assert.Null(cliente);
    }
}