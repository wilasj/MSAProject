using MSAProject.Common;

namespace MSAProject.Domain.Cliente;

public class Cliente
{
    public virtual int Id { get; protected set; }
    public virtual string NomeFantasia { get; protected set; }
    public virtual Cnpj Cnpj { get; protected set; }
    public virtual bool Ativo { get; protected set; }

    //NHibernate precisa disso
    protected Cliente(){}
    
    private Cliente(string nomeFantasia, Cnpj cnpj, bool ativo)
    {
        NomeFantasia = nomeFantasia;
        Cnpj = cnpj;
        Ativo = ativo;
    }

    public static Resultado<Cliente> Criar(string nome, Cnpj cnpj)
    {
        if (string.IsNullOrEmpty(nome))
        {
            return Resultado.Falha<Cliente>(ClienteErros.NomeVazio);
        }

        var cliente = new Cliente(nome, cnpj, true);

        return Resultado.Ok(cliente);
    }
}