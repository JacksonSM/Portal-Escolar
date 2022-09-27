using Microsoft.AspNetCore.Mvc;
using PortalEscolar.Api.Filters.Autorizacao;
using PortalEscolar.Application.UseCases.Aluno.Login;
using PortalEscolar.Application.UseCases.Aluno.ObterExercicio;
using PortalEscolar.Application.UseCases.Aluno.Registrar;
using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Response;
using PortalEscolar.Communication.Response.Aluno.Exercicio;
using PortalEscolar.Domain.Enum;

namespace PortalEscolar.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AlunoController : ControllerBase
{
    [HttpPost("login")]
    [ProducesResponseType(typeof(ResponseTokenJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(
    [FromServices] ILoginAlunoUseCase useCase,
    [FromBody] RequestUsuarioLoginJson request)
    {
        var response = await useCase.ExecuteAsync(request);

        return Ok(response);
    }
    [HttpGet("{exercicioId:length(24)}")]
    [AutorizacaoPortalEscolar(new Papel[] { Papel.Diretor, Papel.Aluno, Papel.Professora })]
    [ProducesResponseType(typeof(ExercicioParaResolverJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Registrar(
[       FromServices] IObterExercicioUseCase useCase,
        string exercicioId)
    {
        var response = await useCase.ExecuteAsync(exercicioId);

        return Ok(response);
    }
}
