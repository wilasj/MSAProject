using MSAProject.Common;

namespace MSAProject.Domain.Cliente;

public static class ClienteErros
{
    public static readonly Erro NomeVazio = new("Nome do cliente não pode ser vazio");
    public static readonly Erro NaoEncontrado = new("Cliente não encontrado");
}