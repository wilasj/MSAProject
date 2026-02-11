using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MSAProject.Infrastructure.Configurations.NHibernate.Cliente;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace MSAProject.Tests.Fixtures;

public class BancoDadosFixture: IDisposable
{
    private static readonly Assembly AssemblyMapeamento = typeof(ClienteMap).Assembly;
    private static Configuration _configuracao;
    public readonly ISession Sessao;
    
    public BancoDadosFixture()
    {
        var sessaoFactory = CriaSessaoFactory();
        Sessao = sessaoFactory.OpenSession();
        
        //Após abrir a conexão, faz o export do schema no banco
        new SchemaExport(_configuracao).Execute(false, true, false, Sessao.Connection, null);
    }

    private static ISessionFactory CriaSessaoFactory()
    {
        return Fluently
            .Configure()
            .Database(SQLiteConfiguration.Standard.InMemory())
            .Mappings(m => m.FluentMappings.AddFromAssembly(AssemblyMapeamento))
            .ExposeConfiguration(cfg => _configuracao = cfg)
            .BuildSessionFactory();
    }
    
    public void Dispose()
    {
        Sessao.Dispose();
    }
}