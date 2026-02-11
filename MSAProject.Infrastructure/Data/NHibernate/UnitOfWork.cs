using MSAProject.Application.Interfaces;
using NHibernate;

namespace MSAProject.Infrastructure.Data.NHibernate;

internal sealed class UnitOfWork(ISession session) : IUnitOfWork
{
    public ITransacao ComecaTransacao()
    {
        return new Transacao(session.BeginTransaction());
    }
}