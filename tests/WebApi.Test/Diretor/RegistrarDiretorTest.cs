
using FluentAssertions;
using System.Net;
using System.Text.Json;
using Utilities.Requests;
using Xunit;

namespace WebApi.Test.Diretor;
public class RegistrarDiretorTest : ControllerBase
{
    private const string METODO = "diretor";

    public RegistrarDiretorTest(PortalEscolarWebApplicationFactory<Program> factory) : base(factory)
    {
        
    }
    [Fact]
    public async Task Validar_Sucesso()
    {
        var requisicao = RequestRegistrarDiretorBuilder.Build();

        var resposta = await PostRequest(METODO, requisicao);

        resposta.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responstaBody = await resposta.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responstaBody);

        responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
        responseData.RootElement.GetProperty("nome").GetString().Should().NotBeNullOrWhiteSpace();
    }
}
