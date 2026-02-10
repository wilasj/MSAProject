using MSAProject.Common;

namespace MSAProject.Application.Interfaces;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task<Resultado> Handle(TCommand command, CancellationToken token);
}

public interface ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    Task<Resultado<TResponse>> Handle(TCommand command, CancellationToken token);
}