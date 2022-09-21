using AutoMapper;
using PortalEscolar.Application.Responses;
using PortalEscolar.Application.UseCases.Aluno.Registrar;
using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Request.Matricula;
using PortalEscolar.Communication.Response;
using PortalEscolar.Domain.Entities.Diretoria.Matricula;
using PortalEscolar.Domain.Interfaces.Repositories;
using PortalEscolar.Domain.Interfaces.Repositories.Diretoria.Matricula;
using PortalEscolar.Domain.Interfaces.Repositories.Turma;
using PortalEscolar.Exceptions;
using PortalEscolar.Exceptions.ExceptionsBase;

namespace PortalEscolar.Application.UseCases.Diretora.Matricular;
public class MatricularAlunoUseCase : IMatricularAlunoUseCase
{
    private readonly ITurmaReadOnlyRepository _turmaReadRepo;
    private readonly IMatriculaWriteOnlyRepository _matriculaWriteRepo;
    private readonly IRegistrarAlunoUseCase _registrarAlunoUseCase;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MatricularAlunoUseCase(
        ITurmaReadOnlyRepository turmaReadRepo, 
        IMatriculaWriteOnlyRepository matriculaWriteRepo,
        IRegistrarAlunoUseCase registrarAlunoUseCase, 
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _turmaReadRepo = turmaReadRepo;
        _matriculaWriteRepo = matriculaWriteRepo;
        _registrarAlunoUseCase = registrarAlunoUseCase;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GenericResponseJson> ExecuteAsync(RequestMatricularAlunoJson request)
    {
        await ValidarAsync(request);

        var requestRegistrarAluno = _mapper.Map<RequestRegistrarAlunoJson>(request.Aluno);
        
        var entidadeMatricula = _mapper.Map<Matricula>(request);

        var responseRegistrarAluno = await _registrarAlunoUseCase.ExecuteAsync(requestRegistrarAluno);

        entidadeMatricula.NomeCompletoAluno = requestRegistrarAluno.NomeCompleto;
        entidadeMatricula.AlunoId = responseRegistrarAluno.Id;

        await _matriculaWriteRepo.AdicionarAsync(entidadeMatricula);
        await _unitOfWork.CommitAsync();

        return new GenericResponseJson { Mensagem = ResourceRespostasUseCases.MATRICULA_REALIZADA_COM_SUCESSO };
    }

    private async Task ValidarAsync(RequestMatricularAlunoJson request)
    {
        var validator = new MatricularAlunoValidator();

        var validationResult = validator.Validate(request);

        var existeTurma = await _turmaReadRepo.ExistePorIdAsync(request.TurmaId);

        if (!existeTurma)    
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(request.TurmaId.ToString(),
                ResourceMensagensDeErro.TURMA_INEXISTENTE));
        
        if (!validationResult.IsValid)
        {
            var mensagensDeErro = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(mensagensDeErro);
        }
    }
}
