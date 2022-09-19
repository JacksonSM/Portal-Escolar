using FluentAssertions;
using PortalEscolar.Application.Responses;
using PortalEscolar.Exceptions;
using System.Net;
using System.Text;
using System.Text.Json;
using Utilities.Requests;
using Utilities.Services.Token;
using Xunit;

namespace WebApi.Test.Diretor;
public class AlterarSenhaDiretorTest : ControllerBase
{
    private const string METODO = "api/diretor/alterar-senha";
    private PortalEscolar.Domain.Entities.Diretoria.Diretor _diretor;
    private string _senha;

    public AlterarSenhaDiretorTest(PortalEscolarWebApplicationFactory<Program> factory) : base(factory)
    {
        _diretor = factory.ObterDiretor();
        _senha = factory.ObterSenha();
    }

    [Fact]
    public async Task EndpoinAlterarSenha_ParametrosValidos_MensagemdeSucesso()
    {
        var request = RequestAlterarSenhaUsuarioBuilder.Build();
        request.SenhaAtual = _senha;

        var tokenController = TokenControllerBuilder.Instance();

        var token = tokenController.GerarToken(_diretor);

        var response = await PutRequest(METODO, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("mensagem").GetString().Should().NotBeNullOrWhiteSpace();
        responseData.RootElement.GetProperty("mensagem")
            .GetString()
            .Should().Contain(ResourceRespostasUseCases.SENHA_ALTERADA_COM_SUCESSO);
        
    }
    [Fact]
    public async Task SenhaAtual_SenhaAtualInvalida_MensagemErroSenhaAtualInvalida()
    {
        var request = RequestAlterarSenhaUsuarioBuilder.Build();

        var tokenController = TokenControllerBuilder.Instance();

        var token = tokenController.GerarToken(_diretor);

        var response = await PutRequest(METODO, request, token);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var erros = responseData.RootElement.GetProperty("mensagens").EnumerateArray();

        erros.Should().ContainSingle().And.Contain(c => c.GetString()
            .Equals(ResourceMensagensDeErro.SENHAATUAL_INVALIDA));

    }
}