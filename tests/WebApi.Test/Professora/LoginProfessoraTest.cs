using FluentAssertions;
using PortalEscolar.Communication.Request;
using PortalEscolar.Exceptions;
using System.Net;
using System.Text.Json;
using Xunit;

namespace WebApi.Test.Professora;
public class LoginProfessoraTest : ControllerBase
{
    private const string METODO = "api/professora/login";

    private PortalEscolar.Domain.Entities.SalaAula.ProfessoraContext.Professora _professora;
    private string _senha;

    public LoginProfessoraTest(PortalEscolarWebApplicationFactory<Program> factory) : base(factory)
    {
        _professora = factory.ObterProfessora();
        _senha = factory.ObterSenhaProfessora();
    }

    [Fact]
    public async Task EndpoinLoginProfessora_ParametrosValidos_TokenENomeValidos()
    {
        var request = new RequestUsuarioLoginJson { Email = _professora.Email, Senha = _senha };
        var response = await PostRequest(METODO, request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
        responseData.RootElement.GetProperty("nome").GetString().Should().NotBeNullOrWhiteSpace();
    }
    [Fact]
    public async Task Senha_SenhaInvalida_ErroLoginInvalido()
    {
        var request = new RequestUsuarioLoginJson { Email = _professora.Email, Senha = "senha@Invalida" };
        var response = await PostRequest(METODO, request);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var erros = responseData.RootElement.GetProperty("mensagens").EnumerateArray();

        erros.Should().ContainSingle().And.Contain(x => x.GetString().Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }
    [Fact]
    public async Task Email_EmailInvalida_ErroLoginInvalido()
    {
        var request = new RequestUsuarioLoginJson { Email = "invalido@gmail.com", Senha = _senha };
        var response = await PostRequest(METODO, request);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var erros = responseData.RootElement.GetProperty("mensagens").EnumerateArray();

        erros.Should().ContainSingle().And.Contain(x => x.GetString().Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }
    [Fact]
    public async Task EmailESenha_EmailESenhaInvalida_ErroLoginInvalido()
    {
        var request = new RequestUsuarioLoginJson { Email = "Invlado@gmail.com", Senha = "senha@Invalida" };
        var response = await PostRequest(METODO, request);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var erros = responseData.RootElement.GetProperty("mensagens").EnumerateArray();

        erros.Should().ContainSingle().And.Contain(x => x.GetString().Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }
}
