using MSAProject.Application.Interfaces;

namespace MSAProject.Application.Cliente.ObtemClientePorId;

public record ObtemClientePorIdQuery(int Id): IQuery<Domain.Cliente.Cliente>;