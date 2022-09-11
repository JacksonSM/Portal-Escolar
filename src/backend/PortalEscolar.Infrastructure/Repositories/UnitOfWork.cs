using PortalEscolar.Domain.Interfaces.Repositories;
using PortalEscolar.Infrastructure.Context;

namespace PortalEscolar.Infrastructure.Repositories;
public class UnitOfWork : IDisposable, IUnitOfWork
{
    private readonly PortalEscolarDbContext _contexto;
    private bool _disposed;

    public UnitOfWork(PortalEscolarDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task CommitAsync()
    {
        await _contexto.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _contexto.Dispose();
        }

        _disposed = true;
    }
}

