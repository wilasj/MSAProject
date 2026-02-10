using MSAProject.Domain.Cliente;

namespace MSAProject.Tests.Cliente;

public class CnpjTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void CnpjVazioOuNulo_DeveRetornar_Erro(string? cnpj)
    {
        var resultado = Cnpj.Criar(cnpj);
        
        Assert.False(resultado.IsSucesso);
        Assert.Equal(resultado.Erro, CnpjErros.CnpjVazio);
    }

    [Theory]
    [InlineData("aaaaaaaa")]
    [InlineData("=-==-=-=++!!")]
    public void CnpjInvalido_DeveRetornar_Erro(string cnpj)
    {
        var resultado = Cnpj.Criar(cnpj);

        Assert.False(resultado.IsSucesso);
        Assert.Equal(resultado.Erro, CnpjErros.CnpjInvalido);
    }

    [Fact]
    public void CnpjMenorQueQuartozeCaracteres_DeveRetornar_Erro()
    {
        var cnpj = "123456";
        var resultado = Cnpj.Criar(cnpj);
        
        Assert.False(resultado.IsSucesso);
        Assert.Equal(resultado.Erro, CnpjErros.CnpjMenor);
    }
}