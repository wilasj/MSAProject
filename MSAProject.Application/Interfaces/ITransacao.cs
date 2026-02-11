namespace MSAProject.Application.Interfaces;

public interface ITransacao: IDisposable
{
    Task CommitaAsync();
    Task RollbackAsync();
}