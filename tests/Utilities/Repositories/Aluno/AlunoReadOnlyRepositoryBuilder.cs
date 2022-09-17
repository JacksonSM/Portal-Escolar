using Moq;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Aluno;

namespace Utilities.Repositories.Diretor;
public class AlunoReadOnlyRepositoryBuilder
{

    private static AlunoReadOnlyRepositoryBuilder _instance;
    private readonly Mock<IAlunoReadOnlyRepository> _repository;

    private AlunoReadOnlyRepositoryBuilder()
    {
        if (_repository is null)
        {
            _repository = new Mock<IAlunoReadOnlyRepository>();
        }
    }

    public static AlunoReadOnlyRepositoryBuilder Instance()
    {
        _instance = new AlunoReadOnlyRepositoryBuilder();
        return _instance;
    }
    public AlunoReadOnlyRepositoryBuilder ExisteEmail(string email)
    {
        if (!string.IsNullOrEmpty(email))
            _repository.Setup(i => i.ExisteEmailAsync(email)).ReturnsAsync(true);

        return this;
    }
    public IAlunoReadOnlyRepository Build()
    {
        return _repository.Object;
    }

}
