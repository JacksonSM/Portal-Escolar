using Moq;
using PortalEscolar.Domain.Interfaces.Repositories.Turma;

namespace Utilities.Repositories.Turma;
public class TurmaWriteOnlyRepositoryBuilder
{
    private static TurmaWriteOnlyRepositoryBuilder _instance;
    private readonly Mock<ITurmaWriteOnlyRepository> _repository;

    private TurmaWriteOnlyRepositoryBuilder()
    {
        if (_repository is null)
        {
            _repository = new Mock<ITurmaWriteOnlyRepository>();
        }
    }

    public static TurmaWriteOnlyRepositoryBuilder Instance()
    {
        _instance = new TurmaWriteOnlyRepositoryBuilder();
        return _instance;
    }

    public ITurmaWriteOnlyRepository Build()
    {
        return _repository.Object;
    }
}
