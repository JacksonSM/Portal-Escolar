using PortalEscolar.Application.Services.Criptografia;
using PortalEscolar.Application.Services.Token;
using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Response;
using PortalEscolar.Domain.Interfaces.Repositories.Diretoria.Diretor;
using PortalEscolar.Exceptions.ExceptionsBase;

namespace PortalEscolar.Application.UseCases.Diretora.FazerLogin;
public class FazerLoginDiretorUseCase : IFazerLoginDiretorUseCase
{
    private readonly IDiretorReadOnlyRepository _repoRead;
    private readonly EncriptadorDeSenha _encriptadorDeSenha;
    private readonly TokenController _tokenController;

    public FazerLoginDiretorUseCase(IDiretorReadOnlyRepository repoRead, EncriptadorDeSenha encriptadorDeSenha, TokenController tokenController)
    {
        _repoRead = repoRead;
        _encriptadorDeSenha = encriptadorDeSenha;
        _tokenController = tokenController;
    }
    public async Task<ResponseTokenJson> ExecuteAsync(RequestUsuarioLoginJson request)
    {
        var senhaCriptografada = _encriptadorDeSenha.Criptografar(request.Senha);

        var diretor = await _repoRead.ObterPorEmailSenhaAsync(request.Email, senhaCriptografada);

        if (diretor is null) throw new LoginInvalidoException();

        return new ResponseTokenJson
        {
            Nome = diretor.NomeCompleto,
            Token = _tokenController.GerarToken(diretor.Email)
        };

    }
}
