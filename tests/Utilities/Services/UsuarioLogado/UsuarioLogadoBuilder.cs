using Moq;
using PortalEscolar.Application.Services.UsuarioLogado;
using PortalEscolar.Domain.Entities.Diretoria;

namespace Utilities.Services.UsuarioLogado;

public class UsuarioLogadoBuilder
{
    private static UsuarioLogadoBuilder _instance;
    private readonly Mock<IUsuarioLogado> _repositorio;

    private UsuarioLogadoBuilder()
    {
        if (_repositorio is null)
        {
            _repositorio = new Mock<IUsuarioLogado>();
        }
    }

    public static UsuarioLogadoBuilder Instance()
    {
        _instance = new UsuarioLogadoBuilder();
        return _instance;
    }

    public UsuarioLogadoBuilder RecuperarDiretor(Diretor diretor)
    {
        _repositorio.Setup(c => c.ObterDiretor()).ReturnsAsync(diretor);

        return this;
    }

    public IUsuarioLogado Build()
    {
        return _repositorio.Object;
    }
}
