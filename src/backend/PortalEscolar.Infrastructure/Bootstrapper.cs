﻿using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PortalEscolar.Infrastructure.Context;
using PortalEscolar.Domain.Interfaces.Repositories.Diretoria.Diretor;
using PortalEscolar.Infrastructure.Repositories.Diretoria;
using PortalEscolar.Domain.Interfaces.Repositories;
using PortalEscolar.Infrastructure.Repositories;

namespace PortalEscolar.Infrastructure;
public static class Bootstrapper
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddContext(services,configuration);
        AddRepository(services);
    }
    public static void AddContext(IServiceCollection services, IConfiguration configuration)
    {
        var ConnectionString = configuration.GetSection("Config:ConnectionString").Value;

        services.AddDbContext<PortalEscolarDbContext>(options =>
            options.UseSqlServer(ConnectionString));

    }
    public static void AddRepository(IServiceCollection services)
    {
        services.AddScoped<IDiretorWriteOnlyRepository, DiretorRepository>();
        services.AddScoped<IDiretorReadOnlyRepository, DiretorRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}