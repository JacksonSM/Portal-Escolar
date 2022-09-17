namespace PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Aluno;
public interface IAlunoReadOnlyRepository
{
    Task<bool> ExisteEmailAsync(string email);
}
