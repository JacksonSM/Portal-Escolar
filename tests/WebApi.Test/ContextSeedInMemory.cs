using PortalEscolar.Domain.Entities.SalaAula;
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

    internal static (PortalEscolar.Domain.Entities.SalaAula.Professora _professora, string _senhaProfessora) SeedProfessora(PortalEscolarDbContext context)
    {
        (var professora, var senha) = ProfessoraBuilder.Build();

        context.Professora.Add(professora);
        context.SaveChanges();

        return (professora, senha);
    }

    internal static (Aluno _aluno, string _senhaAluno) SeedAluno(PortalEscolarDbContext context)
    {
        (var aluno, var senha) = AlunoBuilder.Build();

        context.Aluno.Add(aluno);
        context.SaveChanges();

        return (aluno, senha);
    }
}
