using AutoMapper;
using PortalEscolar.Application.Services.Criptografia;
using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Response;
using PortalEscolar.Domain.Interfaces.Repositories;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Aluno;
using PortalEscolar.Exceptions;
using PortalEscolar.Exceptions.ExceptionsBase;

namespace PortalEscolar.Application.UseCases.Aluno.Registrar;
public class RegistrarAlunoUseCase : IRegistrarAlunoUseCase
{
    private readonly IAlunoReadOnlyRepository _repoAlunoRead;
    private readonly IAlunoWriteOnlyRepository _repoAlunoWrite;
    private readonly IMapper _mapper;
    private readonly EncriptadorDeSenha _encriptadorDeSenha;
    private readonly IUnitOfWork _unitOfWork;

    public RegistrarAlunoUseCase(
        IAlunoReadOnlyRepository repoAlunoRead,
        IAlunoWriteOnlyRepository repoAlunoWrite,
        IMapper mapper, 
        EncriptadorDeSenha encriptadorDeSenha,
        IUnitOfWork unitOfWork)
    {
        _repoAlunoRead = repoAlunoRead;
        _repoAlunoWrite = repoAlunoWrite;
        _mapper = mapper;
        _encriptadorDeSenha = encriptadorDeSenha;
        _unitOfWork = unitOfWork;
    }

    public async Task<GenericResponseJson> ExecuteAsync(RequestRegistrarAlunoJson request)
    {
        await ValidarAsync(request);

        var senhaCriptografada = _encriptadorDeSenha.Criptografar(request.Senha);

        var entidade = _mapper.Map<Domain.Entities.SalaAula.Aluno>(request);

        entidade.Senha = senhaCriptografada;

        await _repoAlunoWrite.AdicionarAsync(entidade);
        await _unitOfWork.CommitAsync();

        return new GenericResponseJson { Mensagem = ResourceMensagensDeErro.REGISTRAR_ALUNO_SUCESSO };
    }

    private async Task ValidarAsync(RequestRegistrarAlunoJson request)
    {
        var validator = new RegistrarAlunoValidator();

        var validationResult = validator.Validate(request);

        var existeEmail = await _repoAlunoRead.ExisteEmailAsync(request.Email);
        if (existeEmail) validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(request.Email,
                ResourceMensagensDeErro.EMAIL_EXISTENTE));
    

        if (!validationResult.IsValid)
        {
            var validationErros = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(validationErros);
        }
    }
}
