using Microsoft.EntityFrameworkCore;
using PortalEscolar.Domain.Entities.Diretoria;
using PortalEscolar.Domain.Entities.Papel;

namespace PortalEscolar.Infrastructure.Context;
public static class SemearDB
{
    private const long PAPEL_DIRETOR_ID = 1;
    private const long PAPEL_PROFESSORA_ID = 2;
    private const long PAPEL_ALUNO_ID = 3;

    private const long DIRETOR_ID = 1;
    public static void SemearPapel(this ModelBuilder builder)
    {
        builder.Entity<Papel>().HasData(new Papel("Diretor", PAPEL_DIRETOR_ID));
        builder.Entity<Papel>().HasData(new Papel("Professora", PAPEL_PROFESSORA_ID));
        builder.Entity<Papel>().HasData(new Papel("Aluno", PAPEL_ALUNO_ID));   
    }
    public static void SemearDiretor(this ModelBuilder builder)
    {
        builder.Entity<Diretor>().HasData(new Diretor
        {
            Id = DIRETOR_ID,
            DataNascimento = DateTime.Today.AddYears(-20),
            Email = "diretor@portalescolar.com",
            NomeCompleto = "Diretor",
            Senha = "Diretor321"
        });
        builder.Entity<PapelUsuario>().HasData(new PapelUsuario
        {
            EmailUsuario = "diretor@portalescolar.com",
            PapelId = PAPEL_DIRETOR_ID
        });
    }
}
