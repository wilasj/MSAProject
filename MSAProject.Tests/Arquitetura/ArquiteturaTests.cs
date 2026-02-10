using System.Reflection;
using MSAProject.Application.Interfaces;
using MSAProject.Common;
using MSAProject.Domain.Cliente;
using MSAProject.Infrastructure;
using NetArchTest.Rules;
using Shouldly;

namespace MSAProject.Tests.Arquitetura;

public class ArquiteturaTests
{
    private static readonly Assembly AssemblyDominio = typeof(Cnpj).Assembly;
    private static readonly Assembly AssemblyAplicacao = typeof(ICommand).Assembly;
    private static readonly Assembly AssemblyInfraestrutura = typeof(DependencyInjection).Assembly;
    private static readonly Assembly AssemblyComum = typeof(Resultado).Assembly;
    
    /// <summary>
    /// Camadas que não devem ter dependências, da esquerda pra direita.
    /// </summary>
    public static IEnumerable<object[]> CamadasIncompativeis => new List<object[]>
    {
        new object[] { AssemblyDominio, "MSProject.Api" },
        new object[] { AssemblyDominio, AssemblyAplicacao.GetName().Name },
        new object[] { AssemblyDominio, AssemblyInfraestrutura.GetName().Name },
        new object[] { AssemblyAplicacao, AssemblyInfraestrutura.GetName().Name },
        new object[] { AssemblyAplicacao, "MSProject.Api" },
        new object[] { AssemblyInfraestrutura, "MSProject.Api" },
        new object[] { AssemblyComum, "MSProject.Api" },
        new object[] { AssemblyComum, AssemblyDominio.GetName().Name },
        new object[] { AssemblyComum, AssemblyAplicacao.GetName().Name },
        new object[] { AssemblyComum, AssemblyInfraestrutura.GetName().Name },
    };

    [Theory]
    [MemberData(nameof(CamadasIncompativeis))]
    public void CamadaInterna_NaoDeveReferenciar_CamadaExterna(Assembly camadaInterna, string camadaExterna)
    {
        var result = Types
            .InAssembly(camadaInterna)
            .ShouldNot()
            .HaveDependencyOn(camadaExterna)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}