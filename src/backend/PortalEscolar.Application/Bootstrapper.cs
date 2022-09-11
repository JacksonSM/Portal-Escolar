using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortalEscolar.Application.Services.Criptografia;
using PortalEscolar.Application.Services.Mapping;
using PortalEscolar.Application.Services.Token;
using PortalEscolar.Application.UseCases.Diretora.Registrar;

namespace PortalEscolar.Application;
public static class Bootstrapper
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddAutoMapper(services);
        AddChaveAdicionalSenha(services, configuration);
        AddTokenJWT(services,configuration);
        AddUseCases(services);
    }
    private static void AddChaveAdicionalSenha(IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetRequiredSection("Config:ChaveAdicionalSenha");

        services.AddScoped(option => new EncriptadorDeSenha(section.Value));
    }

    public static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperConfig));
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
    }
}
