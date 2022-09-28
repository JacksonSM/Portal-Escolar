using AutoMapper;
using PortalEscolar.Application.Responses;
using PortalEscolar.Application.Services.UsuarioLogado;
using PortalEscolar.Communication.Request.Exercicio;
using PortalEscolar.Communication.Response;
using PortalEscolar.Domain.Entities.SalaAula.ProfessoraContext.AtividadesParaResolver.Execicio;
using PortalEscolar.Domain.Interfaces.Repositories;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Exercicio;
using PortalEscolar.Domain.Interfaces.Repositories.Turma;
using PortalEscolar.Exceptions;
using PortalEscolar.Exceptions.ExceptionsBase;

namespace PortalEscolar.Application.UseCases.Professora.EnviarExercicio;
public class EnviarExercicioUseCase : IEnviarExercicioUseCase
{
    private readonly IExercicioWriteOnlyRepository _exercicioWriteRepo;
    private readonly ITurmaReadOnlyRepository _turmaReadRepo;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IMapper _mapper;

    public EnviarExercicioUseCase(
        IExercicioWriteOnlyRepository exercicioWriteRepo,
        ITurmaReadOnlyRepository turmaReadRepo,
        IUnitOfWork unitOfWork,
        IUsuarioLogado usuarioLogado, 
        IMapper mapper)
    {
        _exercicioWriteRepo = exercicioWriteRepo;
        _turmaReadRepo = turmaReadRepo;
        _usuarioLogado = usuarioLogado;
        _mapper = mapper;
    }

    public async Task<GenericResponseJson> ExecuteAsync(RequestEnviarExercicioJson request)
    {
        var turma = await _turmaReadRepo.ObterTurmaPorIdAsync(request.TurmaId);

        Validar(request,turma);

        var professoraLogado = await _usuarioLogado.ObterProfessora();

        var entidade = _mapper.Map<ExercicioParaResolver>(request);

        entidade.ProfessoraId = professoraLogado.Id;
        entidade.NomeProfessora = professoraLogado.NomeCompleto;
        entidade.NomeTurma = turma.NomeTurma;

        await _exercicioWriteRepo.AdicionarAsync(entidade);
        

        return new GenericResponseJson { Mensagem = ResourceRespostasUseCases.ENVIAR_EXERCICIO_ENVIADO_COM_SUCESSO };
    }

    private void Validar(RequestEnviarExercicioJson request, Domain.Entities.SalaAula.Turma turma)
    {
        var validator = new EnviarExercicioValidador();
        var validationResult = validator.Validate(request);

        if (turma is null)
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
                request.TurmaId.ToString(),ResourceMensagensDeErro.TURMA_INEXISTENTE));


        if (!validationResult.IsValid)
        {
            var erros = validationResult.Errors.Select(erro => erro.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(erros);
        }

    }
}
