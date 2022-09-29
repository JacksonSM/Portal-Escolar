using AutoMapper;
using PortalEscolar.Application.Services.UsuarioLogado;
using PortalEscolar.Communication.Request.Aluno;
using PortalEscolar.Communication.Response.Aluno.Exercicio;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Exercicio;
using PortalEscolar.Exceptions;
using PortalEscolar.Exceptions.ExceptionsBase;

namespace PortalEscolar.Application.UseCases.Aluno.ObterListaExercicios;
public class ObterListaExercicios : IObterListaExercicios
{
    private readonly IExercicioReadOnlyRepository _exercicioReadRepo;
    private readonly IUsuarioLogado _usuarioLogado;
    private IMapper _mapper;

    public ObterListaExercicios(IExercicioReadOnlyRepository exercicioReadRepo, IMapper mapper,
        IUsuarioLogado usuarioLogado)
    {
        _exercicioReadRepo = exercicioReadRepo;
        _mapper = mapper;
        _usuarioLogado = usuarioLogado;
    }

    public async Task<List<ResponseExercicioParaResolverJson>> ExecuteAsync(
        RequestObterListaExerciciosQuery requestQuery)
    {
        var alunoLogado = await _usuarioLogado.ObterAluno();

        Validar(alunoLogado);

        var query = _mapper.Map<ObterListaExerciciosQuery>(requestQuery);
        query.TurmaId = alunoLogado.TurmaId;

        var listaExercicio = await _exercicioReadRepo.ObterListaExercicios(query);

        return _mapper.Map<List<ResponseExercicioParaResolverJson>>(listaExercicio);
    }

    private static void Validar(Domain.Entities.SalaAula.AlunoContext.Aluno aluno)
    {
        if(aluno is null)
        {
            throw new ErrosDeValidacaoException(new List<string> { ResourceMensagensDeErro.ALUNO_INEXISTENTE });
        }
    }
}
