using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortalEscolar.Domain.Interfaces.Repositories;
using PortalEscolar.Domain.Interfaces.Repositories.Diretoria.Diretor;
using PortalEscolar.Domain.Interfaces.Repositories.Diretoria.Matricula;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Aluno;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Exercicio;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Professora;
using PortalEscolar.Domain.Interfaces.Repositories.Turma;
using PortalEscolar.Infrastructure.Context;
using PortalEscolar.Infrastructure.Helpers;
using PortalEscolar.Infrastructure.Mapping;
using PortalEscolar.Infrastructure.Repositories;
using PortalEscolar.Infrastructure.Repositories.Aluno;
using PortalEscolar.Infrastructure.Repositories.Diretoria;
using PortalEscolar.Infrastructure.Repositories.Exercicio;
using PortalEscolar.Infrastructure.Repositories.Professora;
using PortalEscolar.Infrastructure.Repositories.Turma;
using System.Reflection;

namespace PortalEscolar.Infrastructure;
public static class Bootstrapper
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddContext(services, configuration);
        AddRepository(services);
        //AddMapping(services);
        AddMongoConfig(services, configuration);
    }
    public static void AddContext(IServiceCollection services, IConfiguration configuration)
    {
        _ = bool.TryParse(configuration.GetSection("Config:DatabaseInMemory").Value, out bool dbInMemory);

        if (!dbInMemory)
        {

            var ConnectionString = configuration.GetSection("Config:ConnectionString").Value;

            services.AddDbContext<PortalEscolarDbContext>(options =>
                options.UseSqlServer(ConnectionString));
        }

    }
    public static void AddRepository(IServiceCollection services)
    {
        services.AddScoped<IDiretorWriteOnlyRepository, DiretorRepository>();
        services.AddScoped<IDiretorReadOnlyRepository, DiretorRepository>();

        services.AddScoped<IProfessoraWriteOnlyRepository, ProfessoraRepository>();
        services.AddScoped<IProfessoraReadOnlyRepository, ProfessoraRepository>();

        services.AddScoped<IExercicioWriteOnlyRepository, ExercicioParaResolverRepository>();

        services.AddScoped<IAlunoReadOnlyRepository, AlunoRepository>();
        services.AddScoped<IAlunoWriteOnlyRepository, AlunoRepository>();

        services.AddScoped<ITurmaWriteOnlyRepository, TurmaRepository>();
        services.AddScoped<ITurmaReadOnlyRepository, TurmaRepository>();

        services.AddScoped<IMatriculaWriteOnlyRepository, MatriculaRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
    public static void AddMapping(IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(InfrastructureProfile)));
    }
    public static void AddMongoConfig(IServiceCollection services, IConfiguration configuration)
    {
        //MongoDbConfig
        //services.Configure<ExercicioDatabaseSettings>(
        //    configuration.GetSection("MongoDbConfig"));
    }
}
