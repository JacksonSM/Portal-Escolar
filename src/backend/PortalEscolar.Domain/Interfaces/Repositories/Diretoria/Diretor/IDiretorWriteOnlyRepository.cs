namespace PortalEscolar.Domain.Interfaces.Repositories.Diretoria.Diretor;
public interface IDiretorWriteOnlyRepository
{
    Task AddAsync(Domain.Entities.Diretoria.Diretor diretor);
}
