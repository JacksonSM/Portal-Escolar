using AutoMapper;
using PortalEscolar.Application.Services.UsuarioLogado;
using PortalEscolar.Communication.Response.Aluno.Exercicio;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Exercicio;
using PortalEscolar.Exceptions;
using PortalEscolar.Exceptions.ExceptionsBase;

namespace PortalEscolar.Application.UseCases.Aluno.ObterExercicio;
public class ObterExercicioUseCase : IObterExercicioUseCase
{
    private readonly IExercicioReadOnlyRepository _exercicioReadRepo;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IMapper _mapper;

    public ObterExercicioUseCase(IExercicioReadOnlyRepository exercicioReadRepo, IMapper mapper, IUsuarioLogado usuarioLogado)
    {
        _exercicioReadRepo = exercicioReadRepo;
        _mapper = mapper;
        _usuarioLogado = usuarioLogado;
    }

    public async Task<ResponseExercicioParaResolverJson> ExecuteAsync(string exercicioId)
    {
        var alunoLogado = await _usuarioLogado.ObterAluno();

        Validar(alunoLogado);

        var exercicio = await _exercicioReadRepo.ObterPorId(exercicioId, alunoLogado.TurmaId);

        return _mapper.Map<ResponseExercicioParaResolverJson>(exercicio);
    }

    private static void Validar(Domain.Entities.SalaAula.AlunoContext.Aluno aluno)
    {
        if (aluno is null)
        {
            throw new ErrosDeValidacaoException(new List<string> { ResourceMensagensDeErro.ALUNO_INEXISTENTE });
        }
    }
}
