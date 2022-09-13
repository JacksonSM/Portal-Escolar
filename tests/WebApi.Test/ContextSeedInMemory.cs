using PortalEscolar.Infrastructure.Context;
using Utilities.Entities;

namespace WebApi.Test;

public class ContextSeedInMemory
{
    public static (PortalEscolar.Domain.Entities.Diretoria.Diretor,string) SeedDiretor(PortalEscolarDbContext context)
    {
        (var diretor,var senha) = DiretorBuilder.Build();

        context.Diretor.Add(diretor);
        context.SaveChanges();

        return (diretor,senha);
    }

   
}
