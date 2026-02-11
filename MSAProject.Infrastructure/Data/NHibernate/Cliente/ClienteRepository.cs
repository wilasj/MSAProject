using MSAProject.Domain.Cliente;
using MSAProject.Domain.Cliente.Persistencia;
using NHibernate;
using NHibernate.Linq;

namespace MSAProject.Infrastructure.Data.NHibernate.Cliente;

public class ClienteRepository: IClienteRepository
{
    private readonly ISession _sessao;
    
    public ClienteRepository(ISession sessao)
    {
        _sessao = sessao;
    }

    public Task<Domain.Cliente.Cliente?> ObtemClientePorIdAsync(int id, CancellationToken token)
    {
        return _sessao.Query<Domain.Cliente.Cliente>().SingleOrDefaultAsync(c => c.Id == id, token)!;
    }

    public Task CriaClienteAsync(Domain.Cliente.Cliente cliente, CancellationToken token)
    {
        return _sessao.SaveAsync(cliente, token);
    }

    public Task<bool> ExisteClientePorCnpjAsync(Cnpj cnpj, CancellationToken token)
    {
        return _sessao.Query<Domain.Cliente.Cliente>().AnyAsync(c => c.Cnpj.Valor == cnpj.Valor, token);
    }
}