using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace MSAProject.Infrastructure.Data.NHibernate;

public static class NHibernateConfig
{
    public static ISessionFactory CreateSessionFactory()
    {
        return Fluently
            .Configure()
            .Database(SQLiteConfiguration.Standard.UsingFile("database.db"))
            .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
            .ExposeConfiguration(cfg =>
            {
                new SchemaExport(cfg)
                    .Create(false, true);
            })
            .BuildSessionFactory();
    }
}