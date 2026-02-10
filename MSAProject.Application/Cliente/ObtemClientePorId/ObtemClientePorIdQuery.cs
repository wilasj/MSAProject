using MSAProject.Application.Interfaces;
using MSAProject.Common;

namespace MSAProject.Application.Cliente.ObtemClientePorId;

public record ObtemClientePorIdQuery(int id): IQuery<Resultado<Domain.Cliente.Cliente>>;