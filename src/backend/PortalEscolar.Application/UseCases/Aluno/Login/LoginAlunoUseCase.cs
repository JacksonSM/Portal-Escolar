using PortalEscolar.Application.Services.Criptografia;
using PortalEscolar.Application.Services.Token;
using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Response;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Aluno;
using PortalEscolar.Exceptions.ExceptionsBase;

namespace PortalEscolar.Application.UseCases.Aluno.Login;
public class LoginAlunoUseCase : ILoginAlunoUseCase
{
    private readonly IAlunoReadOnlyRepository _repoAlunoRead;
    private readonly EncriptadorDeSenha _encriptadorDeSenha;
    private readonly TokenController _tokenController;

    public LoginAlunoUseCase(IAlunoReadOnlyRepository repoAlunoRead,
        EncriptadorDeSenha encriptadorDeSenha,
        TokenController tokenController)
    {
        _repoAlunoRead = repoAlunoRead;
        _encriptadorDeSenha = encriptadorDeSenha;
        _tokenController = tokenController;
    }

    public async Task<ResponseTokenJson> ExecuteAsync(RequestUsuarioLoginJson request)
    {
        var senhaCriptografada = _encriptadorDeSenha.Criptografar(request.Senha);

        var aluno = await _repoAlunoRead.ObterPorEmailSenhaAsync(request.Email, senhaCriptografada);

        if (aluno is null) throw new LoginInvalidoException();

        var token = _tokenController.GerarToken(aluno);

        return new ResponseTokenJson 
        {
            Nome = aluno.NomeCompleto,
            Token = token
        };
    }
}
