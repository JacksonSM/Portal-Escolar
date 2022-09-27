using Microsoft.AspNetCore.Mvc;
using PortalEscolar.Api.Filters.Autorizacao;
using PortalEscolar.Application.UseCases.Professora.EnviarExercicio;
using PortalEscolar.Application.UseCases.Professora.Login;
using PortalEscolar.Application.UseCases.Professora.Registrar;
using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Request.Exercicio;
using PortalEscolar.Communication.Request.Professora;
using PortalEscolar.Communication.Response;
using PortalEscolar.Domain.Enum;

namespace PortalEscolar.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProfessoraController : ControllerBase
{
    [HttpPost]
    [AutorizacaoPortalEscolar(new Papel[] { Papel.Diretor })]
    [ProducesResponseType(typeof(GenericResponseJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Registrar(
        [FromServices] IRegistrarProfessoraUseCase useCase,
        [FromBody] RequestRegistrarProfessoraJson request)
    {
        var response = await useCase.ExecuteAsync(request);

        return Created(string.Empty, response);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ResponseTokenJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(
    [FromServices] ILoginProfessoraUseCase useCase,
    [FromBody] RequestUsuarioLoginJson request)
    {
        var response = await useCase.ExecuteAsync(request);

        return Ok(response);
    }

    [HttpPost("enviar-exercicio")]
    [AutorizacaoPortalEscolar(new Papel[] { Papel.Professora })]
    [ProducesResponseType(typeof(ResponseTokenJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> EnviarExercicio(
        [FromServices] IEnviarExercicioUseCase useCase,
        [FromBody] RequestEnviarExercicioJson request)
    {
        var response = await useCase.ExecuteAsync(request);

        return Ok(response);
    }

}
