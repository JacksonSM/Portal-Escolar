using Microsoft.AspNetCore.Mvc;
using PortalEscolar.Application.UseCases.Professora.Registrar;
using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Response;

namespace PortalEscolar.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProfessoraController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(GenericResponseJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Registrar(
        [FromServices] IRegistrarProfessoraUseCase useCase,
        [FromBody] RequestRegistrarProfessoraJson request)
    {
        var response = await useCase.ExecuteAysnc(request);

        return Created(string.Empty, response);
    }

}
