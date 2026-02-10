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
        Assert.Equal(CnpjErros.CnpjVazio, resultado.Erro);
    }

    [Theory]
    [InlineData("aaaaaaaa")]
    [InlineData("=-==-=-=++!!")]
    [InlineData("91.873.634/0001-0a")]
    public void CnpjInvalido_DeveRetornar_Erro(string cnpj)
    {
        var resultado = Cnpj.Criar(cnpj);

        Assert.False(resultado.IsSucesso);
        Assert.Equal(CnpjErros.CnpjInvalido, resultado.Erro);
    }

    [Fact]
    public void CnpjMenorQueQuartozeCaracteres_DeveRetornar_Erro()
    {
        var cnpj = "91.873.634/0001-0";
        var resultado = Cnpj.Criar(cnpj);
        
        Assert.False(resultado.IsSucesso);
        Assert.Equal(CnpjErros.CnpjMenor, resultado.Erro);
    }
}