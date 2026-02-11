using System.Text.RegularExpressions;
using MSAProject.Common;

namespace MSAProject.Domain.Cliente;

public class Cnpj
{
    public virtual string Valor { get; protected set; }
    
    //Esse regex retira somente pontuações, e não letras. Fiz isso pra poder conseguir validar corretamente
    //em casos onde o tamanho é correto, mas existem letras.
    private static string RetirarPontuacoes(string cnpj) => Regex.Replace(cnpj, @"[\p{P}]", "");

    public static string NormalizarCnpj(string cnpj) => Regex.Replace(cnpj, @"[^\d]", "");
    
    protected Cnpj(){}
    
    private Cnpj(string valor)
    {
        Valor = valor;
    }

    public static Resultado<Cnpj> Criar(string? cnpj)
    {
        if (string.IsNullOrEmpty(cnpj))
        {
            return Resultado.Falha<Cnpj>(CnpjErros.CnpjVazio);
        }
        
        string cnpjSemPontuacao = RetirarPontuacoes(cnpj.Trim());

        if (!cnpjSemPontuacao.All(char.IsDigit))
        {
            return Resultado.Falha<Cnpj>(CnpjErros.CnpjInvalido);
        }

        return cnpjSemPontuacao.Length != 14 ? Resultado.Falha<Cnpj>(CnpjErros.CnpjMenor) : Resultado.Ok(new Cnpj(cnpjSemPontuacao));
    }
}