namespace PortalEscolar.Domain.Interfaces.Repositories.Diretoria.Diretor;
public interface IDiretorReadOnlyRepository
{
    Task<bool> ExisteEmailAsync(string emailUsuario);
    Task<PortalEscolar.Domain.Entities.Diretoria.Diretor> ObterPorEmailSenha(string email, string senha);
}
