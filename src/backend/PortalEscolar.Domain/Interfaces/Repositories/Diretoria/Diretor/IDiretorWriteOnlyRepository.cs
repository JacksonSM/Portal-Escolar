namespace PortalEscolar.Domain.Interfaces.Repositories.Diretoria.Diretor;
public interface IDiretorWriteOnlyRepository
{
    Task AddAsync(Entities.Diretoria.Diretor diretor);
    void Atualizar(Entities.Diretoria.Diretor diretor);
}
