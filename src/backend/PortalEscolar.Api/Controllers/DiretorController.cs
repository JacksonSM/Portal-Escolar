using Microsoft.AspNetCore.Mvc;
using PortalEscolar.Api.Filters.Autorizacao;
using PortalEscolar.Application.UseCases.Diretora.AlterarInfoPessoal;
using PortalEscolar.Application.UseCases.Diretora.AlterarSenha;
using PortalEscolar.Application.UseCases.Diretora.Login;
using PortalEscolar.Application.UseCases.Diretora.Matricular;
using PortalEscolar.Application.UseCases.Diretora.PrimeiroAcesso;
using PortalEscolar.Application.UseCases.Diretora.Registrar;
using PortalEscolar.Application.UseCases.Turma.Criar;
using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Request.Matricula;
using PortalEscolar.Communication.Response;
using PortalEscolar.Domain.Enum;

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
    [AutorizacaoPortalEscolar(new Papel[] { Papel.Diretor })]
    [ProducesResponseType(typeof(ResponseTokenJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> PrimeiroAcesso(
        [FromServices] IPrimeiroAcessoDiretor useCase,
        [FromBody] RequestUsuarioLoginJson request)
    {
        var response = await useCase.ExecuteAsync(request);

        return Ok(response);
    }
    [HttpPut("alterar-senha")]
    [AutorizacaoPortalEscolar(new Papel[] { Papel.Diretor })]
    [ProducesResponseType(typeof(GenericResponseJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> AlterarSenha(
        [FromServices] IAlterarSenhaDiretorUseCase useCase,
        [FromBody] RequestAlterarSenhaUsuarioJson request)
    {
        var response = await useCase.ExecuteAsync(request);

        return Ok(response);
    }
    [HttpPut("alterar-info-pessoal")]
    [AutorizacaoPortalEscolar(new Papel[] { Papel.Diretor })]
    [ProducesResponseType(typeof(ResponseInfoPessoalDiretorJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> AlterarInfoPessoal(
        [FromServices] IAlterarInfoPessoalDiretorUseCase useCase,
        [FromBody] RequestAlterarInfoPessoalDiretorJson request)
    {
        var response = await useCase.ExecuteAsync(request);

        return Ok(response);
    }
    [HttpPost("criar-turma")]
    [AutorizacaoPortalEscolar(new Papel[] { Papel.Diretor })]
    [ProducesResponseType(typeof(GenericResponseJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> CriarTurma(
        [FromServices] ICriarTurmaUseCase useCase,
        [FromBody] RequestCriarTurmaJson request)
    {
        var response = await useCase.ExecuteAsync(request);

        return Created(string.Empty,response);
    }
    [HttpPost("matricular-aluno")]
    [AutorizacaoPortalEscolar(new Papel[] { Papel.Diretor })]
    [ProducesResponseType(typeof(GenericResponseJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> MatricularAluno(
        [FromServices] IMatricularAlunoUseCase useCase,
        [FromBody] RequestMatricularAlunoJson request)
    {
        var response = await useCase.ExecuteAsync(request);

        return Created(string.Empty, response);
    }
}