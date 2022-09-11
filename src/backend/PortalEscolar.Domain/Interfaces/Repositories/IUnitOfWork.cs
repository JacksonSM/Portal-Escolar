namespace PortalEscolar.Domain.Interfaces.Repositories;
public interface IUnitOfWork
{
    Task CommitAsync();
}
