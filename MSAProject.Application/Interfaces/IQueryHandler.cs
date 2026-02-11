using MSAProject.Common;

namespace MSAProject.Application.Interfaces;

public interface IQueryHandler<TQuery> where TQuery : IQuery
{
    Task<Resultado> Handle(TQuery command, CancellationToken token);
}

public interface IQueryHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
{
    Task<Resultado<TResponse>> Handle(TQuery command, CancellationToken token);
}