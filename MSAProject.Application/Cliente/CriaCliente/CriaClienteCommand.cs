using MSAProject.Application.Interfaces;

namespace MSAProject.Application.Cliente.CriaCliente;

public record CriaClienteCommand(string NomeFantasia, string Cnpj): ICommand<int>;