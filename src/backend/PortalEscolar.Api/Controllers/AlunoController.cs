using Microsoft.AspNetCore.Mvc;
using PortalEscolar.Api.Filters.Autorizacao;
using PortalEscolar.Application.UseCases.Aluno.Login;
using PortalEscolar.Application.UseCases.Aluno.ObterExercicio;
using PortalEscolar.Application.UseCases.Aluno.ObterListaExercicios;
using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Request.Aluno;
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
    [AutorizacaoPortalEscolar(new Papel[] { Papel.Aluno })]
    [ProducesResponseType(typeof(ExercicioParaResolverJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterExercicio(
        [FromServices] IObterExercicioUseCase useCase,
        string exercicioId)
    {
        var response = await useCase.ExecuteAsync(exercicioId);

        if (response is null)
            return NotFound();

        return Ok(response);
    }

    [HttpGet("obter-lista-exercicios")]
    [AutorizacaoPortalEscolar(new Papel[] { Papel.Aluno })]
    [ProducesResponseType(typeof(ExercicioParaResolverJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterListaExercicio(
        [FromServices] IObterListaExercicios useCase,
        [FromQuery] RequestObterListaExerciciosQuery request)
    {
        if (request.PaginaAtual is null && request.ExerciciosPorPagina.HasValue ||
           request.PaginaAtual.HasValue && request.ExerciciosPorPagina is null)
        {
            return BadRequest();
        }

        var response = await useCase.ExecuteAsync(request);

        if (response is null || response.Count == 0)
            return NoContent();

        return Ok(response);
    }
}
