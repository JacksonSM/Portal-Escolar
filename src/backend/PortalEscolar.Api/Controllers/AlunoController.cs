﻿using Microsoft.AspNetCore.Mvc;
using PortalEscolar.Api.Filters.Autorizacao;
using PortalEscolar.Application.UseCases.Aluno.Registrar;
using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Response;
using PortalEscolar.Domain.Enum;

namespace PortalEscolar.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AlunoController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(GenericResponseJson), StatusCodes.Status201Created)]
    [AutorizacaoPortalEscolar(new Papel[] { Papel.Diretor })]
    public async Task<IActionResult> Registrar(
    [FromServices] IRegistrarAlunoUseCase useCase,
    [FromBody] RequestRegistrarAlunoJson request)
    {
        var resposta = await useCase.ExecuteAsync(request);

        return Created(string.Empty, resposta);
    }
}