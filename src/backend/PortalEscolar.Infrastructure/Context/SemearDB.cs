using Microsoft.EntityFrameworkCore;
using PortalEscolar.Domain.Entities.Diretoria;

namespace PortalEscolar.Infrastructure.Context;
public static class SemearDB
{
    private const long DIRETOR_ID = 1;

    public static void SemearDiretor(this ModelBuilder builder)
    {
        var diretor = new Diretor
           (
               nomeCompleto:"Diretor",
               dataNascimento: DateTime.Today.AddYears(-20),
               email: "diretor@portalescolar.com",
               senha: "Diretor321"
           );
        diretor.Id = DIRETOR_ID;
        builder.Entity<Diretor>().HasData(diretor);
    }
}
