namespace PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Professora;
public interface IProfessoraReadOnlyRepository
{
    Task<bool> ExisteEmailAsync(string email);
}
