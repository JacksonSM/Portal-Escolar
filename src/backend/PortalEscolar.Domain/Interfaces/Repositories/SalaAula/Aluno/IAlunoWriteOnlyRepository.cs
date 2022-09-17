namespace PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Aluno;
public interface IAlunoWriteOnlyRepository
{
    Task AdicionarAsync(Domain.Entities.SalaAula.Aluno aluno);
}
