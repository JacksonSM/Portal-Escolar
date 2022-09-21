using FluentAssertions;
using PortalEscolar.Application.Responses;
using PortalEscolar.Exceptions;
using System.Net;
using System.Text.Json;
using Utilities.Requests;
using Utilities.Services.Token;
using Xunit;

namespace WebApi.Test.Diretor;
public class CriarTurmaTest : ControllerBase
{
    private const string METODO = "api/diretor/criar-turma";

    private PortalEscolar.Domain.Entities.Diretoria.Diretor _diretor;
    private PortalEscolar.Domain.Entities.SalaAula.Professora _professora;
    public CriarTurmaTest(PortalEscolarWebApplicationFactory<Program> factory) : base(factory)
    {
        _diretor = factory.ObterDiretor();
        _professora = factory.ObterProfessora();
    }

    [Fact]
    public async Task EndPointCriarTurma_DadosValido_StatusCreated()
    {
        var request = RequestCriarTurmaJsonBuilder.Build();
        request.ProfessoraId = _professora.Id;

        var tokenController = TokenControllerBuilder.Instance();

        var token = tokenController.GerarToken(_diretor);

        var response = await PostRequest(METODO, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responstaBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responstaBody);

        responseData.RootElement.GetProperty("mensagem").GetString().Should()
            .NotBeNullOrWhiteSpace().And.Contain(ResourceRespostasUseCases.TURMA_CRIADA_COM_SUCESSO);
    }
    [Fact]
    public async Task Professora_ProfessoraInexistente_StatusBadRequest()
    {
        var request = RequestCriarTurmaJsonBuilder.Build();
        request.ProfessoraId = 9563;

        var tokenController = TokenControllerBuilder.Instance();

        var token = tokenController.GerarToken(_diretor);

        var response = await PostRequest(METODO, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responstaBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responstaBody);

        var erros = responseData.RootElement.GetProperty("mensagens").EnumerateArray();
        erros.Should().ContainSingle().And.Contain(c => c.GetString()
            .Equals(ResourceMensagensDeErro.PROFESSORA_NAO_ENCONTRADA));
    }
}
