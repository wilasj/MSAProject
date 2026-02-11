using MSAProject.Application.Interfaces;

namespace MSAProject.Api.Middlewares;

public class UnitOfWorkMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, IUnitOfWork uow)
    {
        using var transacao =  uow.ComecaTransacao();

        try
        {
            await next(context);
            await transacao.CommitaAsync();
        }
        catch(Exception e)
        {
            await transacao.RollbackAsync();
            throw;
        }
    }
}