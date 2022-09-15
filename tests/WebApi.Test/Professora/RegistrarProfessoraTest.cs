using FluentAssertions;
using PortalEscolar.Exceptions;
using System.Net;
using System.Text.Json;
using Utilities.Requests;
using Xunit;

namespace WebApi.Test.Professora;
public class RegistrarProfessoraTest : ControllerBase
{
    private const string METODO = "api/professora";
    public RegistrarProfessoraTest(PortalEscolarWebApplicationFactory<Program> factory) : base(factory)
    {

    }

    [Fact]
    public async Task EndPointRegistrarProfessora_DadosValido_StatusCreted()
    {
        var request = RequestRegistrarProfessoraBuilder.Build();

        var response = await PostRequest(METODO, request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responstaBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responstaBody);

        responseData.RootElement.GetProperty("mensagem").GetString().Should()
            .NotBeNullOrWhiteSpace().And.Contain(ResourceMensagensDeErro.REGISTRAR_PROFESSORA_SUCESSO);
        
    }
    [Fact]
    public async Task NomeCompleto_NomeCompletoVazio_StatusBadRequest()
    {
        var request = RequestRegistrarProfessoraBuilder.Build();
        request.NomeCompleto = string.Empty;

        var response = await PostRequest(METODO, request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responstaBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responstaBody);

        var erros = responseData.RootElement.GetProperty("mensagens").EnumerateArray();

        erros.Should().ContainSingle().And.Contain(c => c.GetString()
            .Equals(ResourceMensagensDeErro.NOMECOMPLETO_VAZIO));

    }
}
