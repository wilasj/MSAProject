using MSAProject.Domain;

namespace MSAProject.Tests.Cliente;

public class ClienteTests
{
    private static readonly string CnpjTeste = "22.879.135/0001-01";
    
    [Fact]
    public void NomeVazio_DeveRetornar_Falha()
    {
        var resultado = Domain.Cliente.Cliente.Criar(string.Empty, CnpjTeste);

        Assert.False(resultado.IsSucesso);
        Assert.Equal(ClienteErros.NomeVazio, resultado.Erro);
    }

    [Fact]
    public void NomeNulo_DeveRetornar_Falha()
    {
        var resultado = Domain.Cliente.Cliente.Criar(null, CnpjTeste);

        Assert.False(resultado.IsSucesso);
        Assert.Equal(ClienteErros.NomeVazio, resultado.Erro);
    }

    [Fact]
    public void CnpjInvalido_DeveRetornar_Falha()
    {
        var resultado = Domain.Cliente.Cliente.Criar("MSA", "cnpj invalido");
        
        Assert.False(resultado.IsSucesso);
    }

    [Fact]
    public void ClienteValido_DeveRetornar_Cliente()
    {
        var resultado = Domain.Cliente.Cliente.Criar("MSA", CnpjTeste);
        
        Assert.True(resultado.IsSucesso);
        Assert.Null(resultado.Erro);
    }
}