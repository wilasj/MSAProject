namespace MSAProject.Domain.Cliente.Persistencia;

public interface IClienteRepository
{
    Task<Cliente?> ObtemClientePorIdAsync(int id);
    Task CriarClienteAsync(Cliente cliente);
}