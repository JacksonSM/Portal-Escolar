using Microsoft.AspNetCore.Mvc;
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
        var resposta = await useCase.ExecutarAsync(request);

        return Created(string.Empty ,resposta);
    }
}
