using System.Text.RegularExpressions;
using MSAProject.Common;

namespace MSAProject.Domain.Cliente;

public sealed class Cnpj(string valor)
{
    public string Valor { get; private set; } = valor;
    private static string NormalizarCnpj(string cnpj) => Regex.Replace(cnpj, @"[^\d]", "");
    
    public static Resultado<Cnpj> Criar(string? cnpj)
    {
        if (string.IsNullOrEmpty(cnpj))
        {
            return Resultado.Falha<Cnpj>(CnpjErros.CnpjVazio);
        }
        
        string cnpjNormalizado = NormalizarCnpj(cnpj.Trim());

        if (string.IsNullOrEmpty(cnpjNormalizado))
        {
            return Resultado.Falha<Cnpj>(CnpjErros.CnpjInvalido);
        }

        return cnpjNormalizado.Length != 14 ? Resultado.Falha<Cnpj>(CnpjErros.CnpjMenor) : Resultado.Ok(new Cnpj(cnpjNormalizado));
    }
}