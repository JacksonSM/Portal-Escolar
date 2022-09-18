using Microsoft.AspNetCore.Mvc;
using PortalEscolar.Application.UseCases.Diretora.Login;
using PortalEscolar.Application.UseCases.Diretora.PrimeiroAcesso;
using PortalEscolar.Application.UseCases.Diretora.Registrar;
using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Response;

namespace PortalEscolar.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DiretorController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseTokenJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Registrar(
        [FromServices] IRegistrarDiretorUseCase useCase,
        [FromBody] RequestRegistrarDiretorJson request )
    {
        var resposta = await useCase.ExecuteAsync(request);

        return Created(string.Empty ,resposta);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ResponseTokenJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(
        [FromServices] ILoginDiretorUseCase useCase,
        [FromBody] RequestUsuarioLoginJson request)
    {
        var response = await useCase.ExecuteAsync(request);

        return Ok(response);
    }
    [HttpPost("primeiroacesso")]
    [ProducesResponseType(typeof(ResponseTokenJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> PrimeiroAcesso(
        [FromServices] IPrimeiroAcessoDiretor useCase,
        [FromBody] RequestUsuarioLoginJson request)
    {
        var response = await useCase.ExecuteAsync(request);

        return Ok(response);
    }
}
