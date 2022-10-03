using AutoMapper;
using PortalEscolar.Application.Services.UsuarioLogado;
using PortalEscolar.Communication.Request.Aluno.EnviarExercicioResolvido;
using PortalEscolar.Communication.Response.Aluno.ExercicioResolvido;
using PortalEscolar.Domain.Entities.SalaAula.AlunoContext.AtividadesResolvido.Exercicio;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Exercicio;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.ExercicioResolvido;
using PortalEscolar.Exceptions;
using PortalEscolar.Exceptions.ExceptionsBase;

namespace PortalEscolar.Application.UseCases.Aluno.EnviarExercicioResolvido;
public class EnviarExercicioResolvidoUseCase : IEnviarExercicioResolvidoUseCase
{   
    private readonly IExercicioResolvidoWriteOnlyRepository _exercicioResolvidoWriteRepo;
    private readonly IExercicioResolvidoReadOnlyRepository _exercicioResolvidoReadRepo;
    private readonly IExercicioReadOnlyRepository _exercicioReadRepo;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IMapper _mapper;

    public EnviarExercicioResolvidoUseCase(
        IExercicioResolvidoWriteOnlyRepository exercicioResolvidoWriteRepo,
        IExercicioReadOnlyRepository exercicioReadRepo,
        IUsuarioLogado usuarioLogado,
        IMapper mapper,
        IExercicioResolvidoReadOnlyRepository exercicioResolvidoReadRepo)
    {
        _exercicioResolvidoWriteRepo = exercicioResolvidoWriteRepo;
        _exercicioReadRepo = exercicioReadRepo;
        _usuarioLogado = usuarioLogado;
        _mapper = mapper;
        _exercicioResolvidoReadRepo = exercicioResolvidoReadRepo;
    }

    public async Task<ResponseExercicioResolvidoJson> ExecuteAsync(RequestEnviarExercicioResolvidoJson request)
    {
        var alunoLogado = await _usuarioLogado.ObterAluno();

        var exercicioParaGabarito = await _exercicioReadRepo.ObterPorId(request.ExercicioParaResolverId);
        if (exercicioParaGabarito is null)
            throw new ErrosDeValidacaoException
                (new List<string> { ResourceMensagensDeErro.EXERCICIO_INEXISTENTE });

        await ValidarAsync(request, alunoLogado, exercicioParaGabarito.PrazoEntrega);



        var exercicioResolvido = _mapper.Map<ExercicioResolvido>(request);

        exercicioResolvido.TurmaId = exercicioParaGabarito.TurmaId;
        exercicioResolvido.AlunoId = alunoLogado.Id;
        exercicioResolvido.ProfessoraId = exercicioParaGabarito.ProfessoraId;
        exercicioResolvido.Nome = exercicioParaGabarito.Nome;
        exercicioResolvido.Disciplina = exercicioParaGabarito.Disciplina;

        exercicioResolvido.AtribuirNota(exercicioParaGabarito);

        await _exercicioResolvidoWriteRepo.AdicionarAsync(exercicioResolvido);

        return _mapper.Map<ResponseExercicioResolvidoJson>(exercicioResolvido);       
    }

    private async Task ValidarAsync(RequestEnviarExercicioResolvidoJson request,
        Domain.Entities.SalaAula.AlunoContext.Aluno alunoLogado,
        DateTime prazoEntrega)
    {
        var validator = new EnviarExercicioResolvidoValidator();

        var resultadoValidacao = validator.Validate(request);


        if(DateTime.Compare(prazoEntrega.ToUniversalTime(), DateTime.UtcNow) < 0)
            resultadoValidacao.Errors.Add(new FluentValidation.Results.ValidationFailure("Prazo expirado.",
                ResourceMensagensDeErro.ALUNO_ENVIAR_EXERCICIORESOLVIDO_PRAZOEXPIRADO));

        if (alunoLogado is null)
            resultadoValidacao.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(alunoLogado),
                ResourceMensagensDeErro.ALUNO_INEXISTENTE));

        var exercicioEstaResolvido = await _exercicioResolvidoReadRepo
            .ExercicioEstaResolvido(request.ExercicioParaResolverId,alunoLogado.Id);

        if (exercicioEstaResolvido)
            resultadoValidacao.Errors.Add(new FluentValidation.Results.ValidationFailure("Exercicio Já resolvido.",
                ResourceMensagensDeErro.ALUNO_ENVIAR_EXERCICIORESOLVIDO_EXERCICIOJAFOIRESOLVIDO));

        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors.Select(c => c.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(erros);
        }
    }
}
