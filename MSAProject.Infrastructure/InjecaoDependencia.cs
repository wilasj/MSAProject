using Microsoft.Extensions.DependencyInjection;
using MSAProject.Application.Interfaces;
using MSAProject.Infrastructure.Data.NHibernate;
using NHibernate;

namespace MSAProject.Infrastructure;

public static class InjecaoDependencia
{
    public static IServiceCollection AdicionaInfraestrutura(this IServiceCollection services)
    {
        services.AddSingleton(NHibernateConfig.CreateSessionFactory());

        services.AddScoped(sp => sp.GetRequiredService<ISessionFactory>().OpenSession());
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    } 
}