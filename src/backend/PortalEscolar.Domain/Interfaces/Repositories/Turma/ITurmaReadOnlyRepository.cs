namespace PortalEscolar.Domain.Interfaces.Repositories.Turma;
public interface ITurmaReadOnlyRepository
{
    Task<bool> ExisteTurmaAtivaPorIdAsync(long id);
    Task<Entities.SalaAula.Turma> ObterTurmaPorIdAsync(long id);
}
