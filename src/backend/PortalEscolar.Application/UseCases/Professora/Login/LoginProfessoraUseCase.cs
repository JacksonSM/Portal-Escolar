using PortalEscolar.Application.Services.Criptografia;
using PortalEscolar.Application.Services.Token;
using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Response;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Professora;
using PortalEscolar.Exceptions.ExceptionsBase;

namespace PortalEscolar.Application.UseCases.Professora.Login;
public class LoginProfessoraUseCase : ILoginProfessoraUseCase
{
    private readonly IProfessoraReadOnlyRepository _professoraRead;
    private readonly EncriptadorDeSenha _encriptadorDeSenha;
    private readonly TokenController _tokenController;

    public LoginProfessoraUseCase(
        IProfessoraReadOnlyRepository professoraRead,
        EncriptadorDeSenha encriptadorDeSenha,
        TokenController tokenController)
    {
        _professoraRead = professoraRead;
        _encriptadorDeSenha = encriptadorDeSenha;
        _tokenController = tokenController;
    }

    public async Task<ResponseTokenJson> ExecuteAsync(RequestUsuarioLoginJson request)
    {
        var senhaCriptografada = _encriptadorDeSenha.Criptografar(request.Senha);

        var professora = await _professoraRead.ObterPorEmailESenhaAsync(request.Email, senhaCriptografada);

        if (professora is null) throw new LoginInvalidoException();

        var token = _tokenController.GerarToken(professora.Email);

        var response = new ResponseTokenJson { Nome = professora.NomeCompleto, Token = token };
        return response;
    }
}
