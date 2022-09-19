using FluentAssertions;
using System.Net;
using System.Text.Json;
using Utilities.Requests;
using Utilities.Services.Token;
using Xunit;

namespace WebApi.Test.Diretor;
public class AlterarInfoPessoalTest : ControllerBase
{
    private const string METODO = "api/diretor/alterar-info-pessoal";
    private PortalEscolar.Domain.Entities.Diretoria.Diretor _diretor;

    public AlterarInfoPessoalTest(PortalEscolarWebApplicationFactory<Program> factory) : base(factory)
    {
        _diretor = factory.ObterDiretor();
    }
    [Fact]
    public async Task EndpoinAlterarSenha_ParametrosValidos_MensagemdeSucesso()
    {
        var request = RequestAlterarInfoPessoalDiretorBuilder.Build();

        var tokenController = TokenControllerBuilder.Instance();

        var token = tokenController.GerarToken(_diretor);

        var response = await PutRequest(METODO, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("email").GetString().Should().NotBeNullOrWhiteSpace();
        responseData.RootElement.GetProperty("nomeCompleto").GetString().Should().NotBeNullOrWhiteSpace();
        responseData.RootElement.GetProperty("dataNascimento").GetString().Should().NotBeNullOrWhiteSpace();
    }

}
