namespace PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Professora;
public interface IProfessoraReadOnlyRepository
{
    Task<bool> ExisteEmailAsync(string email);
    Task<bool> ExistePorIdAsync(long id);
    Task<Entities.SalaAula.ProfessoraContext.Professora> ObterPorEmailESenhaAsync(string email, string senha);
    Task<Entities.SalaAula.ProfessoraContext.Professora> ObterPorEmailAsync(string email);

}
