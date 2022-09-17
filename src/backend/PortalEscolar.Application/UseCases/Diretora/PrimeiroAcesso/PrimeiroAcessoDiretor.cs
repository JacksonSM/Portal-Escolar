using PortalEscolar.Application.Services.Criptografia;
using PortalEscolar.Application.Services.Token;
using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Response;
using PortalEscolar.Domain.Interfaces.Repositories;
using PortalEscolar.Domain.Interfaces.Repositories.Diretoria.Diretor;
using PortalEscolar.Exceptions.ExceptionsBase;

namespace PortalEscolar.Application.UseCases.Diretora.PrimeiroAcesso;
public class PrimeiroAcessoDiretor : IPrimeiroAcessoDiretor
{
    private readonly IDiretorReadOnlyRepository _repoRead;
    private readonly IDiretorWriteOnlyRepository _repoWrite;
    private readonly EncriptadorDeSenha _encriptadorDeSenha;
    private readonly TokenController _tokenController;
    private readonly IUnitOfWork _unitOfWork;

    public PrimeiroAcessoDiretor(
        IDiretorReadOnlyRepository repoRead,
        EncriptadorDeSenha encriptadorDeSenha,
        TokenController tokenController,
        IDiretorWriteOnlyRepository repoWrite,
        IUnitOfWork unitOfWork)
    {
        _repoRead = repoRead;
        _encriptadorDeSenha = encriptadorDeSenha;
        _tokenController = tokenController;
        _repoWrite = repoWrite;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseTokenJson> ExecuteAsync(RequestUsuarioLoginJson request)
    {
        var diretor = await _repoRead.ObterPorEmailSenhaAsync(request.Email,request.Senha);

        if (diretor is null) throw new LoginInvalidoException();

        diretor.Senha = _encriptadorDeSenha.Criptografar(request.Senha);

        _repoWrite.Atualizar(diretor);
        await _unitOfWork.CommitAsync();

        var token = _tokenController.GerarToken(diretor);

        return new ResponseTokenJson { Nome = diretor.NomeCompleto, Token = token };
    }
}
