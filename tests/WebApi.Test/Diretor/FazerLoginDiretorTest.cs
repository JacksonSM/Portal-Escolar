using FluentAssertions;
using PortalEscolar.Communication.Request;
using PortalEscolar.Exceptions;
using System.Net;
using System.Text.Json;
using Xunit;

namespace WebApi.Test.Diretor;
public class FazerLoginDiretorTest : ControllerBase
{
    private const string METODO = "api/diretor/login";
    private PortalEscolar.Domain.Entities.Diretoria.Diretor _diretor;
    private string _senha;

    public FazerLoginDiretorTest(PortalEscolarWebApplicationFactory<Program> factory) : base(factory)
    {
        _diretor = factory.ObterDiretor();
        _senha = factory.ObterSenha();
    }

    [Fact]
    public async Task EndpoinLogin_ParametrosValidos_TokenENomeValidos()
    {
        var request = new RequestUsuarioLoginJson { Email = _diretor.Email, Senha = _senha };
        var response = await PostRequest(METODO, request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
        responseData.RootElement.GetProperty("nome").GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Email_EmailInvalido_MensagemErroLoginInvalido()
    {
        var request = new RequestUsuarioLoginJson { Email = "teste@gmail.com" , Senha = _senha };
        var response = await PostRequest(METODO, request);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var erros = responseData.RootElement.GetProperty("mensagens").EnumerateArray();

        var mensagemEsperada = ResourceMensagensDeErro.LOGIN_INVALIDO;
        erros.Should().ContainSingle().And.Contain(c => c.GetString().Equals(mensagemEsperada));
    }
    [Fact]
    public async Task Senha_SenhaInvalido_MensagemErroLoginInvalido()
    {
        var request = new RequestUsuarioLoginJson { Email = _diretor.Email, Senha = "tes12@334" };
        var response = await PostRequest(METODO, request);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var erros = responseData.RootElement.GetProperty("mensagens").EnumerateArray();

        var mensagemEsperada = ResourceMensagensDeErro.LOGIN_INVALIDO;
        erros.Should().ContainSingle().And.Contain(c => c.GetString().Equals(mensagemEsperada));
    }
    [Fact]
    public async Task EmailESenha_EmailESenhaInvalido_MensagemErroLoginInvalido()
    {
        var request = new RequestUsuarioLoginJson { Email = "teste@gmail.com", Senha = "tes12@334" };
        var response = await PostRequest(METODO, request);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var erros = responseData.RootElement.GetProperty("mensagens").EnumerateArray();

        var mensagemEsperada = ResourceMensagensDeErro.LOGIN_INVALIDO;
        erros.Should().ContainSingle().And.Contain(c => c.GetString().Equals(mensagemEsperada));
    }
}
