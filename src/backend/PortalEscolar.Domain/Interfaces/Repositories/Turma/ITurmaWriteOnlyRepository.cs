namespace PortalEscolar.Domain.Interfaces.Repositories.Turma;
public interface ITurmaWriteOnlyRepository
{
    Task AdicionarAsync(Entities.SalaAula.Turma turma);
}
