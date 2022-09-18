using Moq;
using PortalEscolar.Domain.Interfaces.Repositories.Diretoria.Diretor;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Aluno;

namespace Utilities.Repositories.Aluno;
public class AlunoWriteOnlyRepositoryBuilder
{

    private static AlunoWriteOnlyRepositoryBuilder _instance;
    private readonly Mock<IAlunoWriteOnlyRepository> _repository;

    private AlunoWriteOnlyRepositoryBuilder()
    {
        if (_repository is null)
        {
            _repository = new Mock<IAlunoWriteOnlyRepository>();
        }
    }

    public static AlunoWriteOnlyRepositoryBuilder Instance()
    {
        _instance = new AlunoWriteOnlyRepositoryBuilder();
        return _instance;
    }

    public IAlunoWriteOnlyRepository Build()
    {
        return _repository.Object;
    }

}
