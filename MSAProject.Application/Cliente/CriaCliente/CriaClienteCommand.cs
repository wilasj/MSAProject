using MSAProject.Application.Interfaces;

namespace MSAProject.Application.Cliente.CriaCliente;

public record CriaClienteCommand(string nomeFantasia, string cnpj): ICommand;