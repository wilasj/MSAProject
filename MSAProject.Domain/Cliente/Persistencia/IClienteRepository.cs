namespace MSAProject.Domain.Cliente.Persistencia;

public interface IClienteRepository
{
    Task<Cliente?> ObtemClientePorIdAsync(int id, CancellationToken token);
    Task CriarClienteAsync(Cliente cliente, CancellationToken token);
}