using FluentAssertions;
using PortalEscolar.Exceptions;
using System.Net;
using System.Text.Json;
using Utilities.Entities;
using Utilities.Requests;
using Utilities.Services.Token;
using Xunit;

namespace WebApi.Test.Aluno;
public class RegistrarAlunoTest : ControllerBase
{
    private const string METODO = "api/aluno";

    private PortalEscolar.Domain.Entities.Diretoria.Diretor _diretor;
    public RegistrarAlunoTest(PortalEscolarWebApplicationFactory<Program> factory) : base(factory)
    {
        _diretor = factory.ObterDiretor();
    }

    [Fact]
    public async Task EndPointRegistrarAluno_DadosValido_StatusCreted()
    {
        var request = RequestRegistrarAlunoBuilder.Build();

        var tokenController = TokenControllerBuilder.Instance();

        var token = tokenController.GerarToken(_diretor);

        var response = await PostRequest(METODO, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responstaBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responstaBody);

        responseData.RootElement.GetProperty("mensagem").GetString().Should()
            .NotBeNullOrWhiteSpace().And.Contain(ResourceMensagensDeErro.REGISTRAR_ALUNO_SUCESSO);

    }
    [Fact]
    public async Task NomeCompleto_NomeCompletoVazio_StatusBadRequest()
    {
        var request = RequestRegistrarAlunoBuilder.Build();
        request.NomeCompleto = string.Empty;

        var tokenController = TokenControllerBuilder.Instance();

        var token = tokenController.GerarToken(_diretor);

        var response = await PostRequest(METODO, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responstaBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responstaBody);

        var erros = responseData.RootElement.GetProperty("mensagens").EnumerateArray();

        erros.Should().ContainSingle().And.Contain(c => c.GetString()
            .Equals(ResourceMensagensDeErro.NOMECOMPLETO_VAZIO));

    }
    [Fact]
    public async Task Autorizacao_UsuarioNaoAutorizado_Status401Unauthorized()
    {
        var request = RequestRegistrarAlunoBuilder.Build();
        request.NomeCompleto = string.Empty;

        (var usuarioProfessora, var _) = ProfessoraBuilder.Build();

        var tokenController = TokenControllerBuilder.Instance();

        var token = tokenController.GerarToken(usuarioProfessora);

        var response = await PostRequest(METODO, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responstaBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responstaBody);

        var erros = responseData.RootElement.GetProperty("mensagens").EnumerateArray();

        erros.Should().ContainSingle().And.Contain(c => c.GetString()
            .Equals(ResourceMensagensDeErro.USUARIO_NAO_AUTORIZADO));

    }

    [Fact]
    public async Task Token_TokenExpirado_Status401Unauthorized()
    {
        var request = RequestRegistrarAlunoBuilder.Build();

        var tokenController = TokenControllerBuilder.ExpiredToken();

        var token = tokenController.GerarToken(_diretor);

        Thread.Sleep(1000);

        var response = await PostRequest(METODO, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responstaBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responstaBody);

        var erros = responseData.RootElement.GetProperty("mensagens").EnumerateArray();

        erros.Should().ContainSingle().And.Contain(c => c.GetString()
            .Equals(ResourceMensagensDeErro.TOKEN_EXPIRADO));

    }
}
