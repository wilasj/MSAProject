using Microsoft.Extensions.DependencyInjection;
using MSAProject.Application.Cliente.CriaCliente;
using MSAProject.Application.Cliente.ObtemClientePorId;
using MSAProject.Application.Interfaces;

namespace MSAProject.Application;

public static class InjecaoDependencia
{
    public static IServiceCollection AdicionaAplicacao(this IServiceCollection services)
    {
        //Se isso aqui crescesse muito, daria pra usar Scrutor, que faz o scan do assebmly e importa tudo mais facil
        services.AddScoped<ICommandHandler<CriaClienteCommand, int>, CriaClienteCommandHandler>();
        services.AddScoped<IQueryHandler<ObtemClientePorIdQuery, Domain.Cliente.Cliente>, ObtemClientePorIdQueryHandler>();
        
        return services;
    }
}