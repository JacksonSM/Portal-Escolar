using Moq;
using PortalEscolar.Domain.Interfaces.Repositories.Diretoria.Diretor;
using PortalEscolar.Domain.Interfaces.Repositories.Papeis;

namespace Utilities.Repositories.Diretor;
public class PapelWriteOnlyRepositoryBuilder
{

    private static PapelWriteOnlyRepositoryBuilder _instance;
    private readonly Mock<IPapelWriteOnlyRepository> _repository;

    private PapelWriteOnlyRepositoryBuilder()
    {
        if (_repository is null)
        {
            _repository = new Mock<IPapelWriteOnlyRepository>();
        }
    }

    public static PapelWriteOnlyRepositoryBuilder Instance()
    {
        _instance = new PapelWriteOnlyRepositoryBuilder();
        return _instance;
    }

    public IPapelWriteOnlyRepository Build()
    {
        return _repository.Object;
    }

}
