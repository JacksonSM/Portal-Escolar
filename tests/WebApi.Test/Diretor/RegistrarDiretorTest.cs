
using FluentAssertions;
using PortalEscolar.Exceptions;
using System.Net;
using System.Text.Json;
using Utilities.Requests;
using Xunit;

namespace WebApi.Test.Diretor;
public class RegistrarDiretorTest : ControllerBase
{
    private const string METODO = "api/diretor";
    private PortalEscolar.Domain.Entities.Diretoria.Diretor _diretor;

    public RegistrarDiretorTest(PortalEscolarWebApplicationFactory<Program> factory) : base(factory)
    {
        _diretor = factory.ObterDiretor();
    }
    [Fact]
    public async Task Validar_Sucesso()
    {
        var request = RequestRegistrarDiretorBuilder.Build();

        var response = await PostRequest(METODO, request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responstaBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responstaBody);

        responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
        responseData.RootElement.GetProperty("nome").GetString().Should().NotBeNullOrWhiteSpace();
    }
    [Fact]
    public async Task Email_EmailExistente_DeveLancarExceptionEmailExistente()
    {
        var request = RequestRegistrarDiretorBuilder.Build();
        request.Email = _diretor.Email;

        var response = await PostRequest(METODO, request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responstaBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responstaBody);

        var erros = responseData.RootElement.GetProperty("mensagens").EnumerateArray();

        erros.Should().ContainSingle().And.Contain(c => c.GetString()
            .Equals(ResourceMensagensDeErro.EMAIL_EXISTENTE));
    }
}
