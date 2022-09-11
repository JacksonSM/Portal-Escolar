namespace PortalEscolar.Domain.Interfaces.Repositories.Diretoria.Diretor;
public interface IDiretorReadOnlyRepository
{
    Task<bool> ExisteEmailAsync(string emailUsuario);
}
