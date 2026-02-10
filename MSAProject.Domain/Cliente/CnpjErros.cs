using MSAProject.Common;

namespace MSAProject.Domain.Cliente;

public static class CnpjErros
{
    public static readonly Erro CnpjVazio = new("CNPJ não pode ser vazio");
    public static readonly Erro CnpjInvalido = new("CNPJ deve conter números válidos");
    public static readonly Erro CnpjMenor = new("CNPJ deve conter 14 caracteres");
}