namespace MSAProject.Common;

public class Resultado(bool sucesso, Erro? erro)
{
    public bool IsSucesso { get; } = sucesso;
    public Erro? Erro { get; } = erro;
    
    public static Resultado Ok() => new(true, null);
    public static Resultado<T> Ok<T>(T valor) => new(valor, true, null);
    
    public static Resultado Falha(Erro erro) => new(false, erro);
    public static Resultado<T> Falha<T>(Erro erro) => new(default, false, erro);
}

public class Resultado<T>(T valor, bool sucesso, Erro? erro)  : Resultado(sucesso, erro)
{
    public T Valor { get; } = valor;
}