using AutoMapper;
using PortalEscolar.Application.Services.Criptografia;
using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Response;
using PortalEscolar.Domain.Interfaces.Repositories;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Professora;
using PortalEscolar.Exceptions;
using PortalEscolar.Exceptions.ExceptionsBase;

namespace PortalEscolar.Application.UseCases.Professora.Registrar;
public class RegistrarProfessoraUseCase : IRegistrarProfessoraUseCase
{
    private readonly IProfessoraReadOnlyRepository _professoraRead;
    private readonly IProfessoraWriteOnlyRepository _professoraWrite;
    private readonly IMapper _mapper;
    private readonly EncriptadorDeSenha _encriptadorDeSenha;
    private readonly IUnitOfWork _unitOfWork;

    private const string _nomePapel = "Professora";

    public RegistrarProfessoraUseCase(
        IProfessoraReadOnlyRepository professoraRead,
        IProfessoraWriteOnlyRepository professoraWrite,
        IMapper mapper,
        EncriptadorDeSenha encriptadorDeSenha,
        IUnitOfWork unitOfWork)
    {
        _professoraRead = professoraRead;
        _professoraWrite = professoraWrite;
        _mapper = mapper;
        _encriptadorDeSenha = encriptadorDeSenha;
        _unitOfWork = unitOfWork;
    }

    public async Task<GenericResponseJson> ExecuteAsync(RequestRegistrarProfessoraJson request)
    {
        await Validar(request);

        var entity = _mapper.Map<Domain.Entities.SalaAula.Professora>(request);

        var senhaCriptografada = _encriptadorDeSenha.Criptografar(entity.Senha);

        entity.Senha = senhaCriptografada;

        await _professoraWrite.AddAsync(entity);

        

        await _unitOfWork.CommitAsync();

        return new GenericResponseJson { Mensagem = ResourceMensagensDeErro.REGISTRAR_PROFESSORA_SUCESSO };
    }

    private async Task Validar(RequestRegistrarProfessoraJson request)
    {
        

        var validator = new RegistrarProfessoraValidator();
        var validationResult = validator.Validate(request);


        var existeEmail = await _professoraRead.ExisteEmailAsync(request.Email);
        if (existeEmail) validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(request.Email,
                ResourceMensagensDeErro.EMAIL_EXISTENTE));


        if (!validationResult.IsValid)
        {
            var erros = validationResult.Errors.Select(c => c.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(erros);
        }
    }
}
