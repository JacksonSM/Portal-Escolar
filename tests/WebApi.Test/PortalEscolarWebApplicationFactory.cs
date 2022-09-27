using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PortalEscolar.Infrastructure.Context;

namespace WebApi.Test;

public class PortalEscolarWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    private PortalEscolar.Domain.Entities.Diretoria.Diretor _diretor;
    private string _senha;

    private PortalEscolar.Domain.Entities.SalaAula.ProfessoraContext.Professora _professora;
    private string _senhaProfessora;

    private PortalEscolar.Domain.Entities.SalaAula.AlunoContext.Aluno _aluno;
    private string _senhaAluno;


    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var descritor = services.SingleOrDefault(d => d.ServiceType == typeof(PortalEscolarDbContext));
                if (descritor is not null)
                    services.Remove(descritor);

                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<PortalEscolarDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(provider);
                });

                var serviceProvider = services.BuildServiceProvider();
                

                using var scope = serviceProvider.CreateScope();
                var scopeService = scope.ServiceProvider;
                
                var database = scopeService.GetRequiredService<PortalEscolarDbContext>();

                database.Database.EnsureDeleted();

                (_diretor,_senha) = ContextSeedInMemory.SeedDiretor(database);
                (_professora, _senhaProfessora) = ContextSeedInMemory.SeedProfessora(database);
                (_aluno, _senhaAluno) = ContextSeedInMemory.SeedAluno(database);
            });
    }
    public PortalEscolar.Domain.Entities.Diretoria.Diretor ObterDiretor()
    {
        return _diretor;
    }
    public string ObterSenha()
    {
        return _senha;
    }
    public PortalEscolar.Domain.Entities.SalaAula.ProfessoraContext.Professora ObterProfessora()
    {
        return _professora;
    }
    public string ObterSenhaProfessora()
    {
        return _senhaProfessora;
    }
    public PortalEscolar.Domain.Entities.SalaAula.AlunoContext.Aluno ObterAluno()
    {
        return _aluno;
    }
    public string ObterSenhaAluno()
    {
        return _senhaAluno;
    }
}
