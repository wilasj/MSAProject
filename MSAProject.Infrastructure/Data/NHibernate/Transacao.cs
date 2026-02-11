using MSAProject.Application.Interfaces;
using NHibernate;

namespace MSAProject.Infrastructure.Data.NHibernate;

internal sealed class Transacao(ITransaction transaction) : ITransacao
{
    public Task CommitaAsync() => transaction.CommitAsync();

    public Task RollbackAsync() => transaction.RollbackAsync();

    public void Dispose()
    {
        transaction.Dispose();
    }
}