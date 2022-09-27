namespace PortalEscolar.Domain.Interfaces.Repositories.Turma;
public interface ITurmaReadOnlyRepository
{
    Task<bool> ExisteTurmaAtivaPorIdAsync(long id);
}
