using FluentNHibernate.Mapping;

namespace MSAProject.Infrastructure.Configurations.NHibernate.Cliente;

public class ClienteMap: ClassMap<Domain.Cliente.Cliente>
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