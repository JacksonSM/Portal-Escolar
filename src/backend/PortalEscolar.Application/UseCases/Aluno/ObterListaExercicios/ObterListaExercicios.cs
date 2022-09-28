using AutoMapper;
using PortalEscolar.Communication.Request.Aluno;
using PortalEscolar.Communication.Response.Aluno.ObterListaExercicios;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Exercicio;

namespace PortalEscolar.Application.UseCases.Aluno.ObterListaExercicios;
public class ObterListaExercicios : IObterListaExercicios
{
    private readonly IExercicioReadOnlyRepository _exercicioReadRepo;
    private IMapper _mapper;

    public ObterListaExercicios(IExercicioReadOnlyRepository exercicioReadRepo, IMapper mapper)
    {
        _exercicioReadRepo = exercicioReadRepo;
        _mapper = mapper;
    }

    public async Task<List<ResponseExercicioParaResolverJson>> ExecuteAsync(
        RequestObterListaExerciciosQuery requestQuery)
    {
        var query = _mapper.Map<ObterListaExerciciosQuery>(requestQuery);

        var listaExercicio = await _exercicioReadRepo.ObterListaExercicios(query);

        return _mapper.Map<List<ResponseExercicioParaResolverJson>>(listaExercicio);
    }
}
