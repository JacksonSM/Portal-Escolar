namespace PortalEscolar.Domain.Interfaces.Repositories.Diretoria.Diretor;
public interface IDiretorReadOnlyRepository
{
    Task<bool> ExisteEmailAsync(string emailUsuario);
    Task<Entities.Diretoria.Diretor> ObterPorEmailSenhaAsync(string email, string senha);
    Task<Entities.Diretoria.Diretor> ObterPorEmailAsync(string email);
}
