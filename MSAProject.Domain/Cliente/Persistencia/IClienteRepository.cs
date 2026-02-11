namespace MSAProject.Domain.Cliente.Persistencia;

public interface IClienteRepository
{
    Task<Cliente?> ObtemClientePorIdAsync(int id, CancellationToken token);
    Task CriaClienteAsync(Cliente cliente, CancellationToken token);
    Task<bool> ExisteClientePorCnpjAsync(Cnpj cnpj, CancellationToken token);
}