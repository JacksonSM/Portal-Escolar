using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortalEscolar.Application.Services.Criptografia;
using PortalEscolar.Application.Services.Mapping;
using PortalEscolar.Application.Services.Token;
using PortalEscolar.Application.Services.UsuarioLogado;
using PortalEscolar.Application.UseCases.Aluno.EnviarExercicioResolvido;
using PortalEscolar.Application.UseCases.Aluno.Login;
using PortalEscolar.Application.UseCases.Aluno.ObterExercicio;
using PortalEscolar.Application.UseCases.Aluno.ObterListaExercicios;
using PortalEscolar.Application.UseCases.Aluno.Registrar;
using PortalEscolar.Application.UseCases.Diretora.AlterarInfoPessoal;
using PortalEscolar.Application.UseCases.Diretora.AlterarSenha;
using PortalEscolar.Application.UseCases.Diretora.Login;
using PortalEscolar.Application.UseCases.Diretora.Matricular;
using PortalEscolar.Application.UseCases.Diretora.PrimeiroAcesso;
using PortalEscolar.Application.UseCases.Diretora.Registrar;
using PortalEscolar.Application.UseCases.Professora.EnviarExercicio;
using PortalEscolar.Application.UseCases.Professora.Login;
using PortalEscolar.Application.UseCases.Professora.Registrar;
using PortalEscolar.Application.UseCases.Turma.Criar;

namespace PortalEscolar.Application;
public static class Bootstrapper
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddChaveAdicionalSenha(services, configuration);
        AddTokenJWT(services,configuration);
        AddUseCases(services);
        AddServices(services);
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IUsuarioLogado, UsuarioLogado>();
    }

    private static void AddChaveAdicionalSenha(IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetRequiredSection("Config:ChaveAdicionalSenha");

        services.AddScoped(option => new EncriptadorDeSenha(section.Value));
    }
    
    public static void AddTokenJWT(IServiceCollection services,IConfiguration configuration)
    {
        var duracaoTokenEmMinutos = configuration.GetRequiredSection("Config:Token:DuracaoTokenEmMinutos").Value;
        var chaveDeSeguranca = configuration.GetRequiredSection("Config:Token:ChaveDeSeguranca").Value;

        services.AddScoped(option => new TokenController(int.Parse(duracaoTokenEmMinutos),chaveDeSeguranca));
    }
    public static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegistrarDiretorUseCase, RegistrarDiretorUseCase>();
        services.AddScoped<ILoginDiretorUseCase, LoginDiretorUseCase>();
        services.AddScoped<IPrimeiroAcessoDiretor, PrimeiroAcessoDiretor>();
        services.AddScoped<IAlterarSenhaDiretorUseCase, AlterarSenhaDiretorUseCase>();
        services.AddScoped<IAlterarInfoPessoalDiretorUseCase, AlterarInfoPessoalDiretorUseCase>();

        services.AddScoped<IRegistrarProfessoraUseCase, RegistrarProfessoraUseCase>();
        services.AddScoped<ILoginProfessoraUseCase, LoginProfessoraUseCase>();

        services.AddScoped<IEnviarExercicioUseCase, EnviarExercicioUseCase>();
        services.AddScoped<IEnviarExercicioResolvidoUseCase, EnviarExercicioResolvidoUseCase>();

        services.AddScoped<IRegistrarAlunoUseCase, RegistrarAlunoUseCase>();
        services.AddScoped<ILoginAlunoUseCase, LoginAlunoUseCase>();

        services.AddScoped<IObterExercicioUseCase, ObterExercicioUseCase>();
        services.AddScoped<IObterListaExercicios, ObterListaExercicios>();

        services.AddScoped<ICriarTurmaUseCase, CriarTurmaUseCase>();

        services.AddScoped<IMatricularAlunoUseCase, MatricularAlunoUseCase>();

    }
}
