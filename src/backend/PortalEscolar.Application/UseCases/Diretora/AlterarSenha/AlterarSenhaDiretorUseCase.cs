using PortalEscolar.Application.Responses;
using PortalEscolar.Application.Services.Criptografia;
using PortalEscolar.Application.Services.Token;
using PortalEscolar.Application.Services.UsuarioLogado;
using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Response;
using PortalEscolar.Domain.Entities.Diretoria;
using PortalEscolar.Domain.Interfaces.Repositories;
using PortalEscolar.Domain.Interfaces.Repositories.Diretoria.Diretor;
using PortalEscolar.Exceptions;
using PortalEscolar.Exceptions.ExceptionsBase;

namespace PortalEscolar.Application.UseCases.Diretora.AlterarSenha;
public class AlterarSenhaDiretorUseCase : IAlterarSenhaDiretorUseCase
{
    private readonly IDiretorWriteOnlyRepository _repoDiretorWrite;
    private readonly EncriptadorDeSenha _encriptadorDeSenha;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IUnitOfWork _unitOfWork;

    public AlterarSenhaDiretorUseCase(
        IDiretorWriteOnlyRepository repoDiretorWrite,
        EncriptadorDeSenha encriptadorDeSenha,
        IUsuarioLogado usuarioLogado,
        IUnitOfWork unitOfWork)
    {
        _repoDiretorWrite = repoDiretorWrite;
        _encriptadorDeSenha = encriptadorDeSenha;
        _usuarioLogado = usuarioLogado;
        _unitOfWork = unitOfWork;
    }

    public async Task<GenericResponseJson> ExecuteAsync(RequestAlterarSenhaUsuarioJson request)
    {
        var diretorLogado = await _usuarioLogado.ObterDiretor();

        Validar(request, diretorLogado);

        var senhaCriptografada = _encriptadorDeSenha.Criptografar(request.SenhaNova);

        diretorLogado.Senha = senhaCriptografada;

        _repoDiretorWrite.Atualizar(diretorLogado);
        await _unitOfWork.CommitAsync();

        return new GenericResponseJson { Mensagem = ResourceRespostasUseCases.SENHA_ALTERADA_COM_SUCESSO };
    }

    private void Validar(RequestAlterarSenhaUsuarioJson request, Diretor diretorLogado)
    {
        var validator = new AlterarSenhaDiretorValidator();

        var validationResult = validator.Validate(request);

        var senhaCriptografada = _encriptadorDeSenha.Criptografar(request.SenhaAtual);

        if (!diretorLogado.Senha.Equals(senhaCriptografada))
        {
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("SenhaAtual", ResourceMensagensDeErro.SENHAATUAL_INVALIDA));
        }

        if (!validationResult.IsValid)
        {
            var mensagensDeErro = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(mensagensDeErro);
        }
    }
}
