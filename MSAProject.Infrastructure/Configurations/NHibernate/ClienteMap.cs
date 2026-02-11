using FluentNHibernate.Mapping;
using MSAProject.Domain.Cliente;

namespace MSAProject.Infrastructure.Configurations.NHibernate;

public class ClienteMap: ClassMap<Cliente>
{
    public ClienteMap()
    {
        Id(c => c.Id);
        Map(c => c.NomeFantasia).Not.Nullable().Length(150);
        Component(c => c.Cnpj, cj =>
        {
            cj.Map(x => x.Valor).Not.Nullable().Length(14).Unique();
        });
        Map(c => c.Ativo).Not.Nullable();
    }
}