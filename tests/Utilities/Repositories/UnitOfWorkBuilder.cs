using Moq;
using PortalEscolar.Domain.Interfaces.Repositories;

namespace Utilities.Repositories;

public class UnitOfWorkBuilder
{
    private static UnitOfWorkBuilder _instance;
    private readonly Mock<IUnitOfWork> _unit;

    private UnitOfWorkBuilder()
    {
        if (_unit is null)
        {
            _unit = new Mock<IUnitOfWork>();
        }
    }

    public static UnitOfWorkBuilder Instance()
    {
        _instance = new UnitOfWorkBuilder();
        return _instance;
    }

    public IUnitOfWork Build()
    {
        return _unit.Object;
    }
}
