using MSAProject.Common;

namespace MSAProject.Domain.Cliente;

public sealed class Cliente
{
    public int Id { get; private set; }
    public string NomeFantasia { get; private set; }
    public Cnpj Cnpj { get; private set; }
    public bool Ativo { get; private set; }

    private Cliente(string nomeFantasia, Cnpj cnpj, bool ativo)
    {
        NomeFantasia = nomeFantasia;
        Cnpj = cnpj;
        Ativo = ativo;
    }

    public static Resultado<Cliente> Criar(string nome, string cnpj)
    {
        if (string.IsNullOrEmpty(nome))
        {
            return Resultado.Falha<Cliente>(ClienteErros.NomeVazio);
        }

        var resultadoCnpj = Cnpj.Criar(cnpj);

        if (!resultadoCnpj.IsSucesso)
        {
            return Resultado.Falha<Cliente>(resultadoCnpj.Erro!);
        }

        var cliente = new Cliente(nome, resultadoCnpj.Valor, true);

        return Resultado.Ok(cliente);
    }
}