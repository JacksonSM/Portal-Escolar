namespace PortalEscolar.Domain.Interfaces.Repositories.Turma;
public interface ITurmaReadOnlyRepository
{
    Task<bool> ExistePorIdAsync(long id);
}
