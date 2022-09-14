using Moq;
using PortalEscolar.Domain.Interfaces.Repositories.Diretoria.Diretor;

namespace Utilities.Repositories.Diretor;
public class DiretorReadOnlyRepositoryBuilder
{

    private static DiretorReadOnlyRepositoryBuilder _instance;
    private readonly Mock<IDiretorReadOnlyRepository> _repository;

    private DiretorReadOnlyRepositoryBuilder()
    {
        if (_repository is null)
        {
            _repository = new Mock<IDiretorReadOnlyRepository>();
        }
    }

    public static DiretorReadOnlyRepositoryBuilder Instance()
    {
        _instance = new DiretorReadOnlyRepositoryBuilder();
        return _instance;
    }
    public DiretorReadOnlyRepositoryBuilder ExisteEmail(string email)
    {
        if (!string.IsNullOrEmpty(email))
            _repository.Setup(i => i.ExisteEmailAsync(email)).ReturnsAsync(true);

        return this;
    }
    public DiretorReadOnlyRepositoryBuilder ObterPorEmailSenha(PortalEscolar.Domain.Entities.Diretoria.Diretor diretor)
    {

        _repository.Setup(i => i.ObterPorEmailSenhaAsync(diretor.Email, diretor.Senha)).ReturnsAsync(diretor);

        return this;
    }

    public IDiretorReadOnlyRepository Build()
    {
        return _repository.Object;
    }

}
