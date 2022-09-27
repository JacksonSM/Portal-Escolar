namespace PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Aluno;
public interface IAlunoWriteOnlyRepository
{
    Task AdicionarAsync(Entities.SalaAula.AlunoContext.Aluno aluno);
}
