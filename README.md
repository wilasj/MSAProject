# MSAProject

## Arquitetura

O projeto segue **Clean Architecture** com DDD, CQRS e Result Pattern.

Fiz algumas escolhas aqui que são diferentes do que vemos normalmente:

1 - Tenho um projeto `MSProject.Common`. O intuito desse projeto foi introduzir o padrão Result. Dessa forma, ao invés
de sempre lançar exceções (que, afinal, são pra casos excepcionais), retornamos um erro, já que ele era esperado.

2 - Dividi os projetos nas camadas do Clean Arch mas separei os diretórios por funcionalidade.
Então, por exemplo, as funcionalidades de Cliente no Domain ficam em `MSAProject.Domain/Cliente`.
Essa lógica segue, mas com algumas modificações - por exemplo, no `MSAProject.Infrastructure` - onde
temos O ORM usado antes. Isso permite flexibilidade ao decidir mudar o ORM, por exemplo. Poderia fazer
`MSAProject.Infrastructure/Configurations/EFCore/Cliente`.

3 - Optei por finalizar as transações no middleware da API.

```
Api → Application → Domain ← Common
         ↑
   Infrastructure
```

| Camada | Responsabilidade |
|--------|-----------------|
| **Api** | Endpoints (Minimal APIs), middleware de transação |
| **Application** | Commands, Queries e seus Handlers |
| **Domain** | Entidades, Value Objects, interfaces de repositório |
| **Infrastructure** | NHibernate, mapeamentos, repositórios |
| **Common** | Result Pattern (`Resultado<T>`, `Erro`) |

## Tecnologias

- .NET 9.0
- NHibernate 5.6.0 + FluentNHibernate
- SQLite (in-memory)
- xUnit v3 + NSubstitute + NetArchTest

## Como rodar

```bash
dotnet run --project MSAProject.Api
```

## Como rodar os testes

```bash
dotnet run --project MSAProject.Tests
```

## Endpoints

### Criar cliente
```
POST /cliente
Content-Type: application/json

{
  "nome": "Nome Fantasia",
  "cnpj": "12.345.678/0001-95"
}
```

### Buscar cliente por ID
```
GET /cliente/{id}
```
