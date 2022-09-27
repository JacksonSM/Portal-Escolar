using AutoMapper;
using PortalEscolar.Communication.Response.Aluno.Exercicio;
using PortalEscolar.Domain.Entities.SalaAula.ProfessoraContext.AtividadesParaResolver.Execicio;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Exercicio;
using PortalEscolar.Exceptions;
using PortalEscolar.Exceptions.ExceptionsBase;

namespace PortalEscolar.Application.UseCases.Aluno.ObterExercicio;
public class ObterExercicioUseCase : IObterExercicioUseCase
{
    private readonly IExercicioReadOnlyRepository _exercicioReadRepo;
    private readonly IMapper _mapper;

    public ObterExercicioUseCase(IExercicioReadOnlyRepository exercicioReadRepo, IMapper mapper)
    {
        _exercicioReadRepo = exercicioReadRepo;
        _mapper = mapper;
    }

    public async Task<ExercicioParaResolverJson> ExecuteAsync(string exercicioId)
    {
        var exercicio = await _exercicioReadRepo.ObterPorId(exercicioId);

        Validar(exercicio);

        return _mapper.Map<ExercicioParaResolverJson>(exercicio);
    }

    private void Validar(ExercicioParaResolver exercicio)
    {
        if (exercicio is null)
            throw new PortalEscolarException(ResourceMensagensDeErro.EXERCICIO_INEXISTENTE);
    }
}
