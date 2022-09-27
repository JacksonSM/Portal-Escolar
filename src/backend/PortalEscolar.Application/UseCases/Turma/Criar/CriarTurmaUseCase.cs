using AutoMapper;
using PortalEscolar.Application.Responses;
using PortalEscolar.Communication.Request.Diretor;
using PortalEscolar.Communication.Response;
using PortalEscolar.Domain.Interfaces.Repositories;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Professora;
using PortalEscolar.Domain.Interfaces.Repositories.Turma;
using PortalEscolar.Exceptions;
using PortalEscolar.Exceptions.ExceptionsBase;

namespace PortalEscolar.Application.UseCases.Turma.Criar;
public class CriarTurmaUseCase : ICriarTurmaUseCase
{
    private readonly ITurmaWriteOnlyRepository _turmaWriteRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IProfessoraReadOnlyRepository _professoraReadRepository;

    public CriarTurmaUseCase(
        ITurmaWriteOnlyRepository turmaWriteRepo,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IProfessoraReadOnlyRepository professoraReadRepository)
    {
        _turmaWriteRepo = turmaWriteRepo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _professoraReadRepository = professoraReadRepository;
    }

    public async Task<GenericResponseJson> ExecuteAsync(RequestCriarTurmaJson request)
    {
        await ValidarAsync(request);

        var entity = _mapper.Map<Domain.Entities.SalaAula.Turma>(request);

        await _turmaWriteRepo.AdicionarAsync(entity);
        await _unitOfWork.CommitAsync();

        return new GenericResponseJson { Mensagem = ResourceRespostasUseCases.TURMA_CRIADA_COM_SUCESSO };
    }

    private async Task ValidarAsync(RequestCriarTurmaJson request)
    {
        var validator = new CriarTurmaValidator();

        var validationResult = validator.Validate(request);

        var existeProfessora = await _professoraReadRepository.ExistePorIdAsync(request.ProfessoraId);

        if (!existeProfessora) validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("Diretor",
                ResourceMensagensDeErro.PROFESSORA_NAO_ENCONTRADA));

        if (!validationResult.IsValid)
        {
            var erros = validationResult.Errors.Select(c => c.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(erros);
        }
    }
}
